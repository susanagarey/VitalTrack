using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTrack
{
    public static class UsuarioActual
    {
        public static uint UsuarioId { get; set; }
        public static uint? RolId { get; set; }
        public static string? NombreUsuario { get; set; }

        public static string? Nombre { get; set; }
        public static string? Apellidos { get; set; }
    }
}
