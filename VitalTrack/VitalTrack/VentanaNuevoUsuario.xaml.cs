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
    /// Interaction logic for VentanaNuevoUsuario.xaml
    /// </summary>
    public partial class VentanaNuevoUsuario : Window
    {
        public VentanaNuevoUsuario()
        {
            InitializeComponent();
        }

        VitaltrackContext db = new VitaltrackContext();
        String nombreArchivoFoto;
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
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string directoryDestino = Path.Combine(baseDir, "Images", "Fotos");
                    Directory.CreateDirectory(directoryDestino);

                    string nombreArchivo = Path.GetFileName(trayectoria);
                    string trayectoriaDestino = Path.Combine(directoryDestino, nombreArchivo);

                    if (File.Exists(trayectoriaDestino))
                    {
                        string nombreSinExt = Path.GetFileNameWithoutExtension(nombreArchivo);
                        string ext = Path.GetExtension(nombreArchivo);
                        string unico = $"{nombreSinExt}_{DateTime.Now:yyyyMMdd_HHmmssfff}{ext}";
                        trayectoriaDestino = Path.Combine(directoryDestino, unico);
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

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos texto:
            if ( txtCuentaUsuario.Text.Trim() == "" ||
                 txtContrasena.Password.Trim() == "" ||
                 txtNombreUsuario.Text.Trim() == "" ||
                 txtApellidosUsuario.Text.Trim() == "" ||
                 txtTelefonoUsuario.Text.Trim() == "" ||
                 txtEmailUsuario.Text.Trim() == "" )
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Comprobar que no existe ya el nombre de usuario
           if (db.Usuarios.FirstOrDefault(u => u.NombreUsuario == txtCuentaUsuario.Text.Trim()) != null)
            {
                MessageBox.Show("El nombre de usuario ya está en uso.", "Nombre de usuario existente", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCuentaUsuario.Focus();
                return;
            }

            // Comprobar que no existe otro usuario con ese email
           if (db.Usuarios.FirstOrDefault(u => u.Email == txtEmailUsuario.Text.Trim()) != null)
            {
                MessageBox.Show("Este email ya está en uso.", "Email ya utilizado", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmailUsuario.Focus();
                return;
            }

            // Validar teléfono español con regex
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

            // Prepara una imagen para usuario sin foto
            var bitmapDesconocido = new BitmapImage();
            bitmapDesconocido.BeginInit();
            bitmapDesconocido.CacheOption = BitmapCacheOption.OnLoad; // para evitar un bloqueo en el archivo
            bitmapDesconocido.UriSource = new Uri("pack://application:,,,/Images/Fotos/desconocido.png", UriKind.Absolute);
            bitmapDesconocido.EndInit();

            if (fotoUsuario.Source == null)
            {
                fotoUsuario.Source = bitmapDesconocido;
                nombreArchivoFoto = "desconocido.png";
            }

            // Guardar en base de datos
            Usuario usuario = new Usuario();
            usuario.NombreUsuario =  txtCuentaUsuario.Text.Trim();
            usuario.HashPassword = CalcularHashSha256(txtContrasena.Password);
            usuario.Nombre = txtNombreUsuario.Text.Trim();  
            usuario.Apellidos = txtApellidosUsuario.Text.Trim();
            usuario.Telefono = txtTelefonoUsuario.Text.Trim();
            usuario.Email = txtEmailUsuario.Text.Trim();
            usuario.Foto = nombreArchivoFoto;
            
            db.Usuarios.Add(usuario);
            db.SaveChanges();

            // Borrar campos, tras éxito en operación:
            txtCuentaUsuario.Clear();
            txtContrasena.Clear();
            txtNombreUsuario.Clear();
            txtApellidosUsuario.Clear();
            txtEmailUsuario.Clear();
            txtTelefonoUsuario.Clear(); 
            fotoUsuario.Source = bitmapDesconocido; 
            txtCreadoUsuario.SelectedDate = DateTime.Today;
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
