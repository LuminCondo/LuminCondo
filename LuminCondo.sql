/*CREATE DATABASE Lumincondo_DB;
USE Lumincondo_DB;

USE master;
DROP DATABASE Lumincondo_DB;
*/

CREATE TABLE EstadoReserva (
							ID INT IDENTITY(1,1) NOT NULL, /*PK*/
							descripcion VARCHAR(20) NOT NULL,
							PRIMARY KEY (ID)
							);/*#1*/

CREATE TABLE TiposUsuarios (
							ID INT IDENTITY(1,1) NOT NULL, /*PK*/
							tipoUsuario VARCHAR(20) NOT NULL,
							PRIMARY KEY (ID)
							);/*#1*/

CREATE TABLE EstadoIncidencia (
								IDEstado INT IDENTITY(1,1) NOT NULL, /*PK*/
								TipoEstado VARCHAR(20) NOT NULL,
								PRIMARY KEY (IDEstado)
								);/*#2*/

CREATE TABLE TipoInformacion (
								IDTipoInfo INT IDENTITY(1,1) NOT NULL, /*PK*/
								tipoInfo VARCHAR(40)NOT NULL,
								PRIMARY KEY (IDTipoInfo)
								);/*#3*/

CREATE TABLE Espacios (
						IDEspacio INT IDENTITY(1,1) NOT NULL, /*PK*/
						descripcion VARCHAR(25) NOT NULL,
						PRIMARY KEY (IDEspacio)
						);/*#4*/

CREATE TABLE EstadoResidencia (
								IDEstadoResidencia INT IDENTITY(1,1) NOT NULL, /*PK*/
								estado VARCHAR(25) NOT NULL,
								PRIMARY KEY (IDEstadoResidencia)
								);/*#5*/

CREATE TABLE GestionRubrosCobros (
									IDRubro INT IDENTITY(1,1) NOT NULL, /*PK*/
									descripcion VARCHAR(100) NOT NULL,
									monto MONEY NOT NULL,
									PRIMARY KEY (IDRubro)
									);/*#6*/
/*CAMBIAR NOMBRES DE FKS*/
CREATE TABLE Usuarios (
						ID INT IDENTITY(1,1) NOT NULL, /*PK*/ 
						nombre VARCHAR(25) NOT NULL, 
						contrasenna VARCHAR(25) NOT NULL, 
						IDTipoUsuario INT NOT NULL, /*FK*/ 
						email VARCHAR(50) NOT NULL, 
						estado BIT NOT NULL, 
						telefono INT NOT NULL,

						PRIMARY KEY (ID),
						CONSTRAINT FK_Usuarios_IDTipoUsuario FOREIGN KEY (IDTipoUsuario) REFERENCES TiposUsuarios(ID)
						);/*#7*/

CREATE TABLE ReporteIncidencias (
									IDIncidencia INT IDENTITY(1,1) NOT NULL, /*PK*/
									IDEstado INT NOT NULL, /*FK*/
									IDUsuario INT NOT NULL, /*FK*/
									descripcion VARCHAR(50) NOT NULL, /*
									evidencias foto*/ 
									PRIMARY KEY (IDIncidencia),
									CONSTRAINT FK_ReporteIncidencias_IDEstado FOREIGN KEY (IDEstado) REFERENCES EstadoIncidencia(IDEstado),
									CONSTRAINT FK_ReporteIncidencias_IDUsuario FOREIGN KEY (IDUsuario) REFERENCES Usuarios(ID)
									);

CREATE TABLE Informacion (
							IDInformacion INT IDENTITY(1,1) NOT NULL, /*PK*/
							IDTipoInfo INT NOT NULL, /*FK*/
							fechaPublicacion DATE NOT NULL,
							titulo VARCHAR(50) NOT NULL,
							descripcion VARCHAR(MAX) NOT NULL,
							foto VARBINARY(MAX)  NULL,
							PRIMARY KEY (IDInformacion),
							CONSTRAINT FK_Informacion_IDTipoInfo FOREIGN KEY (IDTipoInfo) REFERENCES TipoInformacion(IDTipoInfo)
							);

