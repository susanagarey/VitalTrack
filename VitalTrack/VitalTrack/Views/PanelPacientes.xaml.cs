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
    /// Interaction logic for PanelPacientes.xaml
    /// </summary>
    public partial class PanelPacientes : UserControl
    {
        public PanelPacientes()
        {
            InitializeComponent();

            using (VitaltrackContext db = new VitaltrackContext())
            {
                List<Paciente> pacientes = db.Pacientes.ToList();
                gridPacientes.ItemsSource = pacientes;
            }
        }

        private void gridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
