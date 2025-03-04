USE [sizefintechdb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/02/2025 08:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnticipationLimits]    Script Date: 10/02/2025 08:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnticipationLimits](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RevenueMinimun] [decimal](12, 2) NOT NULL,
	[RevenueMaximum] [decimal](12, 2) NULL,
	[AnticipationPercent] [decimal](5, 4) NOT NULL,
	[IndustryId] [bigint] NOT NULL,
 CONSTRAINT [PK_AnticipationLimits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Anticipations]    Script Date: 10/02/2025 08:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Anticipations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Limit] [decimal](12, 2) NOT NULL,
	[NetTotal] [decimal](12, 2) NOT NULL,
	[GrossTotal] [decimal](12, 2) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_Anticipations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Industries]    Script Date: 10/02/2025 08:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Industries](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Industries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 10/02/2025 08:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NOT NULL,
	[NetAmount] [decimal](12, 2) NOT NULL,
	[GrossAmount] [decimal](12, 2) NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[AnticipationId] [bigint] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/02/2025 08:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserIdentifier] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CNPJ] [nvarchar](max) NOT NULL,
	[MonthlyRevenue] [decimal](12, 2) NOT NULL,
	[IndustryId] [bigint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AnticipationLimits]  WITH CHECK ADD  CONSTRAINT [FK_AnticipationLimits_Industries_IndustryId] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AnticipationLimits] CHECK CONSTRAINT [FK_AnticipationLimits_Industries_IndustryId]
GO
ALTER TABLE [dbo].[Anticipations]  WITH CHECK ADD  CONSTRAINT [FK_Anticipations_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Anticipations] CHECK CONSTRAINT [FK_Anticipations_Users_UserId]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Anticipations_AnticipationId] FOREIGN KEY([AnticipationId])
REFERENCES [dbo].[Anticipations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Anticipations_AnticipationId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Industries_IndustryId] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Industries_IndustryId]
GO
