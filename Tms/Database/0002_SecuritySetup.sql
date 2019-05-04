CREATE TABLE [dbo].[SecurityRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Practice] [varchar](10) NULL,
	[RoleName] [varchar](30) NULL,
	[Description] [varchar](MAX) NULL,
	[PermFlag] [int] NULL,
	[IsBuiltIn] [BIT] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDateTime] [DATETIME2] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedDateTime] [DATETIME2] NOT NULL,
 CONSTRAINT [Pk_Tms_SecurityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


INSERT [dbo].[SecurityRole] 
       SELECT 'Audit','TrainingViewers','Training Viewers',546, 0, 0, GetDate(), 0, GetDate()
INSERT [dbo].[SecurityRole] 
       SELECT 'Audit','SessionAdmin','Session Admin',	482, 0, 0, GetDate(), 0, GetDate()
INSERT [dbo].[SecurityRole] 
       SELECT 'Audit','RosterAdmin','Roster Admin',	7714, 0, 0, GetDate(), 0, GetDate()
INSERT [dbo].[SecurityRole] 
       SELECT 'Audit','Trainers','Trainers',	7714, 0, 0, GetDate(), 0, GetDate()


GO


CREATE TABLE [dbo].[SecurityUserRole](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Upn] [int] NULL,
	[SecurityRoleId] [INT] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDateTime] [DATETIME2] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedDateTime] [DATETIME2] NOT NULL,
 CONSTRAINT [Pk_Tms_SecurityUserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SecurityUserRole]  WITH CHECK ADD  CONSTRAINT [Fk_Tms_SecurityUserRole_SecurityRoleId] FOREIGN KEY([SecurityRoleId])
REFERENCES [dbo].[SecurityRole] ([Id])


INSERT [SecurityUserRole] 
       SELECT '2798283' ,1, 0, GetDate(), 0, GetDate() 

INSERT [SecurityUserRole] 
       SELECT '2798283' ,2, 0, GetDate(), 0, GetDate()
	   
INSERT [SecurityUserRole] 
       SELECT '2809276' ,1, 0, GetDate(), 0, GetDate()

INSERT [SecurityUserRole] 
       SELECT '2809276' ,2, 0, GetDate(), 0, GetDate()


INSERT [SecurityUserRole] 
       SELECT '3011399' ,1, 0, GetDate(), 0, GetDate() 

INSERT [SecurityUserRole] 
       SELECT '3011399' ,2, 0, GetDate(), 0, GetDate()
	   
INSERT [SecurityUserRole] 
       SELECT '2749371' ,1, 0, GetDate(), 0, GetDate() 

INSERT [SecurityUserRole] 
       SELECT '2749371' ,2, 0, GetDate(), 0, GetDate()
	   
INSERT [SecurityUserRole] 
       SELECT '2798299' ,1, 0, GetDate(), 0, GetDate() 

INSERT [SecurityUserRole] 
       SELECT '2798299' ,2, 0, GetDate(), 0, GetDate()