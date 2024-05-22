USE [master]
GO
/****** Object:  Database [AcademyShopDB]    Script Date: 22/05/2024 10:34:50 ******/
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
/****** Object:  Table [dbo].[Dettaglio_Ordine]    Script Date: 22/05/2024 10:34:51 ******/
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
/****** Object:  Table [dbo].[Ordine]    Script Date: 22/05/2024 10:34:51 ******/
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
/****** Object:  Table [dbo].[Prodotto]    Script Date: 22/05/2024 10:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prodotto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [varchar](50) NOT NULL,
	[Quantita] [int] NOT NULL,
 CONSTRAINT [PK_Prodotto] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stato_Ordine]    Script Date: 22/05/2024 10:34:51 ******/
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
/****** Object:  Table [dbo].[Utente]    Script Date: 22/05/2024 10:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utente](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cognome] [varchar](50) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Codice_Fiscale] [char](16) NOT NULL,
	[Data_Registrazione] [datetime] NOT NULL,
	[Password] [varchar](16) NOT NULL,
 CONSTRAINT [PK_Utente] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Dettaglio_Ordine] ON 

INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (1, 1, 1, 10)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (2, 2, 2, 20)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (3, 3, 3, 30)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (4, 4, 4, 40)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (5, 5, 5, 50)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (6, 6, 6, 60)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (7, 7, 7, 70)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (8, 8, 8, 80)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (9, 9, 9, 90)
INSERT [dbo].[Dettaglio_Ordine] ([ID], [FK_ID_Ordine], [FK_ID_Prodotto], [Quantita]) VALUES (10, 10, 10, 100)
SET IDENTITY_INSERT [dbo].[Dettaglio_Ordine] OFF
GO
SET IDENTITY_INSERT [dbo].[Ordine] ON 

INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (1, 3, CAST(N'2024-05-20T10:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (2, 4, CAST(N'2024-05-20T10:30:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (3, 5, CAST(N'2024-05-20T11:00:00.000' AS DateTime), NULL, 3)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (4, 6, CAST(N'2024-05-20T11:30:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (5, 7, CAST(N'2024-05-20T12:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (6, 8, CAST(N'2024-05-20T12:30:00.000' AS DateTime), NULL, 3)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (7, 9, CAST(N'2024-05-20T13:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (8, 10, CAST(N'2024-05-20T13:30:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (9, 11, CAST(N'2024-05-20T14:00:00.000' AS DateTime), NULL, 3)
INSERT [dbo].[Ordine] ([ID], [FK_ID_Utente], [Data_Registrazione], [Data_Aggiornamento], [FK_ID_Stato]) VALUES (10, 12, CAST(N'2024-05-20T14:30:00.000' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Ordine] OFF
GO
SET IDENTITY_INSERT [dbo].[Prodotto] ON 

INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (1, N'Smartphone Samsung Galaxy', 100)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (2, N'Laptop Dell XPS 13', 50)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (3, N'Cuffie Bose QuietComfort', 200)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (4, N'Smartwatch Apple Watch Series 6', 150)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (5, N'Tablet iPad Air', 120)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (6, N'Fotocamera Canon EOS R6', 80)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (7, N'Monitor LG UltraFine', 60)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (8, N'Stampante HP LaserJet Pro', 90)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (9, N'Router Netgear Nighthawk', 110)
INSERT [dbo].[Prodotto] ([ID], [Descrizione], [Quantita]) VALUES (10, N'Hard Disk Esterno Seagate', 140)
SET IDENTITY_INSERT [dbo].[Prodotto] OFF
GO
SET IDENTITY_INSERT [dbo].[Stato_Ordine] ON 

INSERT [dbo].[Stato_Ordine] ([ID], [Descrizione]) VALUES (1, N'CREATO')
INSERT [dbo].[Stato_Ordine] ([ID], [Descrizione]) VALUES (2, N'AGGIORNATO')
INSERT [dbo].[Stato_Ordine] ([ID], [Descrizione]) VALUES (3, N'CHIUSO')
SET IDENTITY_INSERT [dbo].[Stato_Ordine] OFF
GO
SET IDENTITY_INSERT [dbo].[Utente] ON 

INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (3, N'Rossi', N'Mario', N'RSSMRA80A01H501Z', CAST(N'2023-01-01T10:00:00.000' AS DateTime), N'password1')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (4, N'Bianchi', N'Luigi', N'BCHLGU80A01F205X', CAST(N'2023-02-15T11:30:00.000' AS DateTime), N'password2')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (5, N'Verdi', N'Carla', N'VRDCRL80A01L219Q', CAST(N'2023-03-20T14:45:00.000' AS DateTime), N'password3')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (6, N'Neri', N'Giorgio', N'NRIGRG80A01D969F', CAST(N'2023-04-25T09:15:00.000' AS DateTime), N'password4')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (7, N'Gialli', N'Elena', N'GLLELN80A01H501Y', CAST(N'2023-05-30T08:20:00.000' AS DateTime), N'password5')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (8, N'Blu', N'Andrea', N'BLUAND80A01C273H', CAST(N'2023-06-10T16:55:00.000' AS DateTime), N'password6')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (9, N'Marroni', N'Sofia', N'MRRSOF80A01G225T', CAST(N'2023-07-14T19:00:00.000' AS DateTime), N'password7')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (10, N'Viola', N'Luca', N'VIOLUC80A01T773P', CAST(N'2023-08-18T12:40:00.000' AS DateTime), N'password8')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (11, N'Rosa', N'Paolo', N'ROSPLP80A01R523W', CAST(N'2023-09-22T17:25:00.000' AS DateTime), N'password9')
INSERT [dbo].[Utente] ([ID], [Cognome], [Nome], [Codice_Fiscale], [Data_Registrazione], [Password]) VALUES (12, N'Argento', N'Francesca', N'ARGFNC80A01S096U', CAST(N'2023-10-05T13:15:00.000' AS DateTime), N'password10')
SET IDENTITY_INSERT [dbo].[Utente] OFF
GO
ALTER TABLE [dbo].[Dettaglio_Ordine]  WITH CHECK ADD  CONSTRAINT [FK_Dettaglio_Ordine_Ordine] FOREIGN KEY([FK_ID_Ordine])
REFERENCES [dbo].[Ordine] ([ID])
GO
ALTER TABLE [dbo].[Dettaglio_Ordine] CHECK CONSTRAINT [FK_Dettaglio_Ordine_Ordine]
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
