using System;
using System.Collections.Generic;

namespace VitalTrack.Models;

public partial class Usuario
{
    public uint UsuarioId { get; set; }

    public bool? Activo { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Email { get; set; } = null!;

    public string? Foto { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime ActualizadoEn { get; set; }

    public virtual ICollection<Alerta> Alerta { get; set; } = new List<Alerta>();

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual ICollection<Paciente> PacienteActualizadoPorNavigations { get; set; } = new List<Paciente>();

    public virtual ICollection<Paciente> PacienteCreadoPorNavigations { get; set; } = new List<Paciente>();

    public virtual ICollection<UmbralesPaciente> UmbralesPacienteActualizadoPorNavigations { get; set; } = new List<UmbralesPaciente>();

    public virtual ICollection<UmbralesPaciente> UmbralesPacienteCreadoPorNavigations { get; set; } = new List<UmbralesPaciente>();

    public virtual ICollection<UsuariosRole> UsuariosRoleAsignadoPorNavigations { get; set; } = new List<UsuariosRole>();

    public virtual ICollection<UsuariosRole> UsuariosRoleUsuarios { get; set; } = new List<UsuariosRole>();
}
