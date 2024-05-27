CREATE TABLE [dbo].[Dettaglio_Ordine] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [FK_ID_Ordine]   INT NOT NULL,
    [FK_ID_Prodotto] INT NOT NULL,
    [Quantita]       INT NOT NULL,
    CONSTRAINT [PK_Dettaglio_Ordine] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Dettaglio_Ordine_Dettaglio_Ordine] FOREIGN KEY ([FK_ID_Ordine]) REFERENCES [dbo].[Ordine] ([ID]),
    CONSTRAINT [FK_Dettaglio_Ordine_Prodotto] FOREIGN KEY ([FK_ID_Prodotto]) REFERENCES [dbo].[Prodotto] ([ID])
);

