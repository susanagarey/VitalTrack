using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VitalTrack.Data;
using VitalTrack.Models;

namespace VitalTrack.Views
{
    public partial class PanelUsuarios : UserControl
    {
        Usuario? usuarioSeleccionadoGlobal;

        public PanelUsuarios()
        {
            InitializeComponent();
            RefrescarListaUsuarios();
        }

        private void gridUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            usuarioSeleccionadoGlobal = (Usuario)gridUsuarios.SelectedItem ?? gridUsuarios.Items.OfType<Usuario>().FirstOrDefault(); 

            if (usuarioSeleccionadoGlobal == null)
                return;

            string trayectoriaFoto;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                Usuario objetoUsuario = db.Usuarios.Find(usuarioSeleccionadoGlobal.UsuarioId)!;
                txtUsuarioID.Text           = objetoUsuario.UsuarioId.ToString();
                txtActivoUsuario.Text       = objetoUsuario.Activo.HasValue ? (objetoUsuario.Activo.Value ? "Sí" : "No") : "Desconocido";
                txtCuentaUsuario.Text       = objetoUsuario.NombreUsuario;
                txtNombreUsuario.Text       = objetoUsuario.Nombre;
                txtApellidosUsuario.Text    = objetoUsuario.Apellidos;
                txtTelefonoUsuario.Text     = objetoUsuario.Telefono;
                txtEmailUsuario.Text        = objetoUsuario.Email;
                txtCreadoUsuario.Text       = objetoUsuario.CreadoEn.ToString();
                txtUltimoAccesoUsuario.Text = objetoUsuario.UltimoAcceso == null ? "Sin datos" : usuarioSeleccionadoGlobal.UltimoAcceso.ToString();
                trayectoriaFoto             = App.CarpetaFotos + (objetoUsuario.Foto ?? ("desconocido.png"));
            }

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(trayectoriaFoto, UriKind.Relative);
            bitmap.EndInit();
            fotoUsuario.Source = bitmap;
        }

        private void btnAlta_Click(object sender, RoutedEventArgs e)
        {
            usuarioSeleccionadoGlobal = (Usuario)gridUsuarios.SelectedItem;  // Si al final no se realiza alta, mantener el usuario seleccionado

            if (usuarioSeleccionadoGlobal == null)
            {
                return;
            }

            VentanaUsuario ventana = new VentanaUsuario();
            ventana.ShowDialog();

            usuarioSeleccionadoGlobal = ventana.usuarioAlta ?? usuarioSeleccionadoGlobal;   // Si no hubo alta, mantener el usuario seleccionado

            RefrescarListaUsuarios(usuarioSeleccionadoGlobal);
        }
        

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = (Usuario)gridUsuarios.SelectedItem;

            if (usuario == null) {
                MessageBox.Show("No hay ningún usuario seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            VentanaUsuario ventanaUsuarios = new VentanaUsuario();
            ventanaUsuarios.usuarioActualizar = usuario;
            ventanaUsuarios.ShowDialog();

            usuarioSeleccionadoGlobal = ventanaUsuarios.usuarioAlta ?? usuarioSeleccionadoGlobal;
            RefrescarListaUsuarios(usuarioSeleccionadoGlobal);
        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = (Usuario)gridUsuarios.SelectedItem;
            if (usuario == null)
            {
                MessageBox.Show("No hay ningún usuario seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas dar de baja a este usuario?", "Confirmar baja", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (VitaltrackContext db = new VitaltrackContext())
                {
                    Usuario objetoUsuario = db.Usuarios.Find(usuario.UsuarioId);
                    objetoUsuario.Activo = false;

                    db.SaveChanges();
                }

                // Limpiar los detalles del usuario mostrado
                txtUsuarioID.Text = "";
                txtActivoUsuario.Text = "";
                txtCuentaUsuario.Text = "";
                txtNombreUsuario.Text = "";
                txtApellidosUsuario.Text = "";
                txtTelefonoUsuario.Text = "";
                txtEmailUsuario.Text = "";
                txtCreadoUsuario.Text = "";
                txtUltimoAccesoUsuario.Text = "";

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(App.CarpetaFotos + App.FotoPorDefecto, UriKind.Relative);
                bitmap.EndInit();
                fotoUsuario.Source = bitmap;

                // Refrescar la lista de usuarios
                RefrescarListaUsuarios();
            }
        }

        private void RefrescarListaUsuarios(Usuario? usuarioSeleccionado = null)
        {
            List<Usuario> usuarios;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                usuarios = db.Usuarios.Where(x=> x.Activo == true).ToList();
            }
          
            gridUsuarios.ItemsSource = usuarios;

            if (usuarioSeleccionado != null)
            {
                gridUsuarios.SelectedItem = usuarios.FirstOrDefault(u => u.UsuarioId == usuarioSeleccionado.UsuarioId); 
            }
            else
            {
                gridUsuarios.SelectedItem = usuarios.Count > 0 ? usuarios[0] : null;
            }
        }
    }
}
