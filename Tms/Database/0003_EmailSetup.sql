-- Create EmailAttachment, EmailQueue and EmailLog  table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = N'EmailQueue')
BEGIN

CREATE TABLE [dbo].[EmailQueue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAddress] [varchar](max) NULL,
	[ToList] [varchar](max) NULL,
	[CCList] [varchar](max) NULL,
	[BCCList] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[Body] [nvarchar](max) NOT NULL,
	[BatchId] [char](36) NULL,
	[EmailType] [varchar](max) NOT NULL,
	[SendOnDate] [datetime] NULL,
	[CreatedBy] int NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[LastUpdatedBy] int NOT NULL,
	[LastUpdatedDateTime] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = N'EmailAttachment')
BEGIN

CREATE TABLE [dbo].[EmailAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailQueueId]  [int] NOT NULL,
	[Filename] [varchar](255) NOT NULL,
	[ContentType] [varchar](255) NOT NULL,
	[FileContents] [varbinary](max) NOT NULL,
	[CreatedBy] int NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[LastUpdatedBy] int NOT NULL,
	[LastUpdatedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EmailAttachment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[EmailAttachment]  WITH CHECK ADD  CONSTRAINT [Fk_EmailAttachment_Id] FOREIGN KEY([EmailQueueId])
REFERENCES [dbo].[EmailQueue] ([Id])

END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = N'EmailLog')
BEGIN

CREATE TABLE [dbo].[EmailLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAddress] [varchar](max) NOT NULL,
	[ToList] [varchar](max) NOT NULL,
	[CCList] [varchar](max) NOT NULL,
	[BCCList] [varchar](max) NOT NULL,
	[Subject] [varchar](max) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[SendOnDate] [datetime] NOT NULL,
	[CreatedBy] int NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[LastUpdatedBy] int NOT NULL,
	[LastUpdatedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PartnerGoals_PDF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END
GO