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