using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VitalTrack.Data;
using VitalTrack.Models;
using VitalTrack.ViewModels;

namespace VitalTrack.Views
{
    public partial class PanelPacientes : UserControl
    {
        private PacienteAmpliadoViewModel? pacienteSeleccionadoGlobal;

        public PanelPacientes()
        {
            InitializeComponent();

            if ( UsuarioActual.RolId == Constantes.Roles.PACIENTE )
            {
                btnAlta.IsEnabled        = false;
                btnBaja.IsEnabled        = false;
                lblObservaciones.Visibility = Visibility.Hidden;
                txtObservacionesPaciente.Visibility = Visibility.Hidden;
            }

            RefrescarListaPacientes();
        }

        private void gridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pacienteSeleccionadoGlobal = (PacienteAmpliadoViewModel)gridPacientes.SelectedItem ?? gridPacientes.Items.OfType<PacienteAmpliadoViewModel>().FirstOrDefault() ;
                     
            if (pacienteSeleccionadoGlobal is null)
            {
                // No hay nada seleccionado: 
                return;
            }

            // Recuperar de base datos datos actualizados del paciente seleccionado
            PacienteAmpliadoViewModel paciente = GenerarListaPacienteAmpliado().FirstOrDefault(p => p.PacienteId == pacienteSeleccionadoGlobal.PacienteId);                           

            txtPacienteID.Text              = paciente.PacienteId.ToString();
            txtActivo.Text                  = paciente.Activo.HasValue ? (paciente.Activo.Value ? "Sí" : "No") : "Desconocido";
            txtNombrePaciente.Text          = paciente.Nombre;
            txtApellidosPaciente.Text       = paciente.Apellidos;
            txtDNIPaciente.Text             = paciente.Dni ?? "Desconocido";
            txtSeguro.Text                  = paciente.Nhc ?? "Desconocido";
            txtDireccionPaciente.Text       = paciente.Direccion ?? "Desconocido";
            txtObservacionesPaciente.Text   = paciente.Observaciones ?? "Ninguna";
            txtTelefonoPaciente.Text        = paciente.Telefono ?? "Desconocido";
            txtEmailPaciente.Text           = paciente.Email ?? "Desconocido";
            txtFechaNacimientoPaciente.Text = paciente.FechaNacimiento.HasValue ? paciente.FechaNacimiento.Value.ToString("d") : "Desconocido";
            txtSexoPaciente.Text            = descipcionSexo(paciente.Sexo);
            txtCreadorPaciente.Text         = paciente.NombreCreadoPor ?? "Desconocido";
            txtAltaEnSistemaPaciente.Text   = paciente.CreadoEn.ToString();
            txtActualizadorPaciente.Text    = paciente.NombreActualizadoPor ?? "Desconocido";
            txtActualizacionPaciente.Text   = paciente.ActualizadoEn.ToString();
        }


        private void btnAlta_Click(object sender, RoutedEventArgs e)
        {
            pacienteSeleccionadoGlobal = (PacienteAmpliadoViewModel)gridPacientes.SelectedItem;

            if ( pacienteSeleccionadoGlobal is null)
            {
                MessageBox.Show("No hay ningún paciente seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            VentanaPacientes ventanaPaciente = new VentanaPacientes();
            ventanaPaciente.ShowDialog();

            pacienteSeleccionadoGlobal = ventanaPaciente.pacienteAlta ?? pacienteSeleccionadoGlobal;

            RefrescarListaPacientes(pacienteSeleccionadoGlobal);
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            PacienteAmpliadoViewModel paciente = (PacienteAmpliadoViewModel)gridPacientes.SelectedItem;

            if (paciente is null)
            {
                MessageBox.Show("No hay ningún paciente seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            VentanaPacientes ventanaPaciente = new VentanaPacientes();
            ventanaPaciente.pacienteActualizar = paciente;
            ventanaPaciente.ShowDialog();

            pacienteSeleccionadoGlobal = ventanaPaciente.pacienteAlta ?? pacienteSeleccionadoGlobal;

            RefrescarListaPacientes(pacienteSeleccionadoGlobal);
        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            PacienteAmpliadoViewModel paciente = (PacienteAmpliadoViewModel)gridPacientes.SelectedItem;
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


        // Métodos auxiliares

        private string descipcionSexo(string? sexo)
        {
            return sexo switch
            {
                Constantes.Sexos.Masculino => Constantes.NombreSexos.Masculino,
                Constantes.Sexos.Femenino => Constantes.NombreSexos.Femenino,
                Constantes.Sexos.Trans => Constantes.NombreSexos.Trans,
                Constantes.Sexos.NoDeclarado => Constantes.NombreSexos.NoDeclarado,
                _   => "Desconocido",
            };
        }
        private void RefrescarListaPacientes(PacienteAmpliadoViewModel? pacienteSeleccionado = null)
        {
            List<PacienteAmpliadoViewModel> pacientes = GenerarListaPacienteAmpliado();

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

        private static List<PacienteAmpliadoViewModel> GenerarListaPacienteAmpliado()
        {
            List<PacienteAmpliadoViewModel> pacientes;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                pacientes = db.Pacientes.
                               Where(x => x.Activo == true).
                               Select(x => new PacienteAmpliadoViewModel
                               {
                                   PacienteId           = x.PacienteId,
                                   Nhc                  = x.Nhc,
                                   Dni                  = x.Dni,
                                   Nombre               = x.Nombre,
                                   Apellidos            = x.Apellidos,
                                   FechaNacimiento      = x.FechaNacimiento,
                                   Sexo                 = x.Sexo,
                                   Telefono             = x.Telefono,
                                   Email                = x.Email,
                                   Direccion            = x.Direccion,
                                   Observaciones        = x.Observaciones,
                                   Activo               = x.Activo,
                                   CreadoEn             = x.CreadoEn,
                                   ActualizadoEn        = x.ActualizadoEn,
                                   CreadoPor            = x.CreadoPor,
                                   NombreCreadoPor      = x.CreadoPorNavigation != null ? (x.CreadoPorNavigation.Nombre + " " + x.CreadoPorNavigation.Apellidos) : "Desconocido",
                                   ActualizadoPor       = x.ActualizadoPor,
                                   NombreActualizadoPor = x.ActualizadoPorNavigation != null ? (x.ActualizadoPorNavigation.Nombre + " " + x.ActualizadoPorNavigation.Apellidos) : "Desconocido",
                               }).
                               ToList();
            }

            if (UsuarioActual.RolId == Constantes.Roles.PACIENTE)
            {
                uint? pacienteIdUsuario;

                using (VitaltrackContext db = new VitaltrackContext())
                {
                    Usuario? usuarioAutenticado = db.Usuarios.
                                               Where(u => u.UsuarioId == UsuarioActual.UsuarioId).
                                               FirstOrDefault();

                    pacienteIdUsuario = db.Pacientes.
                                           Where(u => u.Nombre.Equals(usuarioAutenticado.Nombre) && u.Apellidos.Equals(usuarioAutenticado.Apellidos)).
                                           Select(u => u.PacienteId).
                                           FirstOrDefault();

                    pacientes = pacientes.Where(p => p.PacienteId == pacienteIdUsuario).ToList();
                }
            }

            return pacientes;
        }
    }
}
