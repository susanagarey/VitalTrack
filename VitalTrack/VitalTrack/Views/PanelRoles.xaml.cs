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

namespace VitalTrack.Views
{
    /// <summary>
    /// Interaction logic for PanelRoles.xaml
    /// </summary>
    public partial class PanelRoles : UserControl
    {
        public PanelRoles()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar combo roles con datos
            using (VitaltrackContext db = new VitaltrackContext())
            {
                cmbRoles.ItemsSource = db.Roles.ToList();
            }
            cmbRoles.DisplayMemberPath = "Nombre";
            cmbRoles.SelectedValuePath = "RolId";
            cmbRoles.SelectedIndex      = -1;

            RefrescarListaRoles();
            RefrescarListaUsuarios();
        }

        private void btnAsignarRol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefrescarListaRoles()
        {

        }

        private void RefrescarListaUsuarios()
        {

        }
    }
}
