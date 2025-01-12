USE [master]
GO
/****** Object:  Database [AcademyShopDB]    Script Date: 23/05/2024 16:05:29 ******/
CREATE DATABASE [AcademyShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AcademyShopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AcademyShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AcademyShopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AcademyShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [AcademyShopDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AcademyShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AcademyShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AcademyShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AcademyShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AcademyShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AcademyShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AcademyShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AcademyShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AcademyShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AcademyShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AcademyShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AcademyShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AcademyShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AcademyShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AcademyShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AcademyShopDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AcademyShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AcademyShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AcademyShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AcademyShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AcademyShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AcademyShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AcademyShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AcademyShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [AcademyShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [AcademyShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AcademyShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AcademyShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AcademyShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AcademyShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AcademyShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AcademyShopDB', N'ON'
GO
ALTER DATABASE [AcademyShopDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [AcademyShopDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [AcademyShopDB]
GO
/****** Object:  Table [dbo].[Dettaglio_Ordine]    Script Date: 23/05/2024 16:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dettaglio_Ordine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FK_ID_Ordine] [int] NOT NULL,
	[FK_ID_Prodotto] [int] NOT NULL,
	[Quantita] [int] NOT NULL,
 CONSTRAINT [PK_Dettaglio_Ordine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ordine]    Script Date: 23/05/2024 16:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FK_ID_Utente] [int] NOT NULL,
	[Data_Registrazione] [datetime] NOT NULL,
	[Data_Aggiornamento] [datetime] NULL,
	[FK_ID_Stato] [int] NOT NULL,
 CONSTRAINT [PK_Ordine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prodotto]    Script Date: 23/05/2024 16:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prodotto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Descrizione] [varchar](50) NOT NULL,
	[Quantità] [int] NOT NULL,
 CONSTRAINT [PK_Prodotto_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stato_Ordine]    Script Date: 23/05/2024 16:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stato_Ordine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Stato_Ordine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utente]    Script Date: 23/05/2024 16:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utente](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cognome] [varchar](50) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Data_Nascita] [date] NOT NULL,
	[Citta_Nascita] [varchar](50) NOT NULL,
	[Provincia_Nascita] [char](2) NOT NULL,
	[Sesso] [char](1) NOT NULL,
	[Codice_Fiscale] [char](16) NOT NULL,
	[Data_Registrazione] [datetime] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [char](16) NOT NULL,
 CONSTRAINT [PK_Utente] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Dettaglio_Ordine] ON 

INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (10, 1, 6, 100)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (21, 2, 7, 10)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (22, 3, 8, 20)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (23, 4, 9, 30)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (24, 5, 10, 40)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (25, 6, 11, 50)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (26, 7, 12, 60)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (27, 8, 13, 70)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (28, 9, 14, 80)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (29, 10, 15, 90)
SET IDENTITY_INSERT [dbo].[Dettaglio_Ordine] OFF
GO
SET IDENTITY_INSERT [dbo].[Ordine] ON 

INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (1, 1, CAST(N'2024-05-20T10:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (2, 2, CAST(N'2024-05-20T10:30:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (3, 3, CAST(N'2024-05-20T11:00:00.000' AS DateTime), NULL, 3)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (4, 4, CAST(N'2024-05-20T11:30:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (5, 5, CAST(N'2024-05-20T12:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (6, 6, CAST(N'2024-05-20T12:30:00.000' AS DateTime), NULL, 3)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (7, 7, CAST(N'2024-05-20T13:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (8, 8, CAST(N'2024-05-20T13:30:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (9, 9, CAST(N'2024-05-20T14:00:00.000' AS DateTime), NULL, 3)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (10, 10, CAST(N'2024-05-20T14:30:00.000' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Ordine] OFF
GO
SET IDENTITY_INSERT [dbo].[Prodotto] ON 

INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (6, N'Laptop', N'Laptop potente e versatile.', 25)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (7, N'Smartphone', N'Smartphone con fotocamera avanzata.', 50)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (8, N'Tablet', N'Tablet portatile con display ad alta risoluzione.', 30)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (9, N'Smartwatch', N'Orologio intelligente con monitoraggio attività.', 40)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (10, N'Cuffie Bluetooth', N'Cuffie wireless con cancellazione del rumore.', 75)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (11, N'Monitor', N'Monitor da 27 pollici con risoluzione 4K.', 20)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (12, N'Mouse', N'Mouse ergonomico con sensore ad alta precisione.', 100)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (13, N'Tastiera', N'Tastiera meccanica con retroilluminazione RGB.', 60)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (14, N'Stampante', N'Stampante multifunzione con scanner.', 15)
INSERT [dbo].[Prodotto] ([ID], [Nome], [Descrizione], [Quantità]) VALUES (15, N'Fotocamera', N'Fotocamera digitale con obiettivo intercambiabile.', 10)
SET IDENTITY_INSERT [dbo].[Prodotto] OFF
GO
SET IDENTITY_INSERT [dbo].[Stato_Ordine] ON 

INSERT [dbo].[Stato_Ordine] ([ID], [Descrizione]) VALUES (1, N'CREATO')
INSERT [dbo].[Stato_Ordine] ([ID], [Descrizione]) VALUES (2, N'AGGIORNATO')
INSERT [dbo].[Stato_Ordine] ([ID], [Descrizione]) VALUES (3, N'CHIUSO')
SET IDENTITY_INSERT [dbo].[Stato_Ordine] OFF
GO
SET IDENTITY_INSERT [dbo].[Utente] ON 

INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (1, N'Ferrante', N'Eliana', CAST(N'1940-11-26' AS Date), N'Curci del friuli', N'VB', N'F', N'FRRLNE40S66H501A', CAST(N'2024-01-03T13:20:16.000' AS DateTime), N'stradivariaria@libero.it', N'00c39d5656b6e738')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (2, N'Salieri', N'Edoardo', CAST(N'1942-10-24' AS Date), N'Quarto Liliana', N'RG', N'M', N'SLRDRD42R24H501A', CAST(N'2024-03-04T04:14:34.000' AS DateTime), N'olga96@monaco.it', N'6de31f9921b08731')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (3, N'Castioni', N'Alessandro', CAST(N'1990-05-24' AS Date), N'Leonardo terme', N'AO', N'M', N'CSTLSN90E24H501A', CAST(N'2024-02-08T07:01:10.000' AS DateTime), N'bmastandrea@gmail.com', N'49b0ccb6a598ef2e')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (4, N'Fantoni', N'Nedda', CAST(N'1957-03-06' AS Date), N'San Vittoria', N'PT', N'F', N'FNTNDD57C46H501A', CAST(N'2024-04-15T12:58:45.000' AS DateTime), N'lolita99@tim.it', N'e55e8e2545cf8c04')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (5, N'Ceravolo', N'Flavio', CAST(N'1979-10-10' AS Date), N'Monteverdi umbro', N'RG', N'F', N'CRVFLV79R50H501A', CAST(N'2024-04-15T17:22:54.000' AS DateTime), N'rita17@libero.it', N'00e63c496b9c245d')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (6, N'Corsini', N'Giulia', CAST(N'1941-11-29' AS Date), N'Settimo Cecilia nell emilia', N'VB', N'F', N'CRSGLG41S69H501A', CAST(N'2024-01-02T09:24:17.000' AS DateTime), N'gullottadonna@yahoo.com', N'd70bc2b07d740892')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (7, N'Barzini', N'Matteo', CAST(N'1950-07-13' AS Date), N'Pulci sardo', N'EN', N'F', N'BRZMTT50L53H501A', CAST(N'2024-04-26T21:09:49.000' AS DateTime), N'orsinibenvenuto@alfieri-donatoni.it', N'c511c07c1c80c8ec')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (8, N'Zola', N'Alfio', CAST(N'1990-03-11' AS Date), N'Toscanini calabro', N'AL', N'F', N'ZLOLFA90C51H501A', CAST(N'2024-04-27T12:23:15.000' AS DateTime), N'dfalcone@fastwebnet.it', N'61907316d1d16ac8')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (9, N'Campano', N'Berenice', CAST(N'1975-01-01' AS Date), N'Scaramucci terme', N'OR', N'F', N'CMPBNC75A41H501A', CAST(N'2024-01-23T15:46:59.000' AS DateTime), N'msaracino@hotmail.com', N'36c277f0bf02d77f')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Data_Nascita], [Citta_Nascita], [Provincia_Nascita], [Sesso], [Codice_Fiscale], [Data_Registrazione], [Email], [Password]) VALUES (10, N'Iacovelli', N'Greco', CAST(N'1960-07-06' AS Date), N'Settimo Michela a mare', N'LI', N'F', N'CVLGRC60L46H501A', CAST(N'2024-03-21T19:13:21.000' AS DateTime), N'piergiorgio04@prati-benedetti.it', N'660730a85fc533e9')
SET IDENTITY_INSERT [dbo].[Utente] OFF
GO
ALTER TABLE [dbo].[Dettaglio_Ordine]  WITH CHECK ADD  CONSTRAINT [FK_Dettaglio_Ordine_Dettaglio_Ordine] FOREIGN KEY([FK_ID_Ordine])
REFERENCES [dbo].[Ordine] ([ID])
GO
ALTER TABLE [dbo].[Dettaglio_Ordine] CHECK CONSTRAINT [FK_Dettaglio_Ordine_Dettaglio_Ordine]
GO
ALTER TABLE [dbo].[Dettaglio_Ordine]  WITH CHECK ADD  CONSTRAINT [FK_Dettaglio_Ordine_Prodotto] FOREIGN KEY([FK_ID_Prodotto])
REFERENCES [dbo].[Prodotto] ([ID])
GO
ALTER TABLE [dbo].[Dettaglio_Ordine] CHECK CONSTRAINT [FK_Dettaglio_Ordine_Prodotto]
GO
ALTER TABLE [dbo].[Ordine]  WITH CHECK ADD  CONSTRAINT [FK_Ordine_Stato_Ordine] FOREIGN KEY([FK_ID_Stato])
REFERENCES [dbo].[Stato_Ordine] ([ID])
GO
ALTER TABLE [dbo].[Ordine] CHECK CONSTRAINT [FK_Ordine_Stato_Ordine]
GO
ALTER TABLE [dbo].[Ordine]  WITH CHECK ADD  CONSTRAINT [FK_Ordine_Utente] FOREIGN KEY([FK_ID_Utente])
REFERENCES [dbo].[Utente] ([ID])
GO
ALTER TABLE [dbo].[Ordine] CHECK CONSTRAINT [FK_Ordine_Utente]
GO
USE [master]
GO
ALTER DATABASE [AcademyShopDB] SET  READ_WRITE 
GO
