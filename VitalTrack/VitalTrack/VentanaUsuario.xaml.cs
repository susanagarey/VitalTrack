using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Path = System.IO.Path;
using VitalTrack.Models;
using VitalTrack.Data;
using System.Security.Cryptography;

namespace VitalTrack
{
    /// <summary>
    /// Interaction logic for VentanaUsuario.xaml
    /// </summary>
    public partial class VentanaUsuario : Window
    {
        String? nombreArchivoFoto;
        public Usuario ultimoUsuarioCreado;

        public VentanaUsuario()
        {
            InitializeComponent();
        }


        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            // Validar campos texto:
            if ( txtCuentaUsuario.Text.Trim()    == "" ||
                 txtContrasena.Password.Trim()   == "" ||
                 txtNombreUsuario.Text.Trim()    == "" ||
                 txtApellidosUsuario.Text.Trim() == "" ||
                 txtTelefonoUsuario.Text.Trim()  == "" ||
                 txtEmailUsuario.Text.Trim()     == "" )
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Comprobar que no existe ya el nombre de usuario
            using (VitaltrackContext db = new VitaltrackContext())
            {
                if (db.Usuarios.FirstOrDefault(u => u.NombreUsuario == txtCuentaUsuario.Text.Trim()) != null)
                {
                    MessageBox.Show("El nombre de usuario ya está en uso.", "Nombre de usuario existente", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCuentaUsuario.Focus();
                    return;
                }

                if (db.Usuarios.FirstOrDefault(u => u.Email == txtEmailUsuario.Text.Trim()) != null)
                {
                    MessageBox.Show("Este email ya está en uso.", "Email ya utilizado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtEmailUsuario.Focus();
                    return;
                }
            }

            // Validar teléfono español con regex (también se puede hacer con el evento PreviewTextInput)
            if (!esNumeroTelefonoValido(txtTelefonoUsuario.Text))
            {
                MessageBox.Show("Introduzca un teléfono español válido (9 dígitos comenzando por 6, 7, 8 o 9; puede incluir prefijo +34/0034 y separadores espacio o guion).",
                                "Teléfono inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTelefonoUsuario.Focus();
                return;
            }

            // Validar email básico
            if (!esEmailValido(txtEmailUsuario.Text))
            {
                MessageBox.Show("Introduzca un email válido.",
                                "Email inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmailUsuario.Focus();
                return;
            }

            // Imagen por defecto si no se ha cargado foto
            var bitmap = new BitmapImage();
            if (fotoUsuario.Source == null || string.IsNullOrWhiteSpace(nombreArchivoFoto))
            {
                nombreArchivoFoto = App.FotoPorDefecto;
                string trayectoriaDefecto = ObtenerTrayectoriaFoto(nombreArchivoFoto);
                if ( !File.Exists(trayectoriaDefecto) )
                {
                    // Último recurso: pack URI 
                    trayectoriaDefecto = "pack://application:,,,/Images/Fotos/" + App.FotoPorDefecto;
                }

                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(trayectoriaDefecto, UriKind.Absolute);
                bitmap.EndInit();
                fotoUsuario.Source = bitmap;
            }

            // Guardar en base de datos
            Usuario usuario       = new Usuario();
            usuario.NombreUsuario = txtCuentaUsuario.Text.Trim();
            usuario.HashPassword  = CalcularHashSha256(txtContrasena.Password);
            usuario.Nombre        = txtNombreUsuario.Text.Trim();
            usuario.Apellidos     = txtApellidosUsuario.Text.Trim();
            usuario.Telefono      = txtTelefonoUsuario.Text.Trim();
            usuario.Email         = txtEmailUsuario.Text.Trim();
            usuario.Foto          = nombreArchivoFoto;

            using (VitaltrackContext db = new VitaltrackContext())
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
            }

            ultimoUsuarioCreado = usuario;
            MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Borrar campos, tras éxito en operación:
            txtCuentaUsuario.Clear();
            txtContrasena.Clear();
            txtNombreUsuario.Clear();
            txtApellidosUsuario.Clear();
            txtEmailUsuario.Clear();
            txtTelefonoUsuario.Clear();
            fotoUsuario.Source = bitmap;
            dpCreadoUsuario.SelectedDate = DateTime.Today;
        } 
           
        // Funciones auxiliares
        private static string CalcularHashSha256(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;
            using var sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            byte[] hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        private static bool esNumeroTelefonoValido(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var value = phone.Trim();
            return ExpresionRegularNumeroTelefonoValido().IsMatch(value);
        }

        private static string ObtenerTrayectoriaFoto(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return string.Empty;
            return Path.Combine(App.CarpetaFotos, fileName);
        }

        private void btnCargarFoto_Click(object sender, RoutedEventArgs e)
        {
            var dialogo = new OpenFileDialog
            {
                Title = "Seleccione un fichero gráfico",
                Filter = "Imágenes (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg"
            };

            if (dialogo.ShowDialog() == true)
            {
                string trayectoria = dialogo.FileName;

                try
                {
                    string directorioDestino = App.CarpetaFotos;
                    Directory.CreateDirectory(directorioDestino);

                    string nombreArchivo = Path.GetFileName(trayectoria);
                    string trayectoriaDestino = Path.Combine(directorioDestino, nombreArchivo);

                    if (File.Exists(trayectoriaDestino))
                    {
                        // Si ya existe una foto con ese nombre, generar uno único
                        string nombreSinExt = Path.GetFileNameWithoutExtension(nombreArchivo);
                        string ext          = Path.GetExtension(nombreArchivo);
                        string unico        = $"{nombreSinExt}_{DateTime.Now:yyyyMMdd_HHmmssfff}{ext}";
                        nombreArchivo       = unico;
                        trayectoriaDestino  = Path.Combine(directorioDestino, unico);
                    }

                    File.Copy(trayectoria, trayectoriaDestino);

                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // para evitar un bloqueo en el archivo
                    bitmap.UriSource = new Uri(trayectoriaDestino, UriKind.Absolute);
                    bitmap.EndInit();
                    bitmap.Freeze();

                    fotoUsuario.Source = bitmap;
                    nombreArchivoFoto = nombreArchivo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo copiar/mostrar la imagen.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Patrón:
        // ^(?:\+34|0034)?\s*([6789])(?:[\s\-]?\d){8}$
        // - ^ y $ anclan el inicio/fin
        // - (?:\+34|0034)? prefijo opcional de España
        // - \s* permite espacios tras el prefijo
        // - ([6789]) primer dígito nacional válido (móvil: 6/7, fijo: 8/9)
        // - (?:[\s\-]?\d){8} resto de 8 dígitos, permitiendo espacios/guiones entre ellos
        // [GeneratedRegex(@"^(?:\+34|0034)?\s*([6789])(?:[\s\-]?\d){8}$", RegexOptions.CultureInvariant)]
        private static Regex ExpresionRegularNumeroTelefonoValido()
        {
            return new Regex(@"^(?:\+34|0034)?\s*([6789])(?:[\s\-]?\d){8}$", RegexOptions.CultureInvariant);
        }

        private static bool esEmailValido(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var value = email.Trim();
            return ExpresionRegularEmailBasico().IsMatch(value);
        }

        // Patrón básico de email:
        // ^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+@([A-Za-z0-9-]+\.)+[A-Za-z]{2,}$
        // - Limita longitud total (254) y de la parte local (64)
        // - Permite caracteres comunes en la parte local
        // - Dominio compuesto por etiquetas válidas separadas por puntos
        // - TLD de al menos 2 letras
        private static Regex ExpresionRegularEmailBasico()
        {
            return new Regex(
                @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+@([A-Za-z0-9-]+\.)+[A-Za-z]{2,}$",
                RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        }
    }
}
