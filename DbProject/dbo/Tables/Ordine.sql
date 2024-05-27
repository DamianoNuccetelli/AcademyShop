CREATE TABLE [dbo].[Ordine] (
    [ID]                 INT      IDENTITY (1, 1) NOT NULL,
    [FK_ID_Utente]       INT      NOT NULL,
    [Data_Registrazione] DATETIME NOT NULL,
    [Data_Aggiornamento] DATETIME NULL,
    [FK_ID_Stato]        INT      NOT NULL,
    CONSTRAINT [PK_Ordine] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Ordine_Stato_Ordine] FOREIGN KEY ([FK_ID_Stato]) REFERENCES [dbo].[Stato_Ordine] ([ID]),
    CONSTRAINT [FK_Ordine_Utente] FOREIGN KEY ([FK_ID_Utente]) REFERENCES [dbo].[Utente] ([ID])
);

