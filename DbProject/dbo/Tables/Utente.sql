CREATE TABLE [dbo].[Utente] (
    [ID]                 INT          IDENTITY (1, 1) NOT NULL,
    [Cognome]            VARCHAR (50) NOT NULL,
    [Nome]               VARCHAR (50) NOT NULL,
    [Data_Nascita]       DATE         NOT NULL,
    [Citta_Nascita]      VARCHAR (50) NOT NULL,
    [Provincia_Nascita]  CHAR (2)     NOT NULL,
    [Sesso]              CHAR (1)     NOT NULL,
    [Codice_Fiscale]     CHAR (16)    NOT NULL,
    [Data_Registrazione] DATETIME     NOT NULL,
    [Email]              VARCHAR (50) NOT NULL,
    [Password]           CHAR (16)    NOT NULL,
    CONSTRAINT [PK_Utente] PRIMARY KEY CLUSTERED ([ID] ASC)
);

