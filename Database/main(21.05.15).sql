USE [Smekay]
GO
/****** Object:  Table [dbo].[Advert]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advert](
	[ACode] [int] IDENTITY(1,1) NOT NULL,
	[UCode] [int] NULL,
	[Status] [int] NULL,
	[CCode] [int] NULL,
	[ACCode] [int] NULL,
	[Contact] [text] NULL,
	[Phone] [text] NULL,
	[Email] [text] NULL,
	[Title] [text] NULL,
	[Description] [text] NULL,
	[History] [text] NULL,
	[Price] [int] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_ADVERT] PRIMARY KEY CLUSTERED 
(
	[ACode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Advert_Category]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advert_Category](
	[ACCode] [int] IDENTITY(1,1) NOT NULL,
	[Name] [text] NULL,
	[Desc] [text] NULL,
 CONSTRAINT [PK_ADVERT_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[ACCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CCode] [int] IDENTITY(1,1) NOT NULL,
	[Name] [text] NULL,
	[CoCode] [int] NULL,
 CONSTRAINT [PK_CITY] PRIMARY KEY CLUSTERED 
(
	[CCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CoCode] [int] IDENTITY(1,1) NOT NULL,
	[Country] [text] NULL,
 CONSTRAINT [PK_COUNTRIES] PRIMARY KEY CLUSTERED 
(
	[CoCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[UCode] [int] NOT NULL,
	[ACode] [int] NULL,
 CONSTRAINT [PK_FAVORITES] PRIMARY KEY CLUSTERED 
(
	[UCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ICode] [int] IDENTITY(1,1) NOT NULL,
	[Url] [text] NULL,
 CONSTRAINT [PK_IMAGES] PRIMARY KEY CLUSTERED 
(
	[ICode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images_To_Advert]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images_To_Advert](
	[ICode] [int] NOT NULL,
	[ACode] [int] NULL,
 CONSTRAINT [PK_IMAGES_TO_ADVERT] PRIMARY KEY CLUSTERED 
(
	[ICode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notification]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NCode] [int] IDENTITY(1,1) NOT NULL,
	[AuthorCode] [int] NULL,
	[RecipientCode] [int] NULL,
	[Content] [text] NULL,
 CONSTRAINT [PK_NOTIFICATION] PRIMARY KEY CLUSTERED 
(
	[NCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 21.05.2015 2:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UCode] [int] IDENTITY(1,1) NOT NULL,
	[CCode] [int] NULL,
	[Phone] [text] NULL,
	[News] [int] NULL,
	[Reminders] [int] NULL,
	[Notifications] [int] NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Contacts] [nvarchar](200) NULL,
	[Banned] [int] NULL,
	[Role] [int] NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[UCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Advert]  WITH CHECK ADD  CONSTRAINT [Advert_fk2] FOREIGN KEY([ACCode])
REFERENCES [dbo].[Advert_Category] ([ACCode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Advert] CHECK CONSTRAINT [Advert_fk2]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [City_fk0] FOREIGN KEY([CoCode])
REFERENCES [dbo].[Countries] ([CoCode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [City_fk0]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [Favorites_fk1] FOREIGN KEY([ACode])
REFERENCES [dbo].[Advert] ([ACode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [Favorites_fk1]
GO
ALTER TABLE [dbo].[Images_To_Advert]  WITH CHECK ADD  CONSTRAINT [Images_To_Advert_fk1] FOREIGN KEY([ACode])
REFERENCES [dbo].[Advert] ([ACode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Images_To_Advert] CHECK CONSTRAINT [Images_To_Advert_fk1]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [Users_fk0] FOREIGN KEY([CCode])
REFERENCES [dbo].[City] ([CCode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [Users_fk0]
GO
