using System;
using System.Collections.Generic;

namespace VitalTrack.Models;

public partial class UsuariosRole
{
    public uint UsuarioId { get; set; }

    public uint RolId { get; set; }

    public DateTime AsignadoEn { get; set; }

    public uint? AsignadoPor { get; set; }

    public virtual Usuario? AsignadoPorNavigation { get; set; }

    public virtual Role Rol { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
