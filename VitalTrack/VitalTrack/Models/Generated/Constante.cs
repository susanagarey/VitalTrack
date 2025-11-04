using System;
using System.Collections.Generic;

namespace VitalTrack.Models;

public partial class Constante
{
    public ulong ConstanteId { get; set; }

    public uint PacienteId { get; set; }

    public DateTime MedidoEn { get; set; }

    public short? TensionSistolica { get; set; }

    public short? TensionDiastolica { get; set; }

    public short? FrecuenciaCardiaca { get; set; }

    public byte? Spo2 { get; set; }

    public decimal? TemperaturaC { get; set; }

    public ushort? GlucosaMgdl { get; set; }

    public string? NotasMedicion { get; set; }

    public uint? RegistradoPor { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime ActualizadoEn { get; set; }

    public virtual ICollection<Alerta> Alerta { get; set; } = new List<Alerta>();

    public virtual Paciente Paciente { get; set; } = null!;

    public virtual Usuario? RegistradoPorNavigation { get; set; }
}
