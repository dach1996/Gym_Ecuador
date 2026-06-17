-- =================================================================
-- Catálogos: Nivel actividad física y Ritmo de progreso (Perfiles)
-- Ejecutar antes de Script.CreateTableProfiles.sql
-- =================================================================

USE [Gym];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- Padre: Nivel actividad física
EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'NIVEL_ACTIVIDAD_FISICA',
    N'Nivel actividad física';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'ACTIVIDAD_SEDENTARIO',
    N'Sedentario (poco o ningún ejercicio)',
    'NIVEL_ACTIVIDAD_FISICA',
    '1.2';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'ACTIVIDAD_LIGERO',
    N'Ligero (1-2 días ejercicio/semana)',
    'NIVEL_ACTIVIDAD_FISICA',
    '1.375';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'ACTIVIDAD_MODERADO',
    N'Moderado (3-5 días ejercicio/semana)',
    'NIVEL_ACTIVIDAD_FISICA',
    '1.55';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'ACTIVIDAD_INTENSO',
    N'Intenso (6-7 días ejercicio/semana)',
    'NIVEL_ACTIVIDAD_FISICA',
    '1.725';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'ACTIVIDAD_MUY_INTENSO',
    N'Muy intenso (atleta / trabajo físico)',
    'NIVEL_ACTIVIDAD_FISICA',
    '1.9';
GO

-- Padre: Ritmo de progreso
EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'RITMO_PROGRESO',
    N'Ritmo de progreso';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'RITMO_LENTO',
    N'Lento',
    'RITMO_PROGRESO',
    '250';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'RITMO_MODERADO',
    N'Moderado',
    'RITMO_PROGRESO',
    '500';
GO

EXECUTE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    'RITMO_AGRESIVO',
    N'Agresivo',
    'RITMO_PROGRESO',
    '750';
GO

PRINT 'Catálogos de perfiles insertados o actualizados.';
GO
