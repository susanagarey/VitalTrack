using System;
using System.Collections.Generic;

namespace VitalTrack.Models;

public partial class Paciente
{
    public uint PacienteId { get; set; }

    public string? Nhc { get; set; }

    public string? Dni { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public string? Sexo { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public string? Observaciones { get; set; }

    public bool? Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime ActualizadoEn { get; set; }

    public uint? CreadoPor { get; set; }

    public uint? ActualizadoPor { get; set; }

    public virtual Usuario? ActualizadoPorNavigation { get; set; }

    public virtual ICollection<Alerta> Alerta { get; set; } = new List<Alerta>();

    public virtual Usuario? CreadoPorNavigation { get; set; }

    public virtual ICollection<UmbralesPaciente> UmbralesPacientes { get; set; } = new List<UmbralesPaciente>();
}
