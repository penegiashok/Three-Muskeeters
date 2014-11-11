USE [MytestDB]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_Answer]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_Answer]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionOption_Answer]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionOption]'))
ALTER TABLE [dbo].[QuestionOption] DROP CONSTRAINT [FK_QuestionOption_Answer]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AnswerImages_Answer]') AND parent_object_id = OBJECT_ID(N'[dbo].[AnswerImages]'))
ALTER TABLE [dbo].[AnswerImages] DROP CONSTRAINT [FK_AnswerImages_Answer]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Answer]') AND type in (N'U'))
DROP TABLE [dbo].[Answer]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Answer](
	[AnswerID] [int] NOT NULL IDENTITY(1, 1),
	[AnswerDesc] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AnswerImages_Answer]') AND parent_object_id = OBJECT_ID(N'[dbo].[AnswerImages]'))
ALTER TABLE [dbo].[AnswerImages] DROP CONSTRAINT [FK_AnswerImages_Answer]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnswerImages]') AND type in (N'U'))
DROP TABLE [dbo].[AnswerImages]
GO

CREATE TABLE [dbo].[AnswerImages](
	[ImageId] [int] NOT NULL IDENTITY(1, 1),
	[AnswerID] [int] NOT NULL,
	[ImageUrl] [varchar](max) NOT NULL,
 CONSTRAINT [PK_AnswerImages] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[AnswerImages]  WITH CHECK ADD  CONSTRAINT [FK_AnswerImages_Answer] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[Answer] ([AnswerID])
GO

ALTER TABLE [dbo].[AnswerImages] CHECK CONSTRAINT [FK_AnswerImages_Answer]
GO


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_Question]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionImage_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionImage]'))
ALTER TABLE [dbo].[QuestionImage] DROP CONSTRAINT [FK_QuestionImage_Question]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionOption_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionOption]'))
ALTER TABLE [dbo].[QuestionOption] DROP CONSTRAINT [FK_QuestionOption_Question]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question]') AND type in (N'U'))
DROP TABLE [dbo].[Question]
GO

CREATE TABLE [dbo].[Question](
	[QuestionID] [int] NOT NULL IDENTITY(1, 1),
	[QuestionDesc] [varchar](max) NOT NULL,
	[QuestionOrder] [int] NOT NULL,
	[AnswerExplanation] [varchar](max) NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_QuizCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_QuizCategory]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_QuizCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserQuiz]'))
ALTER TABLE [dbo].[UserQuiz] DROP CONSTRAINT [FK_User_QuizCategory]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizCategory]') AND type in (N'U'))
DROP TABLE [dbo].[QuizCategory]
GO

CREATE TABLE [dbo].[QuizCategory](
	[CategoryID] [int] NOT NULL IDENTITY(1, 1),
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_QuizCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_Answer]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_Answer]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_Question]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_QuizCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_QuizCategory]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]') AND type in (N'U'))
DROP TABLE [dbo].[QuestionAnswer]
GO

CREATE TABLE [dbo].[QuestionAnswer](
	[QAID] [int] NOT NULL IDENTITY(1, 1),
	[QuestionID] [int] NOT NULL,
	[AnswerID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_QuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[QAID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[QuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuestionAnswer_Answer] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[Answer] ([AnswerID])
GO

ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_Answer]
GO

ALTER TABLE [dbo].[QuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuestionAnswer_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([QuestionID])
GO

ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_Question]
GO

ALTER TABLE [dbo].[QuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuestionAnswer_QuizCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[QuizCategory] ([CategoryID])
GO

ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_QuizCategory]
GO


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionImage_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionImage]'))
ALTER TABLE [dbo].[QuestionImage] DROP CONSTRAINT [FK_QuestionImage_Question]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionImage]') AND type in (N'U'))
DROP TABLE [dbo].[QuestionImage]
GO

CREATE TABLE [dbo].[QuestionImage](
	[ImageID] [int] NOT NULL IDENTITY(1, 1),
	[QuestionID] [int] NOT NULL,
	[ImageURL] [varchar](max) NOT NULL,
 CONSTRAINT [PK_QuestionImage] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[QuestionImage]  WITH CHECK ADD  CONSTRAINT [FK_QuestionImage_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([QuestionID])
GO

ALTER TABLE [dbo].[QuestionImage] CHECK CONSTRAINT [FK_QuestionImage_Question]
GO


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionOption_Answer]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionOption]'))
ALTER TABLE [dbo].[QuestionOption] DROP CONSTRAINT [FK_QuestionOption_Answer]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionOption_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionOption]'))
ALTER TABLE [dbo].[QuestionOption] DROP CONSTRAINT [FK_QuestionOption_Question]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionOption]') AND type in (N'U'))
DROP TABLE [dbo].[QuestionOption]
GO

CREATE TABLE [dbo].[QuestionOption](
	[OptionID] [int] NOT NULL IDENTITY(1, 1),
	[QuestionID] [int] NOT NULL,
	[choiceID] [int] NOT NULL,
 CONSTRAINT [PK_QuestionOption] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[QuestionOption]  WITH CHECK ADD  CONSTRAINT [FK_QuestionOption_Answer] FOREIGN KEY([choiceID])
REFERENCES [dbo].[Answer] ([AnswerID])
GO

ALTER TABLE [dbo].[QuestionOption] CHECK CONSTRAINT [FK_QuestionOption_Answer]
GO

ALTER TABLE [dbo].[QuestionOption]  WITH CHECK ADD  CONSTRAINT [FK_QuestionOption_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([QuestionID])
GO

ALTER TABLE [dbo].[QuestionOption] CHECK CONSTRAINT [FK_QuestionOption_Question]
GO



IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_QuizCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserQuiz]'))
ALTER TABLE [dbo].[UserQuiz] DROP CONSTRAINT [FK_User_QuizCategory]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserQuiz]') AND type in (N'U'))
DROP TABLE [dbo].[UserQuiz]
GO

CREATE TABLE [dbo].[UserQuiz](
	[QuizUserID] [int] NOT NULL IDENTITY(1, 1),
	[Name] [varchar](50) NOT NULL,
	[datetimecompleted] [smalldatetime] NULL,
	[Score] [int] NULL,
	[QuizCategoryID] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[QuizUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserQuiz]  WITH CHECK ADD  CONSTRAINT [FK_User_QuizCategory] FOREIGN KEY([QuizCategoryID])
REFERENCES [dbo].[QuizCategory] ([CategoryID])
GO

ALTER TABLE [dbo].[UserQuiz] CHECK CONSTRAINT [FK_User_QuizCategory]
GO


SET ANSI_PADDING OFF
GO


