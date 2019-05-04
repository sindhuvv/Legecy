CREATE TABLE [dbo].[Security_EmployeeDelegation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentUpn] [int] NOT NULL,
	[DelegateUpn] [int] NOT NULL,
	[EffectiveStartDate] [datetime2](7) NULL,
	[EffectiveEndDate] [datetime2](7) NULL,
	[Comments] [varchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDateTime] [datetime2] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedDateTime] [datetime2] NOT NULL,
 CONSTRAINT [Pk_Tms_Security_EmployeeDelegation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


