using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTrack.ViewModels
{
    public class UsuarioAutenticacionViewModel
    {
        public int UsuarioId { get; set; }
        public  int RolId { get; set; }
        public  string? NombreUsuario { get; set; }

        public string? Nombre { get; set; }
        public  string? Apellidos { get; set; }
    }
}
