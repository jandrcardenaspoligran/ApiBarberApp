USE [BarberApp]
GO
/****** Object:  Schema [BarberApp]    Script Date: 4/12/2024 11:42:08 p. m. ******/
CREATE SCHEMA [BarberApp]
GO
/****** Object:  Table [BarberApp].[Agenda]    Script Date: 4/12/2024 11:42:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BarberApp].[Agenda](
	[id] [uniqueidentifier] NOT NULL,
	[id_cliente] [uniqueidentifier] NULL,
	[id_barber] [uniqueidentifier] NOT NULL,
	[fecha_hora] [datetime] NOT NULL,
	[msg_cliente] [varchar](512) NULL,
	[obs_barber] [varchar](512) NULL,
	[img_referencia] [varchar](256) NULL,
	[estado] [varchar](32) NOT NULL,
	[editado_por] [uniqueidentifier] NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [BarberApp].[Notificaciones]    Script Date: 4/12/2024 11:42:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BarberApp].[Notificaciones](
	[id] [uniqueidentifier] NOT NULL,
	[id_usuario] [uniqueidentifier] NOT NULL,
	[fecha_hora] [datetime] NOT NULL,
	[mensaje] [varchar](1024) NOT NULL,
	[leido] [tinyint] NOT NULL,
	[titulo] [varchar](128) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [BarberApp].[Roles]    Script Date: 4/12/2024 11:42:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BarberApp].[Roles](
	[id] [uniqueidentifier] NOT NULL,
	[nombre] [varchar](256) NOT NULL,
	[descripcion] [varchar](512) NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [BarberApp].[Usuarios]    Script Date: 4/12/2024 11:42:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BarberApp].[Usuarios](
	[id] [uniqueidentifier] NOT NULL,
	[correo] [varchar](256) NOT NULL,
	[clave] [varchar](4) NOT NULL,
	[nombre] [varchar](256) NOT NULL,
	[apellidos] [varchar](256) NOT NULL,
	[genero] [varchar](32) NOT NULL,
	[fecha_nacimiento] [date] NOT NULL,
	[id_rol] [uniqueidentifier] NOT NULL,
	[activo] [tinyint] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [BarberApp].[Agenda] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [BarberApp].[Agenda] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [BarberApp].[Notificaciones] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [BarberApp].[Roles] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [BarberApp].[Roles] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [BarberApp].[Usuarios] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [BarberApp].[Usuarios] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [BarberApp].[Agenda]  WITH CHECK ADD  CONSTRAINT [FK_Agenda_Usuario_Barber] FOREIGN KEY([id_barber])
REFERENCES [BarberApp].[Usuarios] ([id])
GO
ALTER TABLE [BarberApp].[Agenda] CHECK CONSTRAINT [FK_Agenda_Usuario_Barber]
GO
ALTER TABLE [BarberApp].[Agenda]  WITH CHECK ADD  CONSTRAINT [FK_Agenda_Usuario_Cliente] FOREIGN KEY([id_cliente])
REFERENCES [BarberApp].[Usuarios] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [BarberApp].[Agenda] CHECK CONSTRAINT [FK_Agenda_Usuario_Cliente]
GO
ALTER TABLE [BarberApp].[Agenda]  WITH CHECK ADD  CONSTRAINT [FK_Agenda_Usuario_Editado] FOREIGN KEY([editado_por])
REFERENCES [BarberApp].[Usuarios] ([id])
GO
ALTER TABLE [BarberApp].[Agenda] CHECK CONSTRAINT [FK_Agenda_Usuario_Editado]
GO
ALTER TABLE [BarberApp].[Notificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Notificaciones_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [BarberApp].[Usuarios] ([id])
GO
ALTER TABLE [BarberApp].[Notificaciones] CHECK CONSTRAINT [FK_Notificaciones_Usuario]
GO
ALTER TABLE [BarberApp].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([id_rol])
REFERENCES [BarberApp].[Roles] ([id])
GO
ALTER TABLE [BarberApp].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
/****** Object:  StoredProcedure [BarberApp].[PR_ASIGNAR_AGENDA_BARBERS]    Script Date: 4/12/2024 11:42:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [BarberApp].[PR_ASIGNAR_AGENDA_BARBERS]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Manana DATE = DATEADD(DAY, 1, CAST(GETDATE() AS DATE));
    DECLARE @PasadoManana DATE = DATEADD(DAY, 2, CAST(GETDATE() AS DATE));
    
    CREATE TABLE #TiemposAgenda (
        HoraCita TIME
    );

    INSERT INTO #TiemposAgenda (HoraCita)
    VALUES 
        ('09:00:00'), ('09:45:00'), ('10:30:00'), ('11:15:00'),
        ('13:00:00'), ('13:45:00'), ('14:30:00'), ('15:15:00'),
        ('16:00:00'), ('16:45:00'), ('17:30:00'), ('18:15:00');

    DECLARE @Barber TABLE (id UNIQUEIDENTIFIER);

    INSERT INTO @Barber (id)
    SELECT a.id FROM BarberApp.BarberApp.Usuarios a
    JOIN BarberApp.BarberApp.Roles b ON a.id_rol = b.id
    WHERE a.activo = 1 AND UPPER(b.nombre) LIKE '%BARBER%';

    INSERT INTO BarberApp.BarberApp.Agenda (
        id, id_cliente, id_barber, fecha_hora, 
        msg_cliente, obs_barber, img_referencia, 
        estado, editado_por, fecha_creacion, fecha_actualizacion
    )
    SELECT 
        NEWID() AS id,
        NULL AS id_cliente,
        b.id AS id_barber,
        CAST(@Manana AS DATETIME) + CAST(t.HoraCita AS DATETIME) AS fecha_hora,
        NULL AS msg_cliente,
        NULL AS obs_barber,
        NULL AS img_referencia,
        'DISPONIBLE' AS estado,
        NULL AS editado_por,
        GETDATE() AS fecha_creacion,
        GETDATE() AS fecha_actualizacion
    FROM @Barber b
    CROSS JOIN #TiemposAgenda t
    WHERE NOT EXISTS (
        SELECT 1
        FROM BarberApp.BarberApp.Agenda a
        WHERE a.id_barber = b.id
          AND a.fecha_hora = CAST(@Manana AS DATETIME) + CAST(t.HoraCita AS DATETIME)
    )

    UNION ALL

    SELECT 
        NEWID() AS id,
        NULL AS id_cliente,
        b.id AS id_barber,
        CAST(@PasadoManana AS DATETIME) + CAST(t.HoraCita AS DATETIME) AS fecha_hora,
        NULL AS msg_cliente,
        NULL AS obs_barber,
        NULL AS img_referencia,
        'DISPONIBLE' AS estado,
        NULL AS editado_por,
        GETDATE() AS fecha_creacion,
        GETDATE() AS fecha_actualizacion
    FROM @Barber b
    CROSS JOIN #TiemposAgenda t
    WHERE NOT EXISTS (
        SELECT 1
        FROM BarberApp.BarberApp.Agenda a
        WHERE a.id_barber = b.id
          AND a.fecha_hora = CAST(@PasadoManana AS DATETIME) + CAST(t.HoraCita AS DATETIME)
    );

    DROP TABLE #TiemposAgenda;

    PRINT 'Registros insertados con éxito';
END;
GO