CREATE TABLE GestionReservas (
								IDReserva INT IDENTITY(1,1) NOT NULL, /*PK*/
								IDUsuario INT NOT NULL, /*FK*/
								IDEspacio INT NOT NULL, /*FK*/
								fecha DATE NOT NULL,/*
								horaInicio TIME NOT NULL,
								horaFinal TIME NOT NULL,*/
								IDEstado INT NOT NULL,
								PRIMARY KEY (IDReserva),
								CONSTRAINT FK_GestionReservas_IDUsuario FOREIGN KEY (IDUsuario) REFERENCES Usuarios(ID),
								CONSTRAINT FK_GestionReservas_IDEspacio FOREIGN KEY (IDEspacio) REFERENCES Espacios(IDEspacio),
								CONSTRAINT FK_GestionReservas_IDEstado FOREIGN KEY (IDEstado) REFERENCES EstadoReserva(ID)
								);

CREATE TABLE GestionResidencias (
									IDResidencia INT IDENTITY(1,1) NOT NULL, /*PK*/
									IDUsuario INT NOT NULL, /*FK*/
									cantPersonas INT NOT NULL,
									IDEstadoResidencia INT NOT NULL, /*FK*/
									annoInicio INT NOT NULL,
									cantCarros INT NOT NULL,
									PRIMARY KEY (IDResidencia),
									CONSTRAINT FK_GestionResidencias_IDUsuario FOREIGN KEY (IDUsuario) REFERENCES Usuarios(ID),
									CONSTRAINT FK_GestionResidencias_IDEstadoResidencia FOREIGN KEY (IDEstadoResidencia) REFERENCES EstadoResidencia(IDEstadoResidencia),
									);

CREATE TABLE Carros (
						IDPlaca VARCHAR(10) NOT NULL,/*PK*/
						IDResidencia INT NOT NULL, /*FK*/
						modelo VARCHAR(50) NOT NULL,
						tipo bit not null,
						PRIMARY KEY (IDPlaca),
						CONSTRAINT FK_Carros_IDResidencia FOREIGN KEY (IDResidencia) REFERENCES GestionResidencias(IDResidencia)
						);

CREATE TABLE Personas (
									IDCedula INT NOT NULL, /*PK*/
									IDResidencia INT NOT NULL, /*FK*/
									nombre VARCHAR(50) NOT NULL,
									tipo bit not null,
									PRIMARY KEY (IDCedula),
									CONSTRAINT FK_PersonasResidentes_IDResidencia FOREIGN KEY (IDResidencia) REFERENCES GestionResidencias(IDResidencia)
									);

CREATE TABLE GestionPlanCobros (
									IDPlan INT IDENTITY(1,1) NOT NULL, /*PK*/
									descripcion VARCHAR(100) NOT NULL,
									PRIMARY KEY (IDPlan),
									);

CREATE TABLE GestionAsignacionPlanes (
										IDAsignacion INT IDENTITY(1,1) NOT NULL, /*PK*/
										IDResidencia INT NOT NULL, /*FK*/
										IDPlan INT NOT NULL, /*FK*/
										fechaAsignacion DATE NOT NULL,
										estadoPago BIT NOT NULL,
										PRIMARY KEY (IDAsignacion),
										CONSTRAINT FK_GestionAsignacionPlanes_IDResidencia FOREIGN KEY (IDResidencia) REFERENCES GestionResidencias(IDResidencia),
										CONSTRAINT FK_GestionAsignacionPlanes_IDPlan FOREIGN KEY (IDPlan) REFERENCES GestionPlanCobros(IDPlan)
										);

CREATE TABLE Rubros_Planes (
								IDPlan INT NOT NULL,
								IDRubro INT NOT NULL
								CONSTRAINT FK_Rubros_Planes_IDRubro FOREIGN KEY (IDRubro) REFERENCES GestionRubrosCobros(IDRubro),
								CONSTRAINT FK_Rubros_Planes_IDPlan FOREIGN KEY (IDPlan) REFERENCES GestionPlanCobros(IDPlan)
								);

