using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using VitalTrack.Models;

namespace VitalTrack.Data;

public partial class VitalTrackDbContext : DbContext
{
    public VitalTrackDbContext()
    {
    }

    public VitalTrackDbContext(DbContextOptions<VitalTrackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alerta> Alertas { get; set; }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Constante> Constantes { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UmbralesPaciente> UmbralesPacientes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=VitalTrackDB;user id=root;password=Abc123.", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.43-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_spanish_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alerta>(entity =>
        {
            entity.HasKey(e => e.AlertaId).HasName("PRIMARY");

            entity.ToTable("alertas");

            entity.HasIndex(e => e.ConstanteId, "fk_alertas_constante");

            entity.HasIndex(e => e.ReconocidaPor, "fk_alertas_reconocida_por");

            entity.HasIndex(e => new { e.PacienteId, e.DisparadaEn }, "idx_alertas_paciente_tiempo").IsDescending(false, true);

            entity.HasIndex(e => new { e.Reconocida, e.ReconocidaEn }, "idx_alertas_reconocida");

            entity.Property(e => e.AlertaId).HasColumnName("alerta_id");
            entity.Property(e => e.ActualizadoEn)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ConstanteId).HasColumnName("constante_id");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.DisparadaEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("disparada_en");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(255)
                .HasColumnName("mensaje");
            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");
            entity.Property(e => e.Reconocida).HasColumnName("reconocida");
            entity.Property(e => e.ReconocidaEn)
                .HasColumnType("datetime")
                .HasColumnName("reconocida_en");
            entity.Property(e => e.ReconocidaPor).HasColumnName("reconocida_por");
            entity.Property(e => e.Severidad)
                .HasColumnType("enum('BAJA','MEDIA','ALTA','CRITICA')")
                .HasColumnName("severidad");
            entity.Property(e => e.TipoAlerta)
                .HasColumnType("enum('TA','FC','SPO2','TEMP','GLUCOSA','GENERAL')")
                .HasColumnName("tipo_alerta");

            entity.HasOne(d => d.Constante).WithMany(p => p.Alerta)
                .HasForeignKey(d => d.ConstanteId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_alertas_constante");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Alerta)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("fk_alertas_paciente");

            entity.HasOne(d => d.ReconocidaPorNavigation).WithMany(p => p.Alerta)
                .HasForeignKey(d => d.ReconocidaPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_alertas_reconocida_por");
        });

        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.AuditoriaId).HasName("PRIMARY");

            entity.ToTable("auditoria");

            entity.HasIndex(e => e.UsuarioId, "fk_auditoria_usuario");

            entity.Property(e => e.AuditoriaId).HasColumnName("auditoria_id");
            entity.Property(e => e.Accion)
                .HasColumnType("enum('CREAR','ACTUALIZAR','ELIMINAR','LOGIN','LOGOUT','ASIGNAR_ROL')")
                .HasColumnName("accion");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Detalles)
                .HasColumnType("json")
                .HasColumnName("detalles");
            entity.Property(e => e.Entidad)
                .HasMaxLength(100)
                .HasColumnName("entidad");
            entity.Property(e => e.EntidadId).HasColumnName("entidad_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_auditoria_usuario");
        });

        modelBuilder.Entity<Constante>(entity =>
        {
            entity.HasKey(e => e.ConstanteId).HasName("PRIMARY");

            entity.ToTable("constantes");

            entity.HasIndex(e => e.FrecuenciaCardiaca, "idx_constantes_fc");

            entity.HasIndex(e => e.GlucosaMgdl, "idx_constantes_glucosa");

            entity.HasIndex(e => new { e.PacienteId, e.MedidoEn }, "idx_constantes_paciente_fecha").IsDescending(false, true);

            entity.HasIndex(e => e.RegistradoPor, "idx_constantes_registrado_por");

            entity.HasIndex(e => e.Spo2, "idx_constantes_spo2");

            entity.HasIndex(e => new { e.TensionSistolica, e.TensionDiastolica }, "idx_constantes_ta");

            entity.HasIndex(e => e.TemperaturaC, "idx_constantes_temp");

            entity.Property(e => e.ConstanteId).HasColumnName("constante_id");
            entity.Property(e => e.ActualizadoEn)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.FrecuenciaCardiaca).HasColumnName("frecuencia_cardiaca");
            entity.Property(e => e.GlucosaMgdl).HasColumnName("glucosa_mgdl");
            entity.Property(e => e.MedidoEn)
                .HasColumnType("datetime")
                .HasColumnName("medido_en");
            entity.Property(e => e.NotasMedicion)
                .HasMaxLength(255)
                .HasColumnName("notas_medicion");
            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");
            entity.Property(e => e.Spo2).HasColumnName("spo2");
            entity.Property(e => e.TemperaturaC)
                .HasPrecision(4, 1)
                .HasColumnName("temperatura_c");
            entity.Property(e => e.TensionDiastolica).HasColumnName("tension_diastolica");
            entity.Property(e => e.TensionSistolica).HasColumnName("tension_sistolica");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Constantes)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("fk_constantes_paciente");

            entity.HasOne(d => d.RegistradoPorNavigation).WithMany(p => p.Constantes)
                .HasForeignKey(d => d.RegistradoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_constantes_registrado_por");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.PacienteId).HasName("PRIMARY");

            entity.ToTable("pacientes");

            entity.HasIndex(e => e.Dni, "dni").IsUnique();

            entity.HasIndex(e => e.ActualizadoPor, "fk_pacientes_actualizado_por");

            entity.HasIndex(e => e.CreadoPor, "fk_pacientes_creado_por");

            entity.HasIndex(e => e.Activo, "idx_pacientes_activo");

            entity.HasIndex(e => new { e.Apellidos, e.Nombre }, "idx_pacientes_nombre");

            entity.HasIndex(e => e.Nhc, "nhc").IsUnique();

            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor).HasColumnName("actualizado_por");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(150)
                .HasColumnName("apellidos");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nhc)
                .HasMaxLength(50)
                .HasColumnName("nhc");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Observaciones)
                .HasColumnType("text")
                .HasColumnName("observaciones");
            entity.Property(e => e.Sexo)
                .HasColumnType("enum('M','F','X','N/D')")
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");

            entity.HasOne(d => d.ActualizadoPorNavigation).WithMany(p => p.PacienteActualizadoPorNavigations)
                .HasForeignKey(d => d.ActualizadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_pacientes_actualizado_por");

            entity.HasOne(d => d.CreadoPorNavigation).WithMany(p => p.PacienteCreadoPorNavigations)
                .HasForeignKey(d => d.CreadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_pacientes_creado_por");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.ActualizadoEn)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UmbralesPaciente>(entity =>
        {
            entity.HasKey(e => e.UmbralId).HasName("PRIMARY");

            entity.ToTable("umbrales_paciente");

            entity.HasIndex(e => e.ActualizadoPor, "fk_umbrales_paciente_actualizado_por");

            entity.HasIndex(e => e.CreadoPor, "fk_umbrales_paciente_creado_por");

            entity.HasIndex(e => new { e.PacienteId, e.Activo }, "idx_umbrales_paciente_activo");

            entity.Property(e => e.UmbralId).HasColumnName("umbral_id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor).HasColumnName("actualizado_por");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.MaxFrecuenciaCardiaca).HasColumnName("max_frecuencia_cardiaca");
            entity.Property(e => e.MaxGlucosa).HasColumnName("max_glucosa");
            entity.Property(e => e.MaxSpo2).HasColumnName("max_spo2");
            entity.Property(e => e.MaxTemperaturaC)
                .HasPrecision(4, 1)
                .HasColumnName("max_temperatura_c");
            entity.Property(e => e.MaxTensionDiastolica).HasColumnName("max_tension_diastolica");
            entity.Property(e => e.MaxTensionSistolica).HasColumnName("max_tension_sistolica");
            entity.Property(e => e.MinFrecuenciaCardiaca).HasColumnName("min_frecuencia_cardiaca");
            entity.Property(e => e.MinGlucosa).HasColumnName("min_glucosa");
            entity.Property(e => e.MinSpo2).HasColumnName("min_spo2");
            entity.Property(e => e.MinTemperaturaC)
                .HasPrecision(4, 1)
                .HasColumnName("min_temperatura_c");
            entity.Property(e => e.MinTensionDiastolica).HasColumnName("min_tension_diastolica");
            entity.Property(e => e.MinTensionSistolica).HasColumnName("min_tension_sistolica");
            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");

            entity.HasOne(d => d.ActualizadoPorNavigation).WithMany(p => p.UmbralesPacienteActualizadoPorNavigations)
                .HasForeignKey(d => d.ActualizadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_umbrales_paciente_actualizado_por");

            entity.HasOne(d => d.CreadoPorNavigation).WithMany(p => p.UmbralesPacienteCreadoPorNavigations)
                .HasForeignKey(d => d.CreadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_umbrales_paciente_creado_por");

            entity.HasOne(d => d.Paciente).WithMany(p => p.UmbralesPacientes)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("fk_umbrales_paciente_paciente");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "nombre_usuario").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(150)
                .HasColumnName("apellidos");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.HashPassword)
                .HasMaxLength(255)
                .HasColumnName("hash_password");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");
            entity.Property(e => e.UltimoAcceso)
                .HasColumnType("datetime")
                .HasColumnName("ultimo_acceso");
        });

        modelBuilder.Entity<UsuariosRole>(entity =>
        {
            entity.HasKey(e => new { e.UsuarioId, e.RolId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("usuarios_roles");

            entity.HasIndex(e => e.AsignadoPor, "fk_usuarios_roles_asignado_por");

            entity.HasIndex(e => e.RolId, "fk_usuarios_roles_rol");

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.AsignadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("asignado_en");
            entity.Property(e => e.AsignadoPor).HasColumnName("asignado_por");

            entity.HasOne(d => d.AsignadoPorNavigation).WithMany(p => p.UsuariosRoleAsignadoPorNavigations)
                .HasForeignKey(d => d.AsignadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_usuarios_roles_asignado_por");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuarios_roles_rol");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosRoleUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("fk_usuarios_roles_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
