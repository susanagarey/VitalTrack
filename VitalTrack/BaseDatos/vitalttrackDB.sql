-- Crear base de datos
CREATE DATABASE IF NOT EXISTS VitalTrackDB
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_spanish_ci;

USE VitalTrackDB;

-- Tabla de roles
CREATE TABLE roles (
  rol_id         INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  nombre         VARCHAR(50) NOT NULL UNIQUE,      -- 'Administrador', 'Medico', 'Enfermera', 'Paciente', ...
  descripcion    VARCHAR(255) NULL,
  creado_en      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  actualizado_en DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- Tabla de usuarios
CREATE TABLE usuarios (
  usuario_id     INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
  email          VARCHAR(120) NOT NULL UNIQUE,
  hash_password  VARCHAR(255) NOT NULL,            -- hash de contraseña (bcrypt/argon2)
  nombre         VARCHAR(100) NOT NULL,
  apellidos      VARCHAR(150) NOT NULL,
  telefono       VARCHAR(30) NULL,
  activo         TINYINT(1) NOT NULL DEFAULT 1,
  ultimo_acceso  DATETIME NULL,
  creado_en      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  actualizado_en DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- Relación muchos-a-muchos: usuarios <-> roles
CREATE TABLE usuarios_roles (
  usuario_id   INT UNSIGNED NOT NULL,
  rol_id       INT UNSIGNED NOT NULL,
  asignado_en  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  asignado_por INT UNSIGNED NULL,                  -- usuario que asigna el rol
  PRIMARY KEY (usuario_id, rol_id),
  CONSTRAINT fk_usuarios_roles_usuario
    FOREIGN KEY (usuario_id) REFERENCES usuarios(usuario_id)
    ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT fk_usuarios_roles_rol
    FOREIGN KEY (rol_id) REFERENCES roles(rol_id)
    ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT fk_usuarios_roles_asignado_por
    FOREIGN KEY (asignado_por) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL
) ENGINE=InnoDB;

-- Tabla de pacientes
CREATE TABLE pacientes (
  paciente_id    INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  nhc            VARCHAR(50) NULL UNIQUE,          -- Nº historia clínica (opcional)
  dni            VARCHAR(20) NULL UNIQUE,          -- Documento identificativo (opcional)
  nombre         VARCHAR(100) NOT NULL,
  apellidos      VARCHAR(150) NOT NULL,
  fecha_nacimiento DATE NULL,
  sexo           ENUM('M','F','X','N/D') NULL,     -- X no binario, N/D no determinado
  telefono       VARCHAR(30) NULL,
  email          VARCHAR(120) NULL,
  direccion      VARCHAR(255) NULL,
  observaciones  TEXT NULL,
  activo         TINYINT(1) NOT NULL DEFAULT 1,
  creado_en      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  actualizado_en DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  creado_por     INT UNSIGNED NULL,
  actualizado_por INT UNSIGNED NULL,
  CONSTRAINT fk_pacientes_creado_por
    FOREIGN KEY (creado_por) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL,
  CONSTRAINT fk_pacientes_actualizado_por
    FOREIGN KEY (actualizado_por) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE INDEX idx_pacientes_nombre ON pacientes (apellidos, nombre);
CREATE INDEX idx_pacientes_activo ON pacientes (activo);

-- Tabla de constantes vitales
CREATE TABLE constantes (
  constante_id      BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  paciente_id       INT UNSIGNED NOT NULL,
  medido_en         DATETIME NOT NULL,             -- fecha/hora de la medición
  tension_sistolica SMALLINT NULL,                 -- mmHg
  tension_diastolica SMALLINT NULL,                -- mmHg
  frecuencia_cardiaca SMALLINT NULL,               -- lpm
  spo2              TINYINT UNSIGNED NULL,         -- %
  temperatura_c     DECIMAL(4,1) NULL,             -- ºC
  glucosa_mgdl      SMALLINT UNSIGNED NULL,        -- mg/dL
  notas_medicion    VARCHAR(255) NULL,
  registrado_por    INT UNSIGNED NULL,             -- usuario que registra
  creado_en         DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  actualizado_en    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT fk_constantes_paciente
    FOREIGN KEY (paciente_id) REFERENCES pacientes(paciente_id)
    ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT fk_constantes_registrado_por
    FOREIGN KEY (registrado_por) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE INDEX idx_constantes_paciente_fecha ON constantes (paciente_id, medido_en DESC);
CREATE INDEX idx_constantes_registrado_por ON constantes (registrado_por);

-- Tabla de umbrales por paciente (para alertas personalizadas)
CREATE TABLE umbrales_paciente (
  umbral_id        INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  paciente_id      INT UNSIGNED NOT NULL,
  min_tension_sistolica  SMALLINT NULL,
  max_tension_sistolica  SMALLINT NULL,
  min_tension_diastolica SMALLINT NULL,
  max_tension_diastolica SMALLINT NULL,
  min_frecuencia_cardiaca SMALLINT NULL,
  max_frecuencia_cardiaca SMALLINT NULL,
  min_spo2         TINYINT UNSIGNED NULL,
  max_spo2         TINYINT UNSIGNED NULL,
  min_temperatura_c DECIMAL(4,1) NULL,
  max_temperatura_c DECIMAL(4,1) NULL,
  min_glucosa      SMALLINT UNSIGNED NULL,
  max_glucosa      SMALLINT UNSIGNED NULL,
  activo           TINYINT(1) NOT NULL DEFAULT 1,
  creado_en        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  actualizado_en   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  creado_por       INT UNSIGNED NULL,
  actualizado_por  INT UNSIGNED NULL,
  CONSTRAINT fk_umbrales_paciente_paciente
    FOREIGN KEY (paciente_id) REFERENCES pacientes(paciente_id)
    ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT fk_umbrales_paciente_creado_por
    FOREIGN KEY (creado_por) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL,
  CONSTRAINT fk_umbrales_paciente_actualizado_por
    FOREIGN KEY (actualizado_por) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE INDEX idx_umbrales_paciente_activo ON umbrales_paciente (paciente_id, activo);

-- Tabla de alertas
CREATE TABLE alertas (
  alerta_id        BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  paciente_id      INT UNSIGNED NOT NULL,
  constante_id     BIGINT UNSIGNED NULL,           -- si proviene de una medición concreta
  tipo_alerta      ENUM('TA','FC','SPO2','TEMP','GLUCOSA','GENERAL') NOT NULL,
  severidad        ENUM('BAJA','MEDIA','ALTA','CRITICA') NOT NULL,
  mensaje          VARCHAR(255) NOT NULL,
  disparada_en     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  reconocida       TINYINT(1) NOT NULL DEFAULT 0,
  reconocida_en    DATETIME NULL,
  reconocida_por   INT UNSIGNED NULL,
  creado_en        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  actualizado_en   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT fk_alertas_paciente
    FOREIGN KEY (paciente_id) REFERENCES pacientes(paciente_id)
    ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT fk_alertas_constante
    FOREIGN KEY (constante_id) REFERENCES constantes(constante_id)
    ON UPDATE CASCADE ON DELETE SET NULL,
  CONSTRAINT fk_alertas_reconocida_por
    FOREIGN KEY (reconocida_por) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE INDEX idx_alertas_paciente_tiempo ON alertas (paciente_id, disparada_en DESC);
CREATE INDEX idx_alertas_reconocida ON alertas (reconocida, reconocida_en);

-- Tabla de auditoría simple (opcional)
CREATE TABLE auditoria (
  auditoria_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  usuario_id   INT UNSIGNED NULL,
  entidad      VARCHAR(100) NOT NULL,              -- 'pacientes','constantes','alertas','usuarios', etc.
  entidad_id   BIGINT UNSIGNED NOT NULL,
  accion       ENUM('CREAR','ACTUALIZAR','ELIMINAR','LOGIN','LOGOUT','ASIGNAR_ROL') NOT NULL,
  detalles     JSON NULL,
  creado_en    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT fk_auditoria_usuario
    FOREIGN KEY (usuario_id) REFERENCES usuarios(usuario_id)
    ON UPDATE SET NULL ON DELETE SET NULL
) ENGINE=InnoDB;

-- Datos iniciales de roles
INSERT INTO roles (nombre, descripcion) VALUES
  ('Administrador', 'Acceso completo a la administración del sistema'),
  ('Medico', 'Profesional médico'),
  ('Enfermera', 'Profesional de enfermería'),
  ('Recepcion', 'Gestión administrativa básica'),
  ('Paciente', 'Usuario paciente con acceso limitado a sus datos');

-- Índices útiles adicionales para informes/gráficas
CREATE INDEX idx_constantes_ta ON constantes (tension_sistolica, tension_diastolica);
CREATE INDEX idx_constantes_fc ON constantes (frecuencia_cardiaca);
CREATE INDEX idx_constantes_spo2 ON constantes (spo2);
CREATE INDEX idx_constantes_temp ON constantes (temperatura_c);
CREATE INDEX idx_constantes_glucosa ON constantes (glucosa_mgdl);