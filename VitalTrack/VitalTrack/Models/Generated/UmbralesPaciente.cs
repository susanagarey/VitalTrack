using System;
using System.Collections.Generic;

namespace VitalTrack.Models;

public partial class UmbralesPaciente
{
    public uint UmbralId { get; set; }

    public uint PacienteId { get; set; }

    public short? MinTensionSistolica { get; set; }

    public short? MaxTensionSistolica { get; set; }

    public short? MinTensionDiastolica { get; set; }

    public short? MaxTensionDiastolica { get; set; }

    public short? MinFrecuenciaCardiaca { get; set; }

    public short? MaxFrecuenciaCardiaca { get; set; }

    public byte? MinSpo2 { get; set; }

    public byte? MaxSpo2 { get; set; }

    public decimal? MinTemperaturaC { get; set; }

    public decimal? MaxTemperaturaC { get; set; }

    public ushort? MinGlucosa { get; set; }

    public ushort? MaxGlucosa { get; set; }

    public bool? Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime ActualizadoEn { get; set; }

    public uint? CreadoPor { get; set; }

    public uint? ActualizadoPor { get; set; }

    public virtual Usuario? ActualizadoPorNavigation { get; set; }

    public virtual Usuario? CreadoPorNavigation { get; set; }

    public virtual Paciente Paciente { get; set; } = null!;
}
