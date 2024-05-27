CREATE TABLE [dbo].[Stato_Ordine] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Descrizione] VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Stato_Ordine] PRIMARY KEY CLUSTERED ([ID] ASC)
);

