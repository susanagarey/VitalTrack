using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VitalTrack.Data;
using VitalTrack.Models;

namespace VitalTrack.Views
{
    public partial class PanelPacientes : UserControl
    {
        private Paciente? pacienteSeleccionadoGlobal;

        public PanelPacientes()
        {
            InitializeComponent();
            RefrescarListaPacientes();
        }

        private void gridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pacienteSeleccionadoGlobal = (Paciente)gridPacientes.SelectedItem ?? gridPacientes.Items.OfType<Paciente>().FirstOrDefault() ;
                     
            if (pacienteSeleccionadoGlobal is null)
            {
                // No hay nada seleccionado: 
                return;
            }

            // Recuperar de base datos datos actualizados del paciente seleccionado
            using (VitaltrackContext db = new VitaltrackContext())
            {
                Paciente paciente = db.Pacientes.Find(pacienteSeleccionadoGlobal.PacienteId);
                txtPacienteID.Text                     = paciente.PacienteId.ToString();
                txtActivo.Text                         = paciente.Activo.HasValue ? (paciente.Activo.Value ? "Sí" : "No") : "Desconocido";
                txtNombrePaciente.Text                 = paciente.Nombre;
                txtApellidosPaciente.Text              = paciente.Apellidos;
                txtDNIPaciente.Text                    = paciente.Dni ?? "Desconocido";
                txtDireccionPaciente.Text              = paciente.Direccion ?? "Desconocido";
                txtObservacionesPaciente.Text          = paciente.Observaciones ?? "Ninguna";
                txtTelefonoPaciente.Text               = paciente.Telefono ?? "Desconocido";
                txtEmailPaciente.Text                  = paciente.Email ?? "Desconocido";
                txtFechaNacimientoPaciente.Text        = paciente.FechaNacimiento.HasValue ? paciente.FechaNacimiento.Value.ToString("d") : "Desconocido";
                txtSexoPaciente.Text                   = paciente.Sexo ?? "Desconocido";
                txtCreadorPaciente.Text                = paciente.CreadoPor.HasValue ? (paciente.Nombre + " " + paciente.Apellidos) : "Desconocido";
                txtAltaEnSistemaPaciente.Text          = paciente.CreadoEn.ToString();
                txtActualizacionPaciente.Text          = paciente.ActualizadoEn.ToString();
            }
        }   
    
        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            Paciente paciente = (Paciente)gridPacientes.SelectedItem;
            if (paciente is null)
            {
                MessageBox.Show("No hay ningún paciente seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas dar de baja a este paciente?", "Confirmar baja", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (VitaltrackContext db = new VitaltrackContext())
                {
                    Paciente objetoPaciente = db.Pacientes.Find(paciente.PacienteId);
                    objetoPaciente.Activo   = false;

                    db.SaveChanges();
                }

                // Limpiar los detalles del usuario mostrado
                txtPacienteID.Text              = "";
                txtActivo.Text                  = "";
                txtNombrePaciente.Text          = "";
                txtApellidosPaciente.Text       = "";
                txtDNIPaciente.Text             = "";
                txtSexoPaciente.Text            = "";
                txtTelefonoPaciente.Text        = "";
                txtEmailPaciente.Text           = "";
                txtDireccionPaciente.Text       = "";
                txtObservacionesPaciente.Text   = "";
                txtCreadorPaciente.Text         = "";
                txtFechaNacimientoPaciente.Text = "";
                txtAltaEnSistemaPaciente.Text   = "";
                txtActualizacionPaciente.Text   = "";

                // Refrescar la lista de usuarios
                pacienteSeleccionadoGlobal = null;
                RefrescarListaPacientes();
            }
        }

        private void RefrescarListaPacientes(Paciente? pacienteSeleccionado = null)
        {
            List<Paciente> pacientes;
            using (VitaltrackContext db = new VitaltrackContext())
            {
               pacientes = db.Pacientes.Where(x => x.Activo == true).ToList();
            }
            
            gridPacientes.ItemsSource = pacientes;

            if (pacienteSeleccionado != null)
            {
                gridPacientes.SelectedItem = pacientes.FirstOrDefault(p => p.PacienteId == pacienteSeleccionado.PacienteId);
            }
            else
            {
                gridPacientes.SelectedItem = pacientes.Count > 0 ? pacientes[0] : null;
            }
        }
    }
}
