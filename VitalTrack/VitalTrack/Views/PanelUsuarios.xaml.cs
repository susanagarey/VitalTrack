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
        public PanelUsuarios()
        {
            InitializeComponent();

            using(VitaltrackContext db = new VitaltrackContext())
            {
                List<Usuario> usuarios = db.Usuarios.ToList();
                gridUsuarios.ItemsSource = usuarios;
            }
        }

        Usuario usuarioGlobal;
        private void gridUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Usuario usuario = (Usuario)gridUsuarios.SelectedItem;
            if (usuarioGlobal == null) {
                usuarioGlobal = usuario;
            }
            usuario = usuario ?? usuarioGlobal;

            txtUsuarioID.Text = usuario.UsuarioId.ToString();
            txtActivo.Text = usuario.Activo.HasValue ? (usuario.Activo.Value ? "Sí" : "No") : "Desconocido";
            txtCuentaUsuario.Text = usuario.NombreUsuario;
            txtNombreUsuario.Text = usuario.Nombre;
            txtApellidosUsuario.Text = usuario.Apellidos;
            txtTelefonoUsuario.Text = usuario.Telefono;
            txtEmailUsuario.Text = usuario.Email;
            txtCreadoUsuario.Text = usuario.CreadoEn.ToString();
            txtUltimoAccesoUsuario.Text = usuario.UltimoAcceso.ToString();
            
            fotoUsuario.Source = new BitmapImage(new Uri("/Images/Fotos/" + (usuario.Foto ?? ("desconocido.png")), UriKind.Relative));
        }

        private void btnAlta_Click(object sender, RoutedEventArgs e)
        {
            usuarioGlobal = (Usuario)gridUsuarios.SelectedItem;
            VentanaNuevoUsuario ventana = new VentanaNuevoUsuario();
            ventana.ShowDialog();

            using (VitaltrackContext db = new VitaltrackContext())
            {
                List<Usuario> usuarios = db.Usuarios.ToList();
                gridUsuarios.ItemsSource = usuarios;
            }
        }
    }
}
