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
using VitalTrack.ViewModels;

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

            RefrescarListaRolesUsuarios();
        }

        private void btnAsignarRol_Click(object sender, RoutedEventArgs e)
        {
            UsuariosRolesViewModel usuarioSeleccionado = (UsuariosRolesViewModel)gridUsuarios.SelectedItem;

            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un usuario de la lista.", "Asignar rol", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbRoles.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un rol del desplegable.", "Asignar rol", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Eliminar rol anterior si existe
            using (VitaltrackContext db = new VitaltrackContext())
            {
                var usuarioRolExistente = db.UsuariosRoles
                    .FirstOrDefault(ur => ur.UsuarioId == usuarioSeleccionado.UsuarioId);
                if (usuarioRolExistente != null)
                {
                    db.UsuariosRoles.Remove(usuarioRolExistente);
                    db.SaveChanges();
                }
            }


            // Asignar rol al usuario
            using (VitaltrackContext db = new VitaltrackContext())
            {
                var usuarioRol = new UsuariosRole
                {
                    UsuarioId = usuarioSeleccionado.UsuarioId,
                    RolId = (uint)cmbRoles.SelectedValue
                };

                db.UsuariosRoles.Add(usuarioRol);
                db.SaveChanges();
            }

            RefrescarListaRolesUsuarios();
        }


        private void RefrescarListaRolesUsuarios()
        {
            using (VitaltrackContext db = new VitaltrackContext())
            {
                var listaRolesUsuarios =
                    (from u in db.Usuarios
                     join ur in db.UsuariosRoles
                         on u.UsuarioId equals ur.UsuarioId into urGroup
                     from ur in urGroup.DefaultIfEmpty()
                     select new UsuariosRolesViewModel
                     {
                         UsuarioId     = u.UsuarioId,
                         RolId         = ur != null ? ur.RolId : 0u,
                         NombreUsuario = u.NombreUsuario,
                         NombreRol     = ur != null ? ur.Rol.Nombre : "Sin rol",
                         Nombre        = u.Nombre,
                         Apellidos     = u.Apellidos
                     })
                    .ToList();

                gridUsuarios.ItemsSource = listaRolesUsuarios;
            }
        }
    }
}
