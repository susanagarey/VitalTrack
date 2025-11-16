using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTrack.ViewModels
{
    class UsuarioAuditoriaViewModel
    {
        public ulong AuditoriaId { get; set; }

        public string Accion { get; set; } = null!;

        public string? Detalles { get; set; }

        public DateTime CreadoEn { get; set; }

        public uint UsuarioId { get; set; }

        public bool? Activo { get; set; }

        public string? NombreUsuario { get; set; } = null!;

        public string? Nombre { get; set; } = null!;

        public string? Apellidos { get; set; } = null!;

        public string? Foto { get; set; }

        public string NombreRol { get; set; } = null!;
    }
}
