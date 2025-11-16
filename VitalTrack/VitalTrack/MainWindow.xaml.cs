using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VitalTrack.ViewModels;

namespace VitalTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool solicitadoFinalApp = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void VentanaPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            if (UsuarioActual.RolId != Constantes.Roles.ADMINISTRADOR )
            {
                btnUsuarios.IsEnabled        = true;
                btnUsuarios.IsHitTestVisible = false;
                btnUsuarios.Focusable        = false;
                UsuarioImagen.Visibility     = Visibility.Hidden;
                txtUsuarios.Visibility       = Visibility.Hidden;
                
                btnRoles.IsEnabled        = true;
                btnRoles.IsHitTestVisible = false;
                btnRoles.Focusable        = false;
                RolesImagen.Visibility    = Visibility.Hidden;
                txtRoles.Visibility       = Visibility.Hidden;
                
                btnAuditoria.IsEnabled        = true;
                btnAuditoria.IsHitTestVisible = false;
                btnAuditoria.Focusable        = false;
                AuditoriaImagen.Visibility    = Visibility.Hidden;
                txtAuditoria.Visibility       = Visibility.Hidden;
            }

            if (UsuarioActual.RolId == Constantes.Roles.PACIENTE ||
                 UsuarioActual.RolId == Constantes.Roles.RECEPCIONISTA)
            {
                btnConstantes.IsEnabled = true;
                btnConstantes.IsHitTestVisible = false;
                btnConstantes.Focusable = false;
                ConstantesImagen.Visibility = Visibility.Hidden;
                txtConstantes.Visibility = Visibility.Hidden;

                btnGraficas.IsEnabled = true;
                btnGraficas.IsHitTestVisible = false;
                btnGraficas.Focusable = false;
                GraficasImagen.Visibility = Visibility.Hidden;
                txtGraficas.Visibility = Visibility.Hidden;

                btnAlertas.IsEnabled = true;
                btnAlertas.IsHitTestVisible = false;
                btnAlertas.Focusable = false;
                AlertasImagen.Visibility = Visibility.Hidden;
                txtAlertas.Visibility = Visibility.Hidden;
            }

            lblNombreVentana.Content = "Gestión pacientes";
            DataContext = new PacientesViewModel();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            lblNombreVentana.Content = "Gestión usuarios";
            DataContext = new UsuariosViewModel();
        }

         private void btnPacientes_Click(object sender, RoutedEventArgs e)
        {
            lblNombreVentana.Content = "Gestión pacientes";
            DataContext = new PacientesViewModel();
        }

        private void btnConstantes_Click(object sender, RoutedEventArgs e)
        {
            lblNombreVentana.Content = "Gestión constantes";
            DataContext = new ConstantesViewModel();
        }

        private void btnGraficas_Click(object sender, RoutedEventArgs e)
        {
            lblNombreVentana.Content = "Gráficas";
            DataContext = new GraficasViewModel();
        }

        private void btnAlertas_Click(object sender, RoutedEventArgs e)
        {
            lblNombreVentana.Content = "Gestión alertas";
            DataContext = new AlertasViewModel();
        }

        private void btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            lblNombreVentana.Content = "Auditoría";
            DataContext = new AuditoriaViewModel();
        }

        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            lblNombreVentana.Content = "Gestión roles";
            DataContext = new RolesViewModel();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            solicitadoFinalApp = true;
            this.Close();
        }
    }
}