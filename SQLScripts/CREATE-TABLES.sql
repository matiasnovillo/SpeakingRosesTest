USE [TaskDB]
GO

/****** Object:  Table [dbo].[System.Failure]    Script Date: 25/06/2024 14:56:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[System.Failure]') AND type in (N'U'))
DROP TABLE [dbo].[System.Failure]
GO

/****** Object:  Table [dbo].[SpeakingRosesTest.Tasks]    Script Date: 25/06/2024 14:56:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpeakingRosesTest.Tasks]') AND type in (N'U'))
DROP TABLE [dbo].[SpeakingRosesTest.Tasks]
GO

/****** Object:  Table [dbo].[SpeakingRosesTest.Status]    Script Date: 25/06/2024 14:56:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpeakingRosesTest.Status]') AND type in (N'U'))
DROP TABLE [dbo].[SpeakingRosesTest.Status]
GO

/****** Object:  Table [dbo].[SpeakingRosesTest.Priority]    Script Date: 25/06/2024 14:56:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpeakingRosesTest.Priority]') AND type in (N'U'))
DROP TABLE [dbo].[SpeakingRosesTest.Priority]
GO

/****** Object:  Table [dbo].[SpeakingRosesTest.Priority]    Script Date: 25/06/2024 14:56:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SpeakingRosesTest.Priority](
	[PriorityId] [int] IDENTITY(1,1) NOT NULL,
	[Active] [tinyint] NULL,
	[DateTimeCreation] [datetime] NULL,
	[DateTimeLastModification] [datetime] NULL,
	[UserCreationId] [int] NULL,
	[UserLastModificationId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SpeakingRosesTestPriority] PRIMARY KEY CLUSTERED 
(
	[PriorityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SpeakingRosesTest.Status]    Script Date: 25/06/2024 14:56:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SpeakingRosesTest.Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[Active] [tinyint] NULL,
	[DateTimeCreation] [datetime] NULL,
	[DateTimeLastModification] [datetime] NULL,
	[UserCreationId] [int] NULL,
	[UserLastModificationId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SpeakingRosesTestStatus] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SpeakingRosesTest.Tasks]    Script Date: 25/06/2024 14:56:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SpeakingRosesTest.Tasks](
	[TasksId] [int] IDENTITY(1,1) NOT NULL,
	[Active] [tinyint] NULL,
	[DateTimeCreation] [datetime] NULL,
	[DateTimeLastModification] [datetime] NULL,
	[UserCreationId] [int] NULL,
	[UserLastModificationId] [int] NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[PriorityId] [int] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK_SpeakingRosesTestTasks] PRIMARY KEY CLUSTERED 
(
	[TasksId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[System.Failure]    Script Date: 25/06/2024 14:56:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[System.Failure](
	[FailureId] [int] IDENTITY(1,1) NOT NULL,
	[HTTPCode] [int] NULL,
	[EmergencyLevel] [int] NULL,
	[Message] [varchar](8000) NULL,
	[StackTrace] [varchar](8000) NULL,
	[Source] [varchar](8000) NULL,
	[Comment] [varchar](8000) NULL,
	[Active] [tinyint] NULL,
	[UserCreationId] [int] NULL,
	[UserLastModificationId] [int] NULL,
	[DateTimeCreation] [datetime] NULL,
	[DateTimeLastModification] [datetime] NULL,
 CONSTRAINT [PK_BasicCoreFailure] PRIMARY KEY CLUSTERED 
(
	[FailureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