ALTER TABLE [dbo].[Rubros_Planes] ADD  CONSTRAINT [PK_Rubros_Planes] PRIMARY KEY NONCLUSTERED 
(
	[IDPlan] ASC,
	[IDRubro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO		

select * from GestionReservas

/**********************************************Inserts de las tablas**************************************************************/
insert into dbo.TiposUsuarios values
('Administrador'), 
('Residente')

insert into dbo.Usuarios values
('Israel','u6QXcpElu3jBzi22FNFdrw==',1,'cmisra2407@gmail.com',1,21211212),
('Luis','u6QXcpElu3jBzi22FNFdrw==',1,'lumincondo@gmail.com',1,24242424),
('Jocelyn','u6QXcpElu3jBzi22FNFdrw==',2,'luminCondo1@gmail.com',1,89170301)

insert into dbo.Espacios values
('Piscina #1'), 
('Piscina #2'),
('Rancho #1'),
('Rancho #2'),
('Rancho #3'),
('Rancho #4'),
('Sala de Eventos'),
('Working Space #1'),
('Working Space #2'),
('Working Space #3'),
('Cancha Multiusos')

insert into dbo.EstadoIncidencia values 
('Nueva'),
('Activa'),
('Resuelta')

insert into dbo.EstadoReserva values 
('Pendiente'),
('Aprobado'),
('Rechazado')

insert into dbo.GestionReservas values
(1,1,CONVERT(date, '19/02/2022', 103),1),
(1,2,CONVERT(date, '19/03/2023', 103),2),
(2,3,CONVERT(date, '28/01/2023', 103),3),
(3,7,CONVERT(date, '09/04/2023', 103),1)

insert into dbo.ReporteIncidencias values
(1,1,'Fuga de Agua en calle frente mi residencia'),
(2,2,'Problema Tomacorriente de la cocina'),
(3,3,'Caída de Arbol')

insert into dbo.EstadoResidencia values
('En Construcción'),
('Habitada'),
('Sin Habitar'),
('Abandonada'),
('Alquilada')

insert into dbo.GestionResidencias values
(1,4,2,2020,1),
(2,3,2,2016,2),
(3,6,2,2006,1)

insert into dbo.Carros values
('LDA-249',1,'BMW Serie3 2022',1),
('XYZ-777',2,'RAV4 2023',1),
('ABC-111',2,'Kia Sportage',0),
('JCV-031',3,'Camaro',1)

insert into Personas values
(117960190,1,'Israel',1),
(111122223,1,'Maria',1),
(112233445,1,'Marelene',1),
(224455755,2,'Luis',1),
(445577669,3,'Jocelyn',1)

insert into TipoInformacion values
('Noticias'),
('Archivos Documentales'),
('Avisos')

insert into dbo.Informacion (IDTipoInfo,fechaPublicacion,titulo,descripcion)values
(1,CONVERT(date, '18/02/2023', 103),'Campaña de donación','En la comunidad se van a desarrollar varia actividades en conjunto con el objetivo de recaudar fondos para ayudar a una asociacion sin fines de lucro, pronto mas detalles de la actividad.'),
(2,CONVERT(date, '18/02/2023', 103),'Calle en reparación','La Calle principal del condominio va a estar con paso regulado durante esta semana debido a resparaciones en las tuberias'),
(3,CONVERT(date, '18/02/2023', 103),'Corte de agua por parte de AYA', 'El dia 24/02/2023 se tiene precisto un corte de agua por el AYA, por favor tomar las medidas necesarias.')

insert into GestionRubrosCobros values 
('Mensualidad Condominio', 25000),
('Mantenimiento de Areas comunes',7500),
('Renta de Sala de Eventos',30000)

insert into GestionPlanCobros values 
('Solo Mensualidad'),
('Solo Mantenimiento de Areas comunes'),
('Mantenimiento y Mensualidad, con uso de Sala de Eventos'),
('Mantenimiento y Mensualidad'),
('Uso de Sala de Eventos')

insert into Rubros_Planes values
(1,1),
(2,2),
(3,1),
(3,2),
(3,3),
(4,1),
(4,2),
(5,3)

insert into GestionAsignacionPlanes values
(1,3,CONVERT(date, '26/02/2023', 103),0),
(1,3,CONVERT(date, '18/03/2023', 103),1),
(2,4,CONVERT(date, '18/04/2023', 103),1),
(3,1,CONVERT(date, '18/12/2022', 103),1)


USE Lumincondo_DB;
GO
SELECT * FROM sys.tables;

DECLARE @TableName NVARCHAR(128)
DECLARE @SQL NVARCHAR(MAX)

DECLARE TableCursor CURSOR FAST_FORWARD FOR
SELECT name FROM sys.tables

OPEN TableCursor
FETCH NEXT FROM TableCursor INTO @TableName

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @SQL = 'SELECT * FROM ' + @TableName
    EXEC sp_executesql @SQL
    FETCH NEXT FROM TableCursor INTO @TableName
END

CLOSE TableCursor
DEALLOCATE TableCursor