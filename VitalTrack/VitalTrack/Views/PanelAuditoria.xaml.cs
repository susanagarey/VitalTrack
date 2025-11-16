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
using Microsoft.EntityFrameworkCore;
using VitalTrack.Data;
using VitalTrack.Models;
using VitalTrack.ViewModels;

namespace VitalTrack.Views
{
    /// <summary>
    /// Interaction logic for PanelAuditoria.xaml
    /// </summary>
    public partial class PanelAuditoria : UserControl
    {
        UsuarioAuditoriaViewModel? registroActual;
        public PanelAuditoria()
        {
            InitializeComponent();
            RefrescarListaRegistrosAuditoria();
        }

        private void RefrescarListaRegistrosAuditoria()
        {
            using var db = new VitaltrackContext();

            var registrosAuditoria = db.Auditoria.
                Include(a => a.Usuario)
                .Select(a => new UsuarioAuditoriaViewModel
                {
                    AuditoriaId   = a.AuditoriaId,
                    Accion        = a.Accion,
                    Detalles      = a.Detalles,
                    CreadoEn      = a.CreadoEn,
                    UsuarioId     = a.UsuarioId ?? 0,
                    Activo        = a.Usuario.Activo ?? false,
                    NombreUsuario = a.Usuario.NombreUsuario ?? string.Empty,
                    Nombre        = a.Usuario.Nombre ?? string.Empty,
                    Apellidos     = a.Usuario.Apellidos ?? string.Empty,
                    Foto          = a.Usuario.Foto ?? App.FotoPorDefecto,
                    NombreRol     = db.UsuariosRoles
                                      .Where(ur => ur.UsuarioId == a.UsuarioId)
                                      .Join(db.Roles,
                                            ur => ur.RolId,
                                            r => r.RolId,
                                            (ur, r) => r.Nombre)
                                      .FirstOrDefault() ?? "Sin Rol"
                })
                .ToList();

            if (registroActual == null && registrosAuditoria.Count > 0)
            {
                registroActual = registrosAuditoria[0];
            }

            gridRegistros.ItemsSource = registrosAuditoria;
        }

        private void gridRegistros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsuarioAuditoriaViewModel seleccionado = gridRegistros.SelectedItem as UsuarioAuditoriaViewModel;

            txtJSON.Text = seleccionado.Detalles ?? string.Empty;

            string trayectoriaFoto = App.CarpetaFotos + (seleccionado.Foto ?? App.FotoPorDefecto);

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(trayectoriaFoto, UriKind.Relative);
            bitmap.EndInit();
            fotoUsuario.Source = bitmap;
        }
    }
}
