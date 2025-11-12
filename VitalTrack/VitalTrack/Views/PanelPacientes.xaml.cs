using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private Paciente? pacienteGlobal;

        private void gridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var usuarioSeleccionado = gridPacientes.SelectedItem as Paciente;
            if (usuarioSeleccionado is not null)
            {
                pacienteGlobal = usuarioSeleccionado;
            }

            var paciente = usuarioSeleccionado ?? pacienteGlobal;
            if (paciente is null)
            {
                // No hay nada seleccionado: 
                return;
            }

            Usuario? usuario;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                usuario = db.Usuarios
                            .FirstOrDefault(u => u.UsuarioId == paciente.CreadoPor);
            }

            txtPacienteID.Text = paciente.PacienteId.ToString();
            txtActivo.Text = paciente.Activo.HasValue ? (paciente.Activo.Value ? "Sí" : "No") : "Desconocido";
            txtNombrePaciente.Text = paciente.Nombre;
            txtApellidosPaciente.Text = paciente.Apellidos;
            txtDNIPaciente.Text = paciente.Dni ?? "Desconocido";
            txtDireccionPaciente.Text = paciente.Direccion ?? "Desconocido";
            txtObservacionesPaciente.Text = paciente.Observaciones ?? "Ninguna";
            txtTelefonoPaciente.Text = paciente.Telefono ?? "Desconocido";
            txtEmailPaciente.Text = paciente.Email ?? "Desconocido";
            dpFechaNacimientoPaciente.SelectedDate = paciente.FechaNacimiento.HasValue
                ? new DateTime(paciente.FechaNacimiento.Value.Year, paciente.FechaNacimiento.Value.Month, paciente.FechaNacimiento.Value.Day)
                : null;
            txtSexoPaciente.Text = paciente.Sexo ?? "Desconocido";
            txtCreadorPaciente.Text = paciente.CreadoPor.HasValue ? (usuario.Nombre + " " + usuario.Apellidos) : "Desconocido";
            dpAltaEnSistemaPaciente.SelectedDate = paciente.CreadoEn;
            dpActualizacionPaciente.SelectedDate = paciente.ActualizadoEn;
        }
    }
}
