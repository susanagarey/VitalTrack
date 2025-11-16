using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
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

namespace VitalTrack
{
    /// <summary>
    /// Interaction logic for InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombreUsuario.Text.Trim() == "" || txtContrasena.Password == "")
            {
                MessageBox.Show("Por favor, introduzca nombre de usuario y contraseña");
            }
            else
            {
                Usuario? usuario;
                using (VitaltrackContext db = new VitaltrackContext())
                {
                    usuario = db.Usuarios.
                                 FirstOrDefault(x => x.NombreUsuario == txtNombreUsuario.Text.Trim() && x.HashPassword.Equals(CalcularHashSha256(txtContrasena.Password)));
                }

                if (usuario != null)
                {
                    // Recuperar rol
                    UsuarioActual.UsuarioId = usuario.UsuarioId;
                    using (VitaltrackContext db = new VitaltrackContext())
                    {
                        var usuarioRol = db.UsuariosRoles
                                           .Where(x => x.UsuarioId == usuario.UsuarioId)
                                           .FirstOrDefault();

                        UsuarioActual.RolId = usuarioRol?.RolId;
                    }

                    UsuarioActual.NombreUsuario = usuario.NombreUsuario;
                    UsuarioActual.Nombre        = usuario.Nombre;
                    UsuarioActual.Apellidos     = usuario.Apellidos;

                    if (UsuarioActual.RolId == null)
                    {
                        MessageBox.Show("El usuario no tiene un rol asignado. Contacte con el administrador.");
                        return;
                    }
                    else
                    {
                        // Registrar inicio de sesión en la tabla de auditoría
                        var ip = ObtenerIpLocalV4() ?? "DESCONOCIDA";
                        using (VitaltrackContext db = new VitaltrackContext())
                        {

                            Auditorium nuevoRegistroAuditoria = new Auditorium
                            {
                                UsuarioId = UsuarioActual.UsuarioId,
                                Accion    = "LOGIN",
                                Detalles  = $"{{\"ip\":\"{ip}\"}}",
                            };
                            db.Auditoria.Add(nuevoRegistroAuditoria);
                            db.SaveChanges();
                        }
                        // Pasar a pantalla principal
                        this.Visibility = Visibility.Collapsed;
                        MainWindow ventanaPrincipal = new MainWindow();
                        ventanaPrincipal.ShowDialog();

                        // De vuelta al cuado de inicio de sesión:
                        if (ventanaPrincipal.solicitadoFinalApp)
                        {
                            Application.Current.Shutdown();
                            return;
                        }

                        txtContrasena.Clear();
                        txtNombreUsuario.Clear();
                        this.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("Credenciales no válidas");
                }
            }
        }

        private static string CalcularHashSha256(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;
            using var sha = SHA256.Create();
            byte[] bytes  = Encoding.UTF8.GetBytes(texto);
            byte[] hash   = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        private static string? ObtenerIpLocalV4()
        {
            try
            {
                foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus != OperationalStatus.Up) continue;
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                        ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel) continue;

                    var props = ni.GetIPProperties();
                    foreach (var ua in props.UnicastAddresses)
                    {
                        if (ua.Address.AddressFamily == AddressFamily.InterNetwork &&
                            !IPAddress.IsLoopback(ua.Address))
                        {
                            return ua.Address.ToString();
                        }
                    }
                }

                foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip))
                        return ip.ToString();
                }
            }
            catch
            {
                // Ignorar y devolver null
            }

            return null;
        }
    }
}
