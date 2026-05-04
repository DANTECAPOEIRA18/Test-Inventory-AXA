USE [InventoryDB]
GO

/****** Objeto: Table [dbo].[AuditLog] Fecha de script: 3/05/2026 11:08:02 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuditLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityName] [nvarchar](100) NULL,
	[Action] [nvarchar](50) NULL,
	[EntityId] [uniqueidentifier] NULL,
	[OldValue] [nvarchar](max) NULL,
	[NewValue] [nvarchar](max) NULL,
	[PerformedBy] [nvarchar](100) NULL,
	[PerformedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AuditLog] ADD  DEFAULT (getdate()) FOR [PerformedAt]
GO


