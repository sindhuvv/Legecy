USE [Tms]
GO

CREATE TABLE [dbo].[Employee](
	[UPN] [int] NOT NULL,
	[Status] [char](1) NOT NULL,
	[HRName] [varchar](50) NOT NULL,
	[BusinessUnitKey] [char](5) NOT NULL,
	[RankId] [int] NOT NULL,
	[DomainLogin] [varchar](50) NULL,
	[SupervisorUPN] [int] NULL,
	[AdminAssistantUPN] [int] NULL,
	[PracticeKey] [varchar](15) NOT NULL,
	[SapId] [varchar](50) NULL,
	[DsId] [varchar](12) NULL,
	[Title] [varchar](50) NOT NULL,
	[PreferredFirstName] [varchar](50) NULL,
	[PreferredLastName] [varchar](50) NULL,
	[PreferredMiddleName] [varchar](50) NULL,
	[State] [varchar](20) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[StandardHours] [int] NOT NULL,
	[Sublevel] [int] NOT NULL,
	[CostCenterKey] [varchar](10) NULL,
	[LocalJobLevel] [varchar](2) NULL,
	[BusinessAreaKey] [char](4) NULL,
	[PrimaryHRSegmentKey] [char](3) NULL,
	[SecondaryHRSegmentKey] [char](3) NULL,
	[TerminationDate] [datetime] NULL,
	[LastRehireDate] [datetime] NULL,
	[HireDate] [datetime] NOT NULL,
	[TempEmployee] [bit] NOT NULL,
	[SeniorityDate] [datetime] NOT NULL,
	[AdjustedSeniorityDate] [datetime] NOT NULL,
	[RankEntryDate] [datetime] NOT NULL,
	[Email] [varchar](255) NULL,
	[EmailDisplayName] [varchar](255) NULL,
	[BillRate] [int] NOT NULL,
	[IsPromotedToPartner] [bit] NULL,
	[PCAOBKey] [varchar](10) NULL,
 CONSTRAINT [PK_Kata_Employee_Active_Next] PRIMARY KEY CLUSTERED 
(
	[UPN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Employee] 
SELECT * FROM [Hive].[dbo].[Kata_Employee] WHERE Status='A'