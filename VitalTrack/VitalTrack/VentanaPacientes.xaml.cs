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
using System.Windows.Shapes;
using VitalTrack.Models;
using VitalTrack.ViewModels;

namespace VitalTrack
{
    /// <summary>
    /// Interaction logic for VentanaPacientes.xaml
    /// </summary>
    public partial class VentanaPacientes : Window
    {
        public PacienteAmpliadoViewModel? pacienteAlta;
        public PacienteAmpliadoViewModel? pacienteActualizar;
        public VentanaPacientes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Alta Paciente";
            this.Title = "VitalTrack - Alta Paciente";

            if ( pacienteActualizar != null )
            {
                lbTitulo.Content = "Actualizar Paciente";
                this.Title       = "VitalTrack - Actualizar Paciente";
                // Rellenar campos con datos del paciente a actualizar
                txtPacienteID.Text         = pacienteActualizar.PacienteId.ToString();
                txtActivo.Text             = pacienteActualizar.Activo.HasValue ? (pacienteActualizar.Activo.Value ? "Sí" : "No") : "Desconocido";

                txtNombrePaciente.Text        = pacienteActualizar.Nombre;
                txtApellidosPaciente.Text     = pacienteActualizar.Apellidos;
                txtDNIPaciente.Text           = pacienteActualizar.Dni;
                txtTelefonoPaciente.Text      = pacienteActualizar.Telefono;
                txtEmailPaciente.Text         = pacienteActualizar.Email;
                txtDireccionPaciente.Text     = pacienteActualizar.Direccion;
                txtObservacionesPaciente.Text = pacienteActualizar.Observaciones;
                
                dpFechaNacimientoPaciente.SelectedDate = pacienteActualizar.FechaNacimiento.HasValue ?
                          pacienteActualizar.FechaNacimiento.Value.ToDateTime(TimeOnly.MinValue)
                        : null;

                rbHombre.IsChecked      = pacienteActualizar.Sexo == Constantes.Sexos.Masculino;
                rbMujer.IsChecked       = pacienteActualizar.Sexo == Constantes.Sexos.Femenino;
                rbTrans.IsChecked       = pacienteActualizar.Sexo == Constantes.Sexos.Trans;
                rbNoDeclarado.IsChecked = pacienteActualizar.Sexo == Constantes.Sexos.NoDeclarado;

                txtCreadorPaciente.Text       = pacienteActualizar.NombreCreadoPor;
                txtActualizadorPaciente.Text  = pacienteActualizar.NombreActualizadoPor;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
