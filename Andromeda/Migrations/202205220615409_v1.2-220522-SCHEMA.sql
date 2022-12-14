USE [Andromeda]
GO
/****** Object:  User [guiag_sa]    Script Date: 16/6/2022 20:32:37 ******/
CREATE USER [guiag_sa] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[guiag_sa]
GO
/****** Object:  User [MVD]    Script Date: 16/6/2022 20:32:37 ******/
CREATE USER [MVD] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [guiag_sa]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [guiag_sa]
GO
ALTER ROLE [db_datareader] ADD MEMBER [guiag_sa]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [guiag_sa]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [MVD]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [MVD]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MVD]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [MVD]
GO
/****** Object:  Schema [guiag_sa]    Script Date: 16/6/2022 20:32:37 ******/
CREATE SCHEMA [guiag_sa]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryDescription] [nvarchar](max) NULL,
	[CategoryImageName] [nvarchar](max) NULL,
	[CategoryOrder] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Changes]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Changes](
	[ChangeObjectId] [nvarchar](128) NOT NULL,
	[ChangeLastAt] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Changes] PRIMARY KEY CLUSTERED 
(
	[ChangeObjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentLines]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentLines](
	[DocumentLineId] [int] NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[DocumentLineItemId] [bigint] NOT NULL,
	[DocumentLineItemDescription] [nvarchar](max) NULL,
	[DocumentLineQty] [int] NOT NULL,
	[DocumentLineItemPrice] [real] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentLines] PRIMARY KEY CLUSTERED 
(
	[DocumentLineId] ASC,
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[DocumentId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentLetter] [nvarchar](max) NULL,
	[DocumentNumber] [bigint] NOT NULL,
	[EntityId] [int] NOT NULL,
	[DocumentStatus] [int] NOT NULL,
	[DocumentTotalAmount] [real] NOT NULL,
	[DocumentLinesQty] [int] NOT NULL,
	[DocumentType] [int] NOT NULL,
	[DocumentNumerator] [int] NOT NULL,
	[DocumentDate] [datetime] NOT NULL,
	[DocumentPreparedAt] [datetime] NOT NULL,
	[DocumentBilledAt] [datetime] NOT NULL,
	[DocumentDeliveredAt] [datetime] NOT NULL,
	[DocumentLastUpdateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Documents] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entities]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entities](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[EntityDescription] [nvarchar](max) NULL,
	[EntityLegalName] [nvarchar](max) NULL,
	[EntityAddress] [nvarchar](max) NULL,
	[EntityLocation] [nvarchar](max) NULL,
	[EntityType] [int] NOT NULL,
	[EntityContact] [nvarchar](max) NULL,
	[EntityPhone] [nvarchar](max) NULL,
	[EntityMail] [nvarchar](max) NULL,
	[EntityInternalCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Entities] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inv]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inv](
	[InventoryId] [int] IDENTITY(1,1) NOT NULL,
	[InventoryDescription] [nvarchar](max) NULL,
	[InventoryType] [int] NOT NULL,
	[InventoryStatus] [int] NOT NULL,
	[InventoryCreationDate] [datetime] NOT NULL,
	[InventoryEndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Inv] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvLines]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvLines](
	[InventoryId] [int] NOT NULL,
	[InventoryLineId] [int] NOT NULL,
	[InventoryLineItemId] [int] NOT NULL,
	[InventoryLineQty] [int] NOT NULL,
	[InventoryLinePU] [int] NOT NULL,
	[InventoryLineUser] [nvarchar](max) NULL,
	[InventoryLineDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.InvLines] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC,
	[InventoryLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemDescription] [nvarchar](max) NULL,
	[ItemExternalCode] [nvarchar](max) NULL,
	[ItemInternalCode] [nvarchar](max) NULL,
	[ItemBarcode] [nvarchar](max) NULL,
	[ItemPrice] [real] NOT NULL,
	[ItemCost] [real] NOT NULL,
	[ItemSugg] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[ItemSuggLow] [int] NOT NULL,
	[ItemSuggHigh] [int] NOT NULL,
	[ItemSuggStep] [int] NOT NULL,
	[ItemOrder] [int] NOT NULL,
	[ItemInternalDescription] [nvarchar](max) NULL,
	[ItemPackUnit] [int] NOT NULL,
	[ItemDisabled] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[LogAt] [datetime] NOT NULL,
	[LogCode] [nvarchar](max) NULL,
	[LogDescription] [nvarchar](max) NULL,
	[LogValue] [bigint] NOT NULL,
	[LogUser] [nvarchar](max) NULL,
	[LogOldValue] [nvarchar](max) NULL,
	[LogNewValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Logs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Numerator]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Numerator](
	[NumeratorLast] [bigint] NOT NULL,
	[NumeratorCode] [nvarchar](max) NULL,
	[NumeratorId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_dbo.Numerator] PRIMARY KEY CLUSTERED 
(
	[NumeratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[UserRoleIsSeller] [bit] NOT NULL,
	[UserRoleIsPicker] [bit] NOT NULL,
	[UserRoleIsDeliverer] [bit] NOT NULL,
	[UserRoleIsBiller] [bit] NOT NULL,
	[UserRoleIsAdmin] [bit] NOT NULL,
	[UserRoleIsSuperAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16/6/2022 20:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[UserPassword] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ((0)) FOR [CategoryOrder]
GO
ALTER TABLE [dbo].[Changes] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ChangeLastAt]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF__Documents__Docum__1FCDBCEB]  DEFAULT ((0)) FOR [DocumentType]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF__Documents__Docum__21B6055D]  DEFAULT ((0)) FOR [DocumentNumerator]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DocumentDate]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DocumentPreparedAt]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DocumentBilledAt]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DocumentDeliveredAt]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DocumentLastUpdateAt]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((0)) FOR [ItemSuggLow]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((0)) FOR [ItemSuggHigh]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((0)) FOR [ItemSuggStep]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((0)) FOR [ItemOrder]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((0)) FOR [ItemPackUnit]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((0)) FOR [ItemDisabled]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT ((0)) FOR [UserRoleIsBiller]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT ((0)) FOR [UserRoleIsAdmin]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT ((0)) FOR [UserRoleIsSuperAdmin]
GO
ALTER TABLE [dbo].[DocumentLines]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentLines_dbo.Documents_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Documents] ([DocumentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentLines] CHECK CONSTRAINT [FK_dbo.DocumentLines_dbo.Documents_DocumentId]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Documents_dbo.Entities_EntityId] FOREIGN KEY([EntityId])
REFERENCES [dbo].[Entities] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_dbo.Documents_dbo.Entities_EntityId]
GO
ALTER TABLE [dbo].[InvLines]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InvLines_dbo.Inv_InventoryId] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inv] ([InventoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvLines] CHECK CONSTRAINT [FK_dbo.InvLines_dbo.Inv_InventoryId]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Items_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_dbo.Items_dbo.Categories_CategoryId]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Items_dbo.Entities_EntityId] FOREIGN KEY([EntityId])
REFERENCES [dbo].[Entities] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_dbo.Items_dbo.Entities_EntityId]
GO
