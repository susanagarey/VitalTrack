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
    /// Interaction logic for PanelConstantes.xaml
    /// </summary>
    public partial class PanelConstantes : UserControl
    {
        private Paciente pacienteSeleccionado;

        public PanelConstantes()
        {
            InitializeComponent();
            RefrescarListaPacientes();
            RefrescarListaConstantesPaciente();
        }


        private void gridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            pacienteSeleccionado = (Paciente)gridPacientes.SelectedItem!;
            RefrescarListaConstantesPaciente();
        }

        private void RefrescarListaConstantesPaciente()
        {
            List<UmbralesPaciente> constantes;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                constantes = db.UmbralesPacientes.Where(x => x.PacienteId == pacienteSeleccionado.PacienteId).ToList();
            }

            gridConstantes.ItemsSource = constantes;

            gridConstantes.SelectedItem = constantes.Count > 0 ? constantes[0] : null;
        }


        private void RefrescarListaPacientes()
        {
            List<Paciente> pacientes;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                pacientes = db.Pacientes.Where(x => x.Activo == true).ToList();
            }
            
            if (pacienteSeleccionado == null && pacientes.Count > 0)
            {
                pacienteSeleccionado = pacientes[0];
            }

            gridPacientes.ItemsSource = pacientes;
        }
        
    }
}
