using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using VitalTrack.Data;
using VitalTrack.Models;
using VitalTrack.ViewModels;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static VitalTrack.Constantes;

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
                // Ocultar elementos UI no necesarios en alta
                txtPacienteID.Visibility = Visibility.Visible;
                lblPacienteID.Visibility = Visibility.Visible;
                txtActivo.Visibility     = Visibility.Visible;
                lblTxtActivo.Visibility  = Visibility.Visible;

                // Rellenar campos con datos del paciente a actualizar
                txtPacienteID.Text            = pacienteActualizar.PacienteId.ToString();
                txtActivo.Text                = pacienteActualizar.Activo.HasValue ? (pacienteActualizar.Activo.Value ? "Sí" : "No") : "Desconocido";  
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
                txtActualizadorPaciente.Text  = UsuarioActual.Nombre + " " + UsuarioActual.Apellidos;
            }
            else
            {
                // Ocultar elementos UI no necesarios en alta
                txtPacienteID.Visibility           = Visibility.Collapsed;
                lblPacienteID.Visibility           = Visibility.Collapsed;
                txtActivo.Visibility               = Visibility.Collapsed;
                lblTxtActivo.Visibility            = Visibility.Collapsed;

                // Rellenar campos de creador/actualizador con usuario actual
                txtCreadorPaciente.Text      = UsuarioActual.Nombre + " " + UsuarioActual.Apellidos;
                txtActualizadorPaciente.Text = UsuarioActual.Nombre + " " + UsuarioActual.Apellidos;

            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool exito = false;

            if (pacienteActualizar == null)
            {
                exito = CrearPaciente();
            }
            else
            {
                exito = ActualizarPaciente();
            }

            if (exito)
            {
                // Borrar campos, tras éxito en operación:
                
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CrearPaciente()
        {
            if ( ValidarCreacionPaciente() )
            {
                // Preparar objeto Paciente
                Paciente paciente = new Paciente();
                paciente.Nhc            = txtSeguro.Text.Trim();
                paciente.Dni            = txtDNIPaciente.Text.Trim();
                paciente.Nombre         = txtNombrePaciente.Text.Trim();
                paciente.Apellidos      = txtApellidosPaciente.Text.Trim();
                paciente.Telefono       = txtTelefonoPaciente.Text.Trim();
                paciente.Email          = txtEmailPaciente.Text.Trim();
                paciente.Direccion      = txtDireccionPaciente.Text.Trim();
                paciente.Observaciones  = txtObservacionesPaciente.Text.Trim();
                paciente.Activo         = true;
                paciente.CreadoPor      = UsuarioActual.UsuarioId;
                paciente.ActualizadoPor = UsuarioActual.UsuarioId;

                paciente.FechaNacimiento = dpFechaNacimientoPaciente.SelectedDate.HasValue ?
                                          DateOnly.FromDateTime(dpFechaNacimientoPaciente.SelectedDate.Value) :
                                          null;
                paciente.Sexo = rbHombre.IsChecked == true ? Constantes.Sexos.Masculino :
                                rbMujer.IsChecked  == true ? Constantes.Sexos.Femenino :
                                rbTrans.IsChecked  == true ? Constantes.Sexos.Trans : 
                                                            Constantes.Sexos.NoDeclarado;
                          
                using (VitaltrackContext db = new VitaltrackContext())
                {
                    db.Pacientes.Add(paciente);
                    db.SaveChanges();
                }

                pacienteAlta = new PacienteAmpliadoViewModel { 
                                PacienteId           = paciente.PacienteId,
                                Nhc                  = paciente.Nhc,
                                Dni                  = paciente.Dni,
                                Nombre               = paciente.Nombre,
                                Apellidos            = paciente.Apellidos,
                                FechaNacimiento      = paciente.FechaNacimiento,
                                Sexo                 = paciente.Sexo,
                                Telefono             = paciente.Telefono,
                                Email                = paciente.Email,
                                Direccion            = paciente.Direccion,
                                Observaciones        = paciente.Observaciones,
                                Activo               = paciente.Activo,
                                CreadoEn             = paciente.CreadoEn,
                                ActualizadoEn        = paciente.ActualizadoEn,
                                CreadoPor            = UsuarioActual.UsuarioId,
                                ActualizadoPor       = UsuarioActual.UsuarioId,
                                NombreCreadoPor      = UsuarioActual.Nombre + " " + UsuarioActual.Apellidos,
                                NombreActualizadoPor = UsuarioActual.Nombre + " " + UsuarioActual.Apellidos
                };

                MessageBox.Show("Paciente añadido correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ActualizarPaciente()
        {
            if ( ValidarActualizarPaciente() )
            {
                Paciente? paciente;
                using (VitaltrackContext db = new VitaltrackContext())
                {
                    paciente = db.Pacientes.Find(pacienteActualizar.PacienteId);
                  
                    paciente.Nhc             = txtSeguro.Text.Trim();
                    paciente.Dni             = txtDNIPaciente.Text.Trim();
                    paciente.Nombre          = txtNombrePaciente.Text.Trim();
                    paciente.Apellidos       = txtApellidosPaciente.Text.Trim();
                    paciente.Telefono        = txtTelefonoPaciente.Text.Trim();
                    paciente.Email           = txtEmailPaciente.Text.Trim();
                    paciente.Direccion       = txtDireccionPaciente.Text.Trim();
                    paciente.Observaciones   = txtObservacionesPaciente.Text.Trim();
                    paciente.ActualizadoPor  = UsuarioActual.UsuarioId;
                    paciente.FechaNacimiento = dpFechaNacimientoPaciente.SelectedDate.HasValue ?
                                                DateOnly.FromDateTime(dpFechaNacimientoPaciente.SelectedDate.Value) :
                                                null;
                    paciente.Sexo = rbHombre.IsChecked == true ? Constantes.Sexos.Masculino :
                                    rbMujer.IsChecked == true ? Constantes.Sexos.Femenino :
                                    rbTrans.IsChecked == true ? Constantes.Sexos.Trans :
                                                        Constantes.Sexos.NoDeclarado;
                    db.SaveChanges();  
                }

                pacienteAlta = new PacienteAmpliadoViewModel  {
                    PacienteId = paciente.PacienteId,
                    Nhc = paciente.Nhc,
                    Dni = paciente.Dni,
                    Nombre = paciente.Nombre,
                    Apellidos = paciente.Apellidos,
                    FechaNacimiento = paciente.FechaNacimiento,
                    Sexo = paciente.Sexo,
                    Telefono = paciente.Telefono,
                    Email = paciente.Email,
                    Direccion = paciente.Direccion,
                    Observaciones = paciente.Observaciones,
                    Activo = paciente.Activo,
                    CreadoEn = paciente.CreadoEn,
                    ActualizadoEn = paciente.ActualizadoEn,
                    CreadoPor = paciente.CreadoPor,
                    ActualizadoPor = paciente.ActualizadoPor,
                    NombreCreadoPor = pacienteActualizar.NombreCreadoPor,
                    NombreActualizadoPor = UsuarioActual.Nombre + " " + UsuarioActual.Apellidos
                };
                pacienteActualizar = null;
                lbTitulo.Content = "Alta Paciente";
                this.Title = "VitalTrack - Alta paciente";
                MessageBox.Show("Paciente actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            else
            {
                 return false;
            }
        }
      

        private bool ValidarCreacionPaciente() {
            return true;
        }

        private bool ValidarActualizarPaciente() {
            return true;
        }
    }
}
