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
    /// <summary>
    /// Interaction logic for PanelUsuarios.xaml
    /// </summary>
    public partial class PanelUsuarios : UserControl
    {
        Usuario? usuarioGlobal;

        public PanelUsuarios()
        {
            InitializeComponent();

            RefrescarListaUsuarios();
        }

        private void gridUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Usuario usuario = (Usuario)gridUsuarios.SelectedItem;

            if  (usuario != null )
            {
                usuarioGlobal = usuario;
            }

            usuario = usuario ?? usuarioGlobal ?? gridUsuarios.Items.OfType<Usuario>().FirstOrDefault();

            if (usuario == null)
                return;

            txtUsuarioID.Text           = usuario.UsuarioId.ToString();
            txtActivoUsuario.Text       = usuario.Activo.HasValue ? (usuario.Activo.Value ? "Sí" : "No") : "Desconocido";
            txtCuentaUsuario.Text       = usuario.NombreUsuario;
            txtNombreUsuario.Text       = usuario.Nombre;
            txtApellidosUsuario.Text    = usuario.Apellidos;
            txtTelefonoUsuario.Text     = usuario.Telefono;
            txtEmailUsuario.Text        = usuario.Email;
            txtCreadoUsuario.Text       = usuario.CreadoEn.ToString();
            txtUltimoAccesoUsuario.Text = usuario.UltimoAcceso == null ? "Sin datos" : usuario.UltimoAcceso.ToString();

            string trayectoriaFoto = App.CarpetaFotos + (usuario.Foto ?? ("desconocido.png"));

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(trayectoriaFoto, UriKind.Relative);
            bitmap.EndInit();
            fotoUsuario.Source = bitmap;
        }

        private void btnAlta_Click(object sender, RoutedEventArgs e)
        {
            usuarioGlobal = (Usuario)gridUsuarios.SelectedItem;  // Si al final no se realiza alta, mantener el usuario seleccionado

            VentanaUsuario ventana = new VentanaUsuario();
            ventana.ShowDialog();

            usuarioGlobal = ventana.ultimoUsuarioCreado ?? usuarioGlobal;

            RefrescarListaUsuarios();

            if (usuarioGlobal != null)
            {
                int index = ObtenerIndiceCorrespondiente(usuarioGlobal.UsuarioId);
                if (index >= 0)
                    gridUsuarios.SelectedIndex = index;
            }
        }
        

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = (Usuario)gridUsuarios.SelectedItem;
            VentanaUsuario ventanaUsuarios = new VentanaUsuario();
            ventanaUsuarios.ultimoUsuarioCreado = usuario;
            ventanaUsuarios.ShowDialog();

            RefrescarListaUsuarios();
        }

        private void RefrescarListaUsuarios()
        {
            using (VitaltrackContext db = new VitaltrackContext())
            {
                List<Usuario> usuarios = db.Usuarios.ToList();
                gridUsuarios.ItemsSource = usuarios;
            }

            if (usuarioGlobal != null)
            {
                gridUsuarios.SelectedItem = usuarioGlobal;
            }
        }

        private int ObtenerIndiceCorrespondiente(uint usuarioId)
        {
            for (int i = 0; i < gridUsuarios.Items.Count; i++)
            {
                if (gridUsuarios.Items[i] is Usuario u && u.UsuarioId == usuarioId)
                    return i;
            }
            return -1;
        }
    }
}
