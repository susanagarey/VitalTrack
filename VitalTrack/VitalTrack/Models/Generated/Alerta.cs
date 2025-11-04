using System;
using System.Collections.Generic;

namespace VitalTrack.Models;

public partial class Alerta
{
    public ulong AlertaId { get; set; }

    public uint PacienteId { get; set; }

    public ulong? ConstanteId { get; set; }

    public string TipoAlerta { get; set; } = null!;

    public string Severidad { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public DateTime DisparadaEn { get; set; }

    public bool Reconocida { get; set; }

    public DateTime? ReconocidaEn { get; set; }

    public uint? ReconocidaPor { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime ActualizadoEn { get; set; }

    public virtual Constante? Constante { get; set; }

    public virtual Paciente Paciente { get; set; } = null!;

    public virtual Usuario? ReconocidaPorNavigation { get; set; }
}
