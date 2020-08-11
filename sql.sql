USE [master]
GO
/****** Object:  Database [ZhiHu]    Script Date: 2020/8/11 17:05:56 ******/
CREATE DATABASE [ZhiHu] ON  PRIMARY 
( NAME = N'ZhiHu', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ZhiHu.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ZhiHu_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ZhiHu_log.ldf' , SIZE = 2560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ZhiHu] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ZhiHu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ZhiHu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ZhiHu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ZhiHu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ZhiHu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ZhiHu] SET ARITHABORT OFF 
GO
ALTER DATABASE [ZhiHu] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ZhiHu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ZhiHu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ZhiHu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ZhiHu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ZhiHu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ZhiHu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ZhiHu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ZhiHu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ZhiHu] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ZhiHu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ZhiHu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ZhiHu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ZhiHu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ZhiHu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ZhiHu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ZhiHu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ZhiHu] SET RECOVERY FULL 
GO
ALTER DATABASE [ZhiHu] SET  MULTI_USER 
GO
ALTER DATABASE [ZhiHu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ZhiHu] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ZhiHu', N'ON'
GO
USE [ZhiHu]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 2020/8/11 17:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[Id] [int] NOT NULL,
	[Author_Id] [nvarchar](32) NOT NULL,
	[Question_Id] [int] NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Answer_Type] [nvarchar](50) NULL,
	[Url] [nvarchar](max) NULL,
	[Is_Collapsed] [bit] NULL,
	[Created_Time] [int] NULL,
	[Updated_Time] [int] NULL,
	[Extras] [nvarchar](max) NULL,
	[Is_Copyable] [bit] NULL,
	[Is_Normal] [bit] NULL,
	[Voteup_Count] [int] NULL,
	[Comment_Count] [int] NULL,
	[Is_Sticky] [bit] NULL,
	[Admin_Closed_Comment] [bit] NULL,
	[Comment_Permission] [nvarchar](max) NULL,
	[Reshipment_Settings] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[Editable_Content] [nvarchar](max) NULL,
	[Excerpt] [nvarchar](max) NULL,
	[Collapsed_By] [nvarchar](max) NULL,
	[Collapse_Reason] [nvarchar](max) NULL,
	[Annotation_Action] [nvarchar](max) NULL,
	[Is_Labeled] [bit] NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 2020/8/11 17:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[Id] [nvarchar](32) NOT NULL,
	[Url_Token] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Avatar_Url] [nvarchar](max) NULL,
	[Avatar_Url_Template] [nvarchar](max) NULL,
	[Is_Org] [bit] NULL,
	[Type] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[User_Type] [nvarchar](max) NULL,
	[Headline] [nvarchar](max) NULL,
	[Gender] [int] NULL,
	[Is_Advertiser] [bit] NULL,
	[Follower_Count] [int] NULL,
	[Is_Followed] [bit] NULL,
	[Is_Privacy] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 2020/8/11 17:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[Type] [nvarchar](max) NULL,
	[Id] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Question_Type] [nvarchar](max) NULL,
	[Created] [int] NULL,
	[Updated_Time] [int] NULL,
	[Url] [nvarchar](max) NULL,
	[Relationship] [nvarchar](max) NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Answer_Author_Id]    Script Date: 2020/8/11 17:05:57 ******/
CREATE NONCLUSTERED INDEX [Answer_Author_Id] ON [dbo].[Answer]
(
	[Author_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Answer_Question_Id]    Script Date: 2020/8/11 17:05:57 ******/
CREATE NONCLUSTERED INDEX [Answer_Question_Id] ON [dbo].[Answer]
(
	[Question_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Author_Id]    Script Date: 2020/8/11 17:05:57 ******/
CREATE NONCLUSTERED INDEX [Author_Id] ON [dbo].[Author]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ZhiHu] SET  READ_WRITE 
GO
