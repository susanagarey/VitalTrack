using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTrack.ViewModels
{
    public class AlertasViewModel
    {
        public ulong AlertaId { get; set; }

        public uint PacienteId { get; set; }

        public string TipoAlerta { get; set; } = null!;

        public string Severidad { get; set; } = null!;

        public string Mensaje { get; set; } = null!;

        public DateTime DisparadaEn { get; set; }

        public bool Reconocida { get; set; }

        public DateTime? ReconocidaEn { get; set; }

        public uint? ReconocidaPor { get; set; }

        public DateTime CreadoEn { get; set; }

        public DateTime ActualizadoEn { get; set; }

        public string NombreCompletoPaciente { get; set; } = null!;

        public string NombreCompletoUsuario { get; set; } = null!;
    }
}
