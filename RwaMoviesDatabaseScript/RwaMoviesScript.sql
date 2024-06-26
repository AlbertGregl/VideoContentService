USE [master]
GO
/****** Object:  Database [RwaMovies]    Script Date: 16.6.2023. 16:07:45 ******/
CREATE DATABASE [RwaMovies]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RwaMovies', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\RwaMovies.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RwaMovies_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\RwaMovies_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RwaMovies] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RwaMovies].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RwaMovies] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RwaMovies] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RwaMovies] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RwaMovies] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RwaMovies] SET ARITHABORT OFF 
GO
ALTER DATABASE [RwaMovies] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [RwaMovies] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RwaMovies] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RwaMovies] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RwaMovies] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RwaMovies] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RwaMovies] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RwaMovies] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RwaMovies] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RwaMovies] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RwaMovies] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RwaMovies] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RwaMovies] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RwaMovies] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RwaMovies] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RwaMovies] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RwaMovies] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RwaMovies] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RwaMovies] SET  MULTI_USER 
GO
ALTER DATABASE [RwaMovies] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RwaMovies] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RwaMovies] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RwaMovies] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RwaMovies] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RwaMovies] SET QUERY_STORE = OFF
GO
USE [RwaMovies]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [char](2) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ReceiverEmail] [nvarchar](256) NOT NULL,
	[Subject] [nvarchar](256) NULL,
	[Body] [nvarchar](1024) NOT NULL,
	[SentAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[Username] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[PwdHash] [nvarchar](256) NOT NULL,
	[PwdSalt] [nvarchar](256) NOT NULL,
	[Phone] [nvarchar](256) NULL,
	[IsConfirmed] [bit] NOT NULL,
	[SecurityToken] [nvarchar](256) NULL,
	[CountryOfResidenceId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Video]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Video](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[GenreId] [int] NOT NULL,
	[TotalSeconds] [int] NOT NULL,
	[StreamingUrl] [nvarchar](256) NULL,
	[ImageId] [int] NULL,
 CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoTag]    Script Date: 16.6.2023. 16:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoTag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VideoId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_VideoTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (1, N'RU', N'Russia')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (2, N'FR', N'France')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (3, N'PT', N'Portugal')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (4, N'CA', N'Canada')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (5, N'GR', N'Greece')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (6, N'KM', N'Comoros')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (7, N'JP', N'Japan')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (8, N'PH', N'Philippines')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (9, N'PA', N'Panama')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (10, N'BA', N'Bosnia and Herzegovina')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (11, N'CN', N'China')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (12, N'PL', N'Poland')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (13, N'UA', N'Ukraine')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (14, N'US', N'United States')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (15, N'AF', N'Afghanistan')
