create database PRUEBA_1024

use PRUEBA_1024

CREATE TABLE Roles(
	IdRol int not null constraint PK_RolesIdRol Primary key identity(1,1),
	Nombre varchar(50) not null,
	Estado bit not null default(1)
)
INSERT INTO Roles (Nombre) VALUES ('Super Admin'), ('Agente');
-------------------------------------------------------------------

CREATE TABLE Usuario(
	IdUsuario INT NOT NULL CONSTRAINT PK_UsuarioIdSuario PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50) NOT NULL,
	Correo varchar(100) not null,
	--cositas de auditoria
	Clave varchar(300) null,
	FechaCreacion datetime NOT NULL default(getdate()),
	UsuarioCreacion varchar(50) NOT NULL default(current_user),
	FechaModificacion datetime,
	UsuarioModificacion varchar(50),
	FechaEliminacion datetime,
	UsuarioEliminacion varchar(50),
	IpLocal VARCHAR(15) NOT NULL default('0.0.0.0'),
	Estado BIT NOT NULL default(1),
	IdRol int not null,
	CONSTRAINT FK_UsuarioIdRol_IdRol FOREIGN KEY (IdRol) REFERENCES Roles(IdRol) ON DELETE CASCADE
)
INSERT INTO Usuario (Nombre, Correo, Clave, IdRol) VALUES ('usu1', 'usu1@correo.com', null, 2),
														  ('usu2', 'usu2@correo.com', null, 2),
														  ('usu3', 'usu3@correo.com', null, 2),
														  ('admin', 'admin@correo.com', 'admin', 1);
-----------------------------------------------------------------

CREATE TABLE Agente (
    IdAgente INT NOT NULL CONSTRAINT PK_AgenteIdAgente PRIMARY KEY IDENTITY(1,1),
	IdUsuario INT UNIQUE NOT NULL,
	FechaCreacion datetime NOT NULL default(getdate()),
	UsuarioCreacion varchar(50) NOT NULL default(current_user),
	FechaEliminacion datetime,
	UsuarioEliminacion varchar(50),
	IpLocal VARCHAR(15) NOT NULL  default('0.0.0.0'),
	Estado bit not null default(1),
	CONSTRAINT FK_AgenteIdUsuario_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE
);
INSERT INTO Agente (IdUsuario) VALUES (1), (2), (3);
----------------------------------------------------------------

CREATE TABLE Semanas (
    IdSemana INT CONSTRAINT PK_SemanasIdSemana PRIMARY KEY IDENTITY(1,1),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
	Numero int not null,
	FechaCreacion datetime NOT NULL default(getdate()),
	UsuarioCreacion varchar(50) NOT NULL default(current_user),
	FechaEliminacion datetime,
	UsuarioEliminacion varchar(50),
	Estado BIT NOT NULL default(1)
);
INSERT INTO Semanas (FechaInicio, FechaFin, Numero) VALUES ('2024-01-01', '2024-01-07', 1), 
														   ('2024-01-08', '2024-01-14', 2), 
														   ('2024-01-15', '2024-01-21', 3);
---------------------------------------------------------------

CREATE TABLE Disponibilidad (
    IdDisponibilidad INT CONSTRAINT PK_Disponibilidad PRIMARY KEY IDENTITY(1,1),
    IdAgente INT NOT NULL,
    IdSemana INT NOT NULL,
    Dia VARCHAR(10) NOT NULL,
    Disponible BIT NOT NULL,
	FechaCreacion datetime NOT NULL default(getdate()),
	UsuarioCreacion varchar(50) NOT NULL default(current_user),
	FechaEliminacion datetime,
	UsuarioEliminacion varchar(50),
	Estado bit not null default(1),
    CONSTRAINT FK_DisponibiliadIdAgente_IdAgente FOREIGN KEY (IdAgente) REFERENCES Agente(IdAgente),
    CONSTRAINT FK_DisponibiliadIdSemana_IdSemana FOREIGN KEY (IdSemana) REFERENCES Semanas(IdSemana)
);
-- Insertar Disponibilidad para usuarios en semana 1
INSERT INTO Disponibilidad (IdAgente, IdSemana, Dia, Disponible) 
VALUES 
(1, 1, 'Lunes', 0), (1, 1, 'Martes', 0), (1, 1, 'Miércoles', 1), (1, 1, 'Jueves', 1), (1, 1, 'Viernes', 1), (1, 1, 'Sábado', 1), (1, 1, 'Domingo', 1),
(2, 1, 'Lunes', 1), (2, 1, 'Martes', 1), (2, 1, 'Miércoles', 0), (2, 1, 'Jueves', 0), (2, 1, 'Viernes', 1), (2, 1, 'Sábado', 1), (2, 1, 'Domingo', 1),
(3, 1, 'Lunes', 1), (3, 1, 'Martes', 1), (3, 1, 'Miércoles', 1), (3, 1, 'Jueves', 1), (3, 1, 'Viernes', 0), (3, 1, 'Sábado', 0), (3, 1, 'Domingo', 1);

-- Insertar Disponibilidad para usuarios en semana 2
INSERT INTO Disponibilidad (IdAgente, IdSemana, Dia, Disponible) 
VALUES 
(1, 2, 'Lunes', 1), (1, 2, 'Martes', 1), (1, 2, 'Miércoles', 0), (1, 2, 'Jueves', 0), (1, 2, 'Viernes', 1), (1, 2, 'Sábado', 1), (1, 2, 'Domingo', 1),
(2, 2, 'Lunes', 1), (2, 2, 'Martes', 1), (2, 2, 'Miércoles', 1), (2, 2, 'Jueves', 1), (2, 2, 'Viernes', 0), (2, 2, 'Sábado', 0), (2, 2, 'Domingo', 1),
(3, 2, 'Lunes', 1), (3, 2, 'Martes', 1), (3, 2, 'Miércoles', 1), (3, 2, 'Jueves', 1), (3, 2, 'Viernes', 1), (3, 2, 'Sábado', 1), (3, 2, 'Domingo', 0);

---
EXEC sp_addextendedproperty 
    @name = N'MS_Description',
    @value = '0 = No disponible, 1 = Disponible',
    @level0type = N'SCHEMA', 
    @level0name = 'dbo',
    @level1type = N'TABLE', 
    @level1name = 'Disponibilidad',
    @level2type = N'COLUMN', 
    @level2name = 'Disponible';

CREATE UNIQUE INDEX IX_Usuarios_Correo_Activo 
ON Usuario (Correo, Estado) 
WHERE Estado = 1;

CREATE UNIQUE INDEX IX_Usuarios_Nombre_Activo 
ON Usuario (Nombre, Estado) 
WHERE Estado = 1;