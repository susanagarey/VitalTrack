using System;
using System.Collections.Generic;

namespace VitalTrack.Models;

public partial class Auditorium
{
    public ulong AuditoriaId { get; set; }

    public uint? UsuarioId { get; set; }

    public string Accion { get; set; } = null!;

    public string? Detalles { get; set; }

    public DateTime CreadoEn { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