INSERT [dbo].[Country] ([Id], [Code], [Name]) VALUES (16, N'SE', N'Sweden')
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (1, N'action', N'Should contain numerous scenes where action is spectacular and usually destructive. Often includes non-stop motion, high energy physical stunts, chases, battles, and destructive crises.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (2, N'adventure', N'Should contain numerous consecutive and inter-related scenes of characters participating in hazardous or exciting experiences for a specific goal. Often include searches or expeditions for lost continents and exotic locales, characters embarking in treasure hunt or heroic journeys, travels, and quests for the unknown.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (3, N'animation', N'Over 75% of the title''s running time should have scenes that are wholly, or part-animated. Any form of animation is acceptable, e.g., hand-drawn, computer-generated, stop-motion, etc. Puppetry does not count as animation, unless a form of animation such as stop-motion is also applied. Incidental animated sequences should be indicated with the keywords part-animated or animated-sequence instead.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (4, N'comedy', N'Virtually all scenes should contain characters participating in humorous or comedic experiences. The comedy can be exclusively for the viewer, at the expense of the characters in the title, or be shared with them. Please submit qualifying keywords to better describe the humor.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (5, N'crime', N'Whether the protagonists or antagonists are criminals this should contain numerous consecutive and inter-related scenes of characters participating, aiding, abetting, and/or planning criminal behavior or experiences usually for an illicit goal.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (6, N'documentary', N'Should contain numerous consecutive scenes of real personages and not characters portrayed by actors. This does not include fake or spoof documentaries, which should instead have the fake-documentary keyword.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (7, N'history', N'Primary focus is on real-life events of historical significance featuring real-life characters (allowing for some artistic license); in current terms, the sort of thing that might be expected to dominate the front page of a national newspaper for at least a week; for older times, the sort of thing likely to be included in any major history book.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (8, N'war', N'Should contain numerous scenes and/or a narrative that pertains to a real war (i.e., past or current).')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (9, N'western', N'Should contain numerous scenes and/or a narrative where the portrayal is similar to that of frontier life in the American West during 1600s to contemporary times.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (11, N'drama', N'Should contain numerous consecutive scenes of characters portrayed to effect a serious narrative throughout the title, usually involving conflicts and emotions. This can be exaggerated upon to produce melodrama.')
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (12, N'sci-fi', NULL)
INSERT [dbo].[Genre] ([Id], [Name], [Description]) VALUES (13, N'fantasy', NULL)
SET IDENTITY_INSERT [dbo].[Genre] OFF
GO
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([Id], [Content]) VALUES (1, N'https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_QL75_UX380_CR0,4,380,562_.jpg 380w')
INSERT [dbo].[Image] ([Id], [Content]) VALUES (2, N'https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_QL75_UY562_CR8,0,380,562_.jpg 380w')
INSERT [dbo].[Image] ([Id], [Content]) VALUES (3, N'https://m.media-amazon.com/images/M/MV5BNzA5ZDNlZWMtM2NhNS00NDJjLTk4NDItYTRmY2EwMWZlMTY3XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_QL75_UX380_CR0,0,380,562_.jpg 380w')
INSERT [dbo].[Image] ([Id], [Content]) VALUES (6, N'https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_QL75_UX380_CR0,0,380,562_.jpg 380w')
INSERT [dbo].[Image] ([Id], [Content]) VALUES (7, N'https://m.media-amazon.com/images/M/MV5BODU2NDhjMjMtNDU4YS00M2JhLWIwMDUtN2IyODNkOTc4MmM3XkEyXkFqcGdeQXVyMTE0NzY5OTk5._V1_QL75_UY562_CR35,0,380,562_.jpg 380w')
INSERT [dbo].[Image] ([Id], [Content]) VALUES (9, N'https://m.media-amazon.com/images/M/MV5BYTYxNGMyZTYtMjE3MS00MzNjLWFjNmYtMDk3N2FmM2JiM2M1XkEyXkFqcGdeQXVyNjY5NDU4NzI@._V1_QL75_UX380_CR0,1,380,562_.jpg 380w')
SET IDENTITY_INSERT [dbo].[Image] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (1, CAST(N'2023-04-16T18:42:40.0000000' AS DateTime2), N'ivic@algebra.hr', N'Potvrdi', N'WzdbsVUE4LUt6aHqiMGZF7wnpH7h/LQZ3Bv7YOYPVmg=', CAST(N'2023-04-16T17:58:30.3696434' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ana@algebra.hr', N'Email confirmation', N'Please confirm your email by clicking on the following link: https://localhost:7078/api/Users/ValidateEmail?username=anaanic&b64SecToken=Wpl%2BKIajitKVzYStOQFEnn322vDULGMAOfVTvLbuzJc%3D', CAST(N'2023-04-16T18:34:37.6150628' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'petraperic@algebra.hr', N'Email confirmation', N'Please confirm your email by clicking on the following link: <a href=''https://localhost:7078/api/Users/ValidateEmail?username=petraperic&b64SecToken=phlqSn5ocUyUKVtOgqNLVAtkPuYq0rmWryvTt7KHuEQ%3D''>Click here</a>', CAST(N'2023-04-17T07:52:14.8181353' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (4, CAST(N'2023-04-17T08:19:33.2359426' AS DateTime2), N'francfranci@algebra.hr', N'Email confirmation', N'Please confirm your email by clicking on the following link: <a href=''https://localhost:7078/api/Users/ValidateEmail?username=francfranci&b64SecToken=bPWKk7J4IqTuSXhHK5JcqP42xthF0qE2FFrPyYtP5TQ%3D''>Click here</a>', CAST(N'2023-04-17T08:22:42.9399016' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (5, CAST(N'2023-04-17T19:35:39.8651573' AS DateTime2), N'mariomaric@algebra.hr', N'Email confirmation', N'Please confirm your email by clicking on the following link: <a href=''/validate-email.html?username=mariomaric&b64SecToken=JKax8JDzSd5OV/ClFm49nKDplR9V7agyGK28M/7N5dA=''>Click here</a>', CAST(N'2023-04-17T19:37:09.9162227' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (6, CAST(N'2023-04-17T20:29:22.0448156' AS DateTime2), N'johndoe@algebra.hr', N'Email confirmation', N'Please confirm your email by clicking on the following link: https://localhost:7078/validate-email.html?username=johndoe&b64SecToken=Sy8b8fmT2foHg6UROWQySl/hPNZOEX/YmRGLgO/7Zlo=', CAST(N'2023-04-17T20:29:50.0095644' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (7, CAST(N'2023-06-14T15:04:24.2002369' AS DateTime2), N'admin@algebra.hr', N'Email confirmation', N'Please confirm your email by clicking on the following link: https://localhost:7078/validate-email.html?username=administrator&b64SecToken=/KTy6+BkCXxHFIcj+voVcLkf+wSRXGTtE71ERZg2Ok4=', NULL)
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (8, CAST(N'2023-06-15T13:18:10.4888849' AS DateTime2), N'3r3r', N'Email confirmation', N'Please confirm your email by clicking on the following link: https://localhost:7078/validate-email.html?username=r3r3r3rr3r3r&b64SecToken=h8InWcEoDbh5TtS0rvUm4GrNwh9G8gUqBGNjmA5SxwE=', NULL)
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (9, CAST(N'2023-06-15T13:31:18.4475032' AS DateTime2), N'perica', N'Email confirmation', N'Please confirm your email by clicking on the following link: https://localhost:7078/validate-email.html?username=peroperopero&b64SecToken=/bM4yvuBpemHD4XDz4ZK1kVja2HXvryx+d53cEy6i74=', NULL)
INSERT [dbo].[Notification] ([Id], [CreatedAt], [ReceiverEmail], [Subject], [Body], [SentAt]) VALUES (10, CAST(N'2023-06-15T16:14:01.8889300' AS DateTime2), N'pero@algebra.hr', N'Email confirmation', N'Please confirm your email by clicking on the following link: https://localhost:7078/validate-email.html?username=pero@algebra.hr&b64SecToken=5r6DoRvwqNSDjgBsJ43oplQ/cOiSR4E43g0DkueLl6A=', NULL)
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
SET IDENTITY_INSERT [dbo].[Tag] ON 

INSERT [dbo].[Tag] ([Id], [Name]) VALUES (1, N'Action')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (2, N'Romantic comedy')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (3, N'Mystery thriller')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (4, N'Animated adventure')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (5, N'Horror suspense')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (6, N'Time travel')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (7, N'Space opera')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (8, N'Coming of age')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (9, N'Family drama')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (10, N'Superhero action')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (11, N'Historical epic')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (12, N'Sports drama')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (13, N'Political thriller')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (14, N'Biographical drama')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (15, N'Post-apocalyptic')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (16, N'Time Travel')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (17, N'Superhero')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (18, N'High School')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (19, N'Revenge')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (20, N'Betrayal')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (21, N'Underdog')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (22, N'Coming of Age')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (23, N'Forbidden Love')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (24, N'Survival')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (25, N'Road Trip')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (26, N'Adventure')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (27, N'Heist')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (28, N'Family Drama')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (29, N'Mystery-solving')
INSERT [dbo].[Tag] ([Id], [Name]) VALUES (30, N'Space Exploration')
SET IDENTITY_INSERT [dbo].[Tag] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (4, CAST(N'2023-04-15T11:20:46.1301213' AS DateTime2), CAST(N'2023-06-15T06:04:42.3774432' AS DateTime2), N'ivoivic', N'Ivo', N'Ivic', N'ivic@algebra.hr', N'hMlv3FhlgOMoj/kv4fXQfkEx58OqQ+ksFihvdCAydLI=', N'NGlfonxjvm+9ylhSdHW7QQ==', N'string', 0, N'WzdbsVUE4LUt6aHqiMGZF7wnpH7h/LQZ3Bv7YOYPVmg=', 2)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (5, CAST(N'2023-04-15T13:01:54.9951979' AS DateTime2), CAST(N'2023-06-15T06:02:31.2874226' AS DateTime2), N'miromiric', N'Miro', N'Miric', N'miro@algebra.hr', N'Nkbxvvub81A1jfw1zNEj7PbhsvFwJ0jrmTFXxdt9lms=', N'8Q89ukQ5+cJz0cQBoNNqGw==', N'string', 0, N'D+tw24htcw6KBYmG8DLR0XpdD5LStHyZySbk0oBw9Xo=', 6)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (6, CAST(N'2023-04-15T13:13:18.9979723' AS DateTime2), CAST(N'2023-06-16T13:23:16.0176669' AS DateTime2), N'perobot', N'Pero', N'Peric', N'perobot@algebra.hr', N'c1sjOO9bAdROF6joFdYSonZhN8qYBpXHkVqTWgKOB1s=', N'7tThF423RXy028X/f07vSw==', N'00385917775555', 0, N'5Bbh3986etBkFIIZ7YdX3NOz34J85d1LmstZIS9Tz8A=', 9)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (8, CAST(N'2023-04-16T20:33:40.2188858' AS DateTime2), CAST(N'2023-06-16T13:23:18.3752482' AS DateTime2), N'anaanic', N'Ana', N'Anic', N'ana@algebra.hr', N'0v2GokRU+oj4EmVimugYDmjA0k1EeNHCnhsV4+W60iU=', N'5muYG4l/H20DZenprnHhYw==', NULL, 0, N'Wpl+KIajitKVzYStOQFEnn322vDULGMAOfVTvLbuzJc=', 5)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (9, CAST(N'2023-04-17T09:45:52.5182948' AS DateTime2), CAST(N'2023-06-16T13:23:19.0110386' AS DateTime2), N'petraperic', N'Petra', N'Peric', N'petraperic@algebra.hr', N'QeLqTfuR/5ZMVeWijZQrC4PEOh27eBHV79r4sKJl2Aw=', N'FQ8WFRG/sGMVrA96KEGhUw==', N'', 0, N'phlqSn5ocUyUKVtOgqNLVAtkPuYq0rmWryvTt7KHuEQ=', 7)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (10, CAST(N'2023-04-17T10:19:32.9178393' AS DateTime2), CAST(N'2023-06-16T13:23:19.5187210' AS DateTime2), N'francfranci', N'Franc', N'Franci', N'francfranci@algebra.hr', N'Q9Mj4C0INYvR32m4W6jitB/cHqalenwFr+vmmHAN9AI=', N'tV4aps4ZNTUY2M7oJJk2Cw==', N'00915556666', 0, N'bPWKk7J4IqTuSXhHK5JcqP42xthF0qE2FFrPyYtP5TQ=', 2)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (11, CAST(N'2023-04-17T21:35:39.5562928' AS DateTime2), CAST(N'2023-06-16T13:23:20.8225309' AS DateTime2), N'mariomaric', N'Mario', N'Maric', N'mariomaric@algebra.hr', N'BVr17XbNYyOVrOJEq9CYadYq6Ap4dr8YLqaIjaE4Hmg=', N'UAo2l1hfAj7p4Pv8wwVvCA==', N'00912224444', 0, N'JKax8JDzSd5OV/ClFm49nKDplR9V7agyGK28M/7N5dA=', 6)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (12, CAST(N'2023-04-17T22:29:21.8839501' AS DateTime2), CAST(N'2023-06-16T13:23:23.0762602' AS DateTime2), N'johndoe', N'John', N'Doe', N'johndoe@algebra.hr', N'OFekZupTzzgXUSH1YHWR0aXN9Q0UFlbZHKN8rafMpVQ=', N'BCuVxnipTm3nG311saODdA==', NULL, 0, N'Sy8b8fmT2foHg6UROWQySl/hPNZOEX/YmRGLgO/7Zlo=', 1)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (13, CAST(N'2023-06-14T17:04:23.8924200' AS DateTime2), NULL, N'administrator', N'Gaius Julius', N'Caesar', N'admin@algebra.hr', N'1f/LbMsKN5+kRkDtZj7HjbGli4Ruc2dm/70xlHvLoQc=', N'iE3xbsTeVs/zT9PkKVfjwQ==', N'Illyricum', 1, N'/KTy6+BkCXxHFIcj+voVcLkf+wSRXGTtE71ERZg2Ok4=', 1)
INSERT [dbo].[User] ([Id], [CreatedAt], [DeletedAt], [Username], [FirstName], [LastName], [Email], [PwdHash], [PwdSalt], [Phone], [IsConfirmed], [SecurityToken], [CountryOfResidenceId]) VALUES (16, CAST(N'2023-06-15T18:14:01.5980343' AS DateTime2), NULL, N'pero@algebra.hr', N'Pero', N'Peric', N'pero@algebra.hr', N'BPJX0MYmHWqgrZEHos4sJ+NLDr4T7P6GenyLUc0TL2E=', N'QsBW3vQCyPSA1SCYMe0Tvw==', N'00', 1, N'5r6DoRvwqNSDjgBsJ43oplQ/cOiSR4E43g0DkueLl6A=', 5)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Video] ON 

INSERT [dbo].[Video] ([Id], [CreatedAt], [Name], [Description], [GenreId], [TotalSeconds], [StreamingUrl], [ImageId]) VALUES (3, CAST(N'2023-04-12T18:20:12.3400000' AS DateTime2), N'The Shawshank Redemption', N'Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.', 11, 8520, N'https://www.imdb.com/video/vi3877612057/?playlistId=tt0111161?ref_=ext_shr_lnk', 1)
INSERT [dbo].[Video] ([Id], [CreatedAt], [Name], [Description], [GenreId], [TotalSeconds], [StreamingUrl], [ImageId]) VALUES (4, CAST(N'2023-04-12T18:32:16.0300000' AS DateTime2), N'The Godfather', N'The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son.', 5, 10500, N'https://www.imdb.com/video/vi1348706585/?playlistId=tt0068646?ref_=ext_shr_lnk', 2)
INSERT [dbo].[Video] ([Id], [CreatedAt], [Name], [Description], [GenreId], [TotalSeconds], [StreamingUrl], [ImageId]) VALUES (5, CAST(N'2023-04-12T18:53:49.0400000' AS DateTime2), N'The Lord of the Rings: The Return of the King', N'Gandalf and Aragorn lead the World of Men against Sauron''s army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.', 2, 12060, N'https://www.imdb.com/video/vi718127897/?playlistId=tt0167260?ref_=ext_shr_lnk', 3)
INSERT [dbo].[Video] ([Id], [CreatedAt], [Name], [Description], [GenreId], [TotalSeconds], [StreamingUrl], [ImageId]) VALUES (8, CAST(N'2023-04-16T08:44:14.6000000' AS DateTime2), N'Inception', N'A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.', 1, 8880, N'https://www.imdb.com/video/vi2959588889/?playlistId=tt1375666&ref_=ext_shr_lnk', 6)
INSERT [dbo].[Video] ([Id], [CreatedAt], [Name], [Description], [GenreId], [TotalSeconds], [StreamingUrl], [ImageId]) VALUES (9, CAST(N'2023-06-14T15:50:17.4366667' AS DateTime2), N'Nimona', N'When a knight in a futuristic medieval world is framed for a crime he didn''t commit, the only one who can help him prove his innocence is Nimona -- a mischievous teen who happens to be a shapeshifting creature he''s sworn to destroy.', 2, 6000, N'https://www.imdb.com/video/vi783468313/?listId=ls053181649&ref_=ext_shr_lnk', 7)
INSERT [dbo].[Video] ([Id], [CreatedAt], [Name], [Description], [GenreId], [TotalSeconds], [StreamingUrl], [ImageId]) VALUES (11, CAST(N'2023-06-14T17:01:32.5566667' AS DateTime2), N'The Lion King', N'Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.', 3, 5280, N'https://www.imdb.com/video/vi3764362265/?playlistId=tt0110357&ref_=ext_shr_lnk', 9)
SET IDENTITY_INSERT [dbo].[Video] OFF
GO
SET IDENTITY_INSERT [dbo].[VideoTag] ON 

INSERT [dbo].[VideoTag] ([Id], [VideoId], [TagId]) VALUES (3, 3, 14)
INSERT [dbo].[VideoTag] ([Id], [VideoId], [TagId]) VALUES (4, 3, 20)
INSERT [dbo].[VideoTag] ([Id], [VideoId], [TagId]) VALUES (5, 4, 20)
INSERT [dbo].[VideoTag] ([Id], [VideoId], [TagId]) VALUES (6, 4, 19)
INSERT [dbo].[VideoTag] ([Id], [VideoId], [TagId]) VALUES (7, 4, 27)
INSERT [dbo].[VideoTag] ([Id], [VideoId], [TagId]) VALUES (8, 5, 1)
INSERT [dbo].[VideoTag] ([Id], [VideoId], [TagId]) VALUES (9, 5, 26)
SET IDENTITY_INSERT [dbo].[VideoTag] OFF
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsConfirmed]  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[Video] ADD  CONSTRAINT [DF_Video_CreatedAt]  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Video] ADD  CONSTRAINT [DF_Video_TotalSeconds]  DEFAULT ((0)) FOR [TotalSeconds]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Country] FOREIGN KEY([CountryOfResidenceId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Country]
GO
ALTER TABLE [dbo].[Video]  WITH CHECK ADD  CONSTRAINT [FK_Video_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([Id])
GO
ALTER TABLE [dbo].[Video] CHECK CONSTRAINT [FK_Video_Genre]
GO
ALTER TABLE [dbo].[Video]  WITH CHECK ADD  CONSTRAINT [FK_Video_Images] FOREIGN KEY([ImageId])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[Video] CHECK CONSTRAINT [FK_Video_Images]
GO
ALTER TABLE [dbo].[VideoTag]  WITH CHECK ADD  CONSTRAINT [FK_VideoTag_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
GO
ALTER TABLE [dbo].[VideoTag] CHECK CONSTRAINT [FK_VideoTag_Tag]
GO
ALTER TABLE [dbo].[VideoTag]  WITH CHECK ADD  CONSTRAINT [FK_VideoTag_Video] FOREIGN KEY([VideoId])
REFERENCES [dbo].[Video] ([Id])
GO
ALTER TABLE [dbo].[VideoTag] CHECK CONSTRAINT [FK_VideoTag_Video]
GO
USE [master]
GO
ALTER DATABASE [RwaMovies] SET  READ_WRITE 
GO
