using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTrack.ViewModels
{
    public class PacientesRolesViewModel
    {
        public uint PacienteId { get; set; }

        public string? Dni { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public uint RolId { get; set; }

        public string NombreRol { get; set; } = null!;
    }
}
