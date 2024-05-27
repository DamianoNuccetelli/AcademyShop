CREATE TABLE [dbo].[Prodotto] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (50) NOT NULL,
    [Descrizione] VARCHAR (50) NOT NULL,
    [Quantità]    INT          NOT NULL,
    CONSTRAINT [PK_Prodotto_1] PRIMARY KEY CLUSTERED ([ID] ASC)
);

