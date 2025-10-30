CREATE TABLE [AUTENTICACION].[PERSONA] (
    [PNA_ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [PNA_GUID]                  UNIQUEIDENTIFIER CONSTRAINT [DEFAULT_PERSONA_PNA_GUID] DEFAULT (newid()) NOT NULL,
    [PNA_FECHA_REGISTRO]        DATETIME         NULL,
    [PNA_CODIGO_NACIONALIDAD]   VARCHAR (50)     NULL,
    [PNA_CODIGO_TIPO_DOCUMENTO] VARCHAR (50)     NULL,
    [PNA_NUMERO_DOCUMENTO]      VARCHAR (50)     NOT NULL,
    [PNA_NOMBRE_INGRESADO]      VARCHAR (150)    NULL,
    [PNA_APELLIDO_INGRESADO]    VARCHAR (150)    NULL,
    [PNA_NOMBRES_REALES]        VARCHAR (150)    NOT NULL,
    [PNA_APELLIDOS_REALES]      VARCHAR (150)    NOT NULL,
    [PNA_NOMBRE_COMPLETO]       VARCHAR (300)    NOT NULL,
    [USR_ID_REGISTRADOR]        INT              NULL,
    [USR_ID_ULTIMO_LOGIN]       INT              NULL,
    [PNA_FECHA_NACIMIENTO]      DATE             NULL,
    [CAT_ID_GENERO]             INT              NULL,
    CONSTRAINT [PK_PERSONA] PRIMARY KEY CLUSTERED ([PNA_ID] ASC),
    CONSTRAINT [FK_PERSONA_CATALOGO] FOREIGN KEY ([CAT_ID_GENERO]) REFERENCES [ADMINISTRACION].[CATALOGO] ([CAT_ID]),
    CONSTRAINT [FK_PERSONA_USUARIO_REGISTRADOR] FOREIGN KEY ([USR_ID_REGISTRADOR]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]),
    CONSTRAINT [FK_PERSONA_USUARIO_ULTIMO_REGISTRO] FOREIGN KEY ([USR_ID_ULTIMO_LOGIN]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID])
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice número de Documento', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'INDEX', @level2name = N'IX_NUMERO_DOCUMENTO';


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_NUMERO_DOCUMENTO]
    ON [AUTENTICACION].[PERSONA]([PNA_NUMERO_DOCUMENTO] ASC);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_FECHA_REGISTRO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombres Reales', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_NOMBRES_REALES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_NOMBRE_COMPLETO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de último usuario que realizó Logín', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'USR_ID_ULTIMO_LOGIN';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Catálogo de Géneros', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'CONSTRAINT', @level2name = N'FK_PERSONA_CATALOGO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Persona', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Personas', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_FECHA_NACIMIENTO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Tipo de Documento', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_CODIGO_TIPO_DOCUMENTO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Ingresado', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_NOMBRE_INGRESADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apeliidos Reales', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_APELLIDOS_REALES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Nacionalidad', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_CODIGO_NACIONALIDAD';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid de Persona', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciòn con ùltimo usuario que ingresò', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'CONSTRAINT', @level2name = N'FK_PERSONA_USUARIO_ULTIMO_REGISTRO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario que registró la persona', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Persona', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'CONSTRAINT', @level2name = N'PK_PERSONA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de catálogo de Género', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'CAT_ID_GENERO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciòn con usuario registrador', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'CONSTRAINT', @level2name = N'FK_PERSONA_USUARIO_REGISTRADOR';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apellido Ingresado', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_APELLIDO_INGRESADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Documento', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_NUMERO_DOCUMENTO';


GO

