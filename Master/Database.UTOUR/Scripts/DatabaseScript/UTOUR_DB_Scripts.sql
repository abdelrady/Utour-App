USE [master]
GO
/****** Object:  Database [UTOURDB]    Script Date: 07/27/2012 19:49:35 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'UTOURDB')
BEGIN
CREATE DATABASE [UTOURDB] ON  PRIMARY 
( NAME = N'UTOURDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\UTOURDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UTOURDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\UTOURDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END
GO
ALTER DATABASE [UTOURDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UTOURDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UTOURDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [UTOURDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [UTOURDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [UTOURDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [UTOURDB] SET ARITHABORT OFF
GO
ALTER DATABASE [UTOURDB] SET AUTO_CLOSE ON
GO
ALTER DATABASE [UTOURDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [UTOURDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [UTOURDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [UTOURDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [UTOURDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [UTOURDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [UTOURDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [UTOURDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [UTOURDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [UTOURDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [UTOURDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [UTOURDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [UTOURDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [UTOURDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [UTOURDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [UTOURDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [UTOURDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [UTOURDB] SET  READ_WRITE
GO
ALTER DATABASE [UTOURDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [UTOURDB] SET  MULTI_USER
GO
ALTER DATABASE [UTOURDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [UTOURDB] SET DB_CHAINING OFF
GO
USE [UTOURDB]
GO
/****** Object:  ForeignKey [FK_layerhotspots_city]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_layerhotspots_city]') AND parent_object_id = OBJECT_ID(N'[dbo].[layerhotspots]'))
ALTER TABLE [dbo].[layerhotspots] DROP CONSTRAINT [FK_layerhotspots_city]
GO
/****** Object:  ForeignKey [FK_UploadedPhotos_layerhotspots]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos] DROP CONSTRAINT [FK_UploadedPhotos_layerhotspots]
GO
/****** Object:  ForeignKey [FK_UploadedPhotos_Tourists]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos] DROP CONSTRAINT [FK_UploadedPhotos_Tourists]
GO
/****** Object:  ForeignKey [FK_Monuments_Videos_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Videos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Videos]'))
ALTER TABLE [dbo].[Monuments_Videos] DROP CONSTRAINT [FK_Monuments_Videos_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monuments_Reviews_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews] DROP CONSTRAINT [FK_Monuments_Reviews_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monuments_Reviews_Tourists]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews] DROP CONSTRAINT [FK_Monuments_Reviews_Tourists]
GO
/****** Object:  ForeignKey [FK_Monuments_Photos_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Photos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Photos]'))
ALTER TABLE [dbo].[Monuments_Photos] DROP CONSTRAINT [FK_Monuments_Photos_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monument_Ratings_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings] DROP CONSTRAINT [FK_Monument_Ratings_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monument_Ratings_Tourists]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings] DROP CONSTRAINT [FK_Monument_Ratings_Tourists]
GO
/****** Object:  StoredProcedure [dbo].[GetHotSpotsWithinDistance]    Script Date: 07/27/2012 19:49:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetHotSpotsWithinDistance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetHotSpotsWithinDistance]
GO
/****** Object:  Table [dbo].[Monument_Ratings]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings] DROP CONSTRAINT [FK_Monument_Ratings_layerhotspots]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings] DROP CONSTRAINT [FK_Monument_Ratings_Tourists]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]') AND type in (N'U'))
DROP TABLE [dbo].[Monument_Ratings]
GO
/****** Object:  Table [dbo].[Monuments_Photos]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Photos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Photos]'))
ALTER TABLE [dbo].[Monuments_Photos] DROP CONSTRAINT [FK_Monuments_Photos_layerhotspots]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monuments_Photos]') AND type in (N'U'))
DROP TABLE [dbo].[Monuments_Photos]
GO
/****** Object:  Table [dbo].[Monuments_Reviews]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews] DROP CONSTRAINT [FK_Monuments_Reviews_layerhotspots]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews] DROP CONSTRAINT [FK_Monuments_Reviews_Tourists]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]') AND type in (N'U'))
DROP TABLE [dbo].[Monuments_Reviews]
GO
/****** Object:  Table [dbo].[Monuments_Videos]    Script Date: 07/27/2012 19:49:43 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Videos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Videos]'))
ALTER TABLE [dbo].[Monuments_Videos] DROP CONSTRAINT [FK_Monuments_Videos_layerhotspots]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monuments_Videos]') AND type in (N'U'))
DROP TABLE [dbo].[Monuments_Videos]
GO
/****** Object:  Table [dbo].[UploadedPhotos]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos] DROP CONSTRAINT [FK_UploadedPhotos_layerhotspots]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos] DROP CONSTRAINT [FK_UploadedPhotos_Tourists]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]') AND type in (N'U'))
DROP TABLE [dbo].[UploadedPhotos]
GO
/****** Object:  Table [dbo].[layerhotspots]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_layerhotspots_city]') AND parent_object_id = OBJECT_ID(N'[dbo].[layerhotspots]'))
ALTER TABLE [dbo].[layerhotspots] DROP CONSTRAINT [FK_layerhotspots_city]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[layerhotspots]') AND type in (N'U'))
DROP TABLE [dbo].[layerhotspots]
GO
/****** Object:  Table [dbo].[Tourists]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tourists]') AND type in (N'U'))
DROP TABLE [dbo].[Tourists]
GO
/****** Object:  Table [dbo].[Admin_Users]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_Users]') AND type in (N'U'))
DROP TABLE [dbo].[Admin_Users]
GO
/****** Object:  Table [dbo].[city]    Script Date: 07/27/2012 19:49:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[city]') AND type in (N'U'))
DROP TABLE [dbo].[city]
GO
/****** Object:  UserDefinedFunction [dbo].[GetDistance]    Script Date: 07/27/2012 19:49:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDistance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetDistance]
GO
/****** Object:  Schema [Utour]    Script Date: 07/27/2012 19:49:35 ******/
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'Utour')
DROP SCHEMA [Utour]
GO
/****** Object:  User [AspNetUser]    Script Date: 07/27/2012 19:49:35 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AspNetUser')
DROP USER [AspNetUser]
GO
/****** Object:  User [netService]    Script Date: 07/27/2012 19:49:35 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'netService')
DROP USER [netService]
GO
/****** Object:  User [user1]    Script Date: 07/27/2012 19:49:35 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'user1')
DROP USER [user1]
GO
/****** Object:  User [user1]    Script Date: 07/27/2012 19:49:35 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'user1')
CREATE USER [user1] FOR LOGIN [user1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [netService]    Script Date: 07/27/2012 19:49:35 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'netService')
CREATE USER [netService] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [AspNetUser]    Script Date: 07/27/2012 19:49:35 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AspNetUser')
CREATE USER [AspNetUser] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Schema [Utour]    Script Date: 07/27/2012 19:49:35 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Utour')
EXEC sys.sp_executesql N'CREATE SCHEMA [Utour] AUTHORIZATION [netService]'
GO
/****** Object:  UserDefinedFunction [dbo].[GetDistance]    Script Date: 07/27/2012 19:49:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDistance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetDistance]

(
      @lat1 Float(8),
      @long1 Float(8),
      @lat2 Float(8),
      @long2 Float(8)
)
RETURNS Float(8)
AS
BEGIN
 
      DECLARE @R Float(8);
      DECLARE @dLat Float(8);
      DECLARE @dLon Float(8);
      DECLARE @a Float(8);
      DECLARE @c Float(8);
      DECLARE @d Float(8);
 
      SET @R = 3960;
      SET @dLat = RADIANS(@lat2 - @lat1);
      SET @dLon = RADIANS(@long2 - @long1);
 
      SET @a = SIN(@dLat / 2) * SIN(@dLat / 2) + COS(RADIANS(@lat1))
                        * COS(RADIANS(@lat2)) * SIN(@dLon / 2) *SIN(@dLon / 2);
      SET @c = 2 * ASIN(MIN(SQRT(@a)));
      SET @d = @R * @c;
 
      RETURN @d;
 
END
' 
END
GO
/****** Object:  Table [dbo].[city]    Script Date: 07/27/2012 19:49:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[city]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[city](
	[CityName] [nvarchar](50) NULL,
	[CityID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_city_1] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[city] ON
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Cairo', 1)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Aswan', 2)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Alexandria', 3)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Luxor', 4)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Abu-Simbel', 5)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Bahariya Oasis', 6)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Dakhla Oasis', 7)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Farafra Oasis', 8)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Hurghada City', 9)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Kharga Oasis', 10)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Marsa Matrouh', 11)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Sharm El Sheikh', 12)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Siwa Oasis', 13)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Taba', 14)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Qena', 15)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'Minya', 16)
INSERT [dbo].[city] ([CityName], [CityID]) VALUES (N'fayoum', 17)
SET IDENTITY_INSERT [dbo].[city] OFF
/****** Object:  Table [dbo].[Admin_Users]    Script Date: 07/27/2012 19:49:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Admin_Users](
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Admin_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Admin_Users] ON
INSERT [dbo].[Admin_Users] ([UserName], [Password], [ID]) VALUES (N'Abdelrady', N'aaaa37f40b987f5273884c5e43e056a7', 1)
INSERT [dbo].[Admin_Users] ([UserName], [Password], [ID]) VALUES (N'rana', N'90a1e95dba0d3d9c11e3f220cc4f7879', 2)
INSERT [dbo].[Admin_Users] ([UserName], [Password], [ID]) VALUES (N'ahmed', N'9193ce3b31332b03f7d8af056c692b84', 3)
SET IDENTITY_INSERT [dbo].[Admin_Users] OFF
/****** Object:  Table [dbo].[Tourists]    Script Date: 07/27/2012 19:49:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tourists]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tourists](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[First Name] [nvarchar](50) NOT NULL,
	[Last Name] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[Nationality] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Preferred Language] [nchar](10) NULL,
 CONSTRAINT [PK_Tourists] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Tourists] ON
INSERT [dbo].[Tourists] ([ID], [UserName], [Password], [First Name], [Last Name], [Gender], [Nationality], [Email], [Preferred Language]) VALUES (4, N'Huda', N'0075a4e7a2e71083262da135ecdbdd14', N'Huda', N'Ahmed', 1, N'Eg', N'Huda@gmail.com', N'en-us     ')
INSERT [dbo].[Tourists] ([ID], [UserName], [Password], [First Name], [Last Name], [Gender], [Nationality], [Email], [Preferred Language]) VALUES (5, N'Abdelrady', N'aaaa37f40b987f5273884c5e43e056a7', N'Abdelrady', N'Fcis', 0, N'Eg', N'Abdelrady@gmail.com', N'en-us     ')
INSERT [dbo].[Tourists] ([ID], [UserName], [Password], [First Name], [Last Name], [Gender], [Nationality], [Email], [Preferred Language]) VALUES (6, N'Ahmed', N'9193ce3b31332b03f7d8af056c692b84', N'Ahmed', N'Sayed', 0, N'Eg', N'Ahmed@gmail.com', N'en-us     ')
INSERT [dbo].[Tourists] ([ID], [UserName], [Password], [First Name], [Last Name], [Gender], [Nationality], [Email], [Preferred Language]) VALUES (7, N'Somaia', N'b51de1043076c353da09f1b0afa860ea', N'Somaia', N'Osama', 1, N'Eg', N'Somaia@gmail.com', N'ar-eg     ')
INSERT [dbo].[Tourists] ([ID], [UserName], [Password], [First Name], [Last Name], [Gender], [Nationality], [Email], [Preferred Language]) VALUES (8, N'Rana', N'90a1e95dba0d3d9c11e3f220cc4f7879', N'Rana', N'Amr', 1, N'Eg', N'Rana@gmail.com', N'ar-eg     ')
SET IDENTITY_INSERT [dbo].[Tourists] OFF
/****** Object:  Table [dbo].[layerhotspots]    Script Date: 07/27/2012 19:49:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[layerhotspots]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[layerhotspots](
	[Id] [varchar](255) NOT NULL,
	[footnote] [nvarchar](150) NULL,
	[title] [nvarchar](150) NULL,
	[lat] [decimal](13, 10) NULL,
	[lon] [decimal](13, 10) NULL,
	[imageurl] [varchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[biwStyle] [varchar](50) NULL,
	[alt] [int] NULL,
	[doNotIndex] [tinyint] NULL,
	[showSmallBiw] [tinyint] NULL,
	[showBiwOnClick] [tinyint] NULL,
	[poiType] [varchar](50) NULL,
	[Monument_Audio] [nvarchar](255) NULL,
	[Long_Description] [ntext] NULL,
	[CityID] [int] NULL,
 CONSTRAINT [PK_layerhotspots] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_1', N'powered by ITIANs', N'Temple of Dendera', CAST(26.1420820000 AS Decimal(13, 10)), CAST(32.6697710000 AS Decimal(13, 10)), N'img/Anc_1.jpg', N'A Gorgeous Graeco-Roman Temple ', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Walk by several Roman kiosks, pass through the colossal gateway of Domitian and Trajan that is surrounded by a massive mud-brick enclosure and enter into the cavernous hypostyle hall of Tiberius. Visit the best preserved temple in Egypt. See vivid scenes that depict the Roman emperor Trajan paying homage to the Ancient Egyptian goddess Hathor and other mythical reliefs that adorn this gorgeous Graeco-Roman temple.', 15)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_10', N'powered by ITIANs', N'The Temple of Philae', CAST(24.0256110000 AS Decimal(13, 10)), CAST(32.8840940000 AS Decimal(13, 10)), N'img/Anc_10.jpg', N'A UNESCO World Heritage Site', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Dedicated to the goddess Isis, the Temple of Philae is located in a beautiful setting, landscaped to match the original site of the temple when it was relocated by UNESCO after the building of the Aswan Dam threatened the site. The temple has several shrines and sanctuaries such as Trajan’s Kiosk or Pharaoh''s Bed. Visit the temple at night to attend the Sound and Light show.', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_11', N'powered by ITIANs', N'Temple of Philea Sound and Light Show', CAST(24.0256190000 AS Decimal(13, 10)), CAST(32.8841390000 AS Decimal(13, 10)), N'img/Anc_11.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Sail at night to reach the Island of Philae to enjoy a show of sound and light among a magnificent temple complex set on an island. The Temple of Philae is dramatically transformed by the sound and light show. Philae Temple comes to life as a voice over narrates the myths of Isis and Osiris as you tour the temple. The music, light and myths transform touring the temple into a theatrical experience. You''ll also gain a deeper understanding of Ancient Egyptian gods- their romantic relations, rivalry and the symbolic acts their actions represented. Keep in mind that it can get cold at night, especially during winter. ', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_12', N'powered by ITIANs', N'Temple of Ramses II', CAST(31.3947710000 AS Decimal(13, 10)), CAST(27.0414050000 AS Decimal(13, 10)), N'img/Anc_12.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'About 24 km west of Marsa Matrouh, you can explore the ruins of the Temple of Ramses II, which dates back to the 26th dynasty (1200 BC). The site is known as Umm Al-Rakhm, it was discovered by the Egyptian archaeologist Labib Habash back in 1942, and houses some admirable hieroglyphic inscriptions referring to the Ancient Egyptian pharaoh.', 11)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_13', N'powered by ITIANs', N'Temple of Satis', CAST(24.0846560000 AS Decimal(13, 10)), CAST(32.8860170000 AS Decimal(13, 10)), N'img/Anc_13.jpg', N'On Elephantine Island', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located on the southern end of Elephantine Island; the temple of Satis is dedicated to the Goddess Satis also known as Satet, however her partner Khnum (the Ram Headed God) and her daughter Anket were also worshipped at this temple. At least ten rulers, from the early dynasties to the Ptolemaic, either added to or restored the temple; some of them are Pepi I, Mentuhotep II, Thutmose III, and Hatshepsut. This temple has some fascinating reliefs. Make sure you notice the Nilometer carved into the river bank, which indicates the level of Nile flooding, and one of the rare times that a carving of face in the stone is portrayed in full frontal rather than the usual profile image that we see in Ancient Egyptian art.', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_14', N'powered by ITIANs', N'Temple of the Oracle', CAST(29.2029290000 AS Decimal(13, 10)), CAST(25.5474880000 AS Decimal(13, 10)), N'img/Anc_14.jpg', N'Siwa & Alexander the Great', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located 4 km east of the present town of Siwa, the Temple of the Oracle is believed to have housed the famous Greek oracle of Jupiter Amun, to which Alexander the Great headed directly when he came to Egypt for the first time in 331 BC. Reputedly, it is believed that the Macedonian leader asked the oracle if he was going to “rule the world,” legend has it that the answer was “yes, but not for very long.” The temple has a great vestibule and forecourt.', 13)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_15', N'powered by ITIANs', N'Temples of Aten', CAST(27.6511320000 AS Decimal(13, 10)), CAST(30.8985510000 AS Decimal(13, 10)), N'img/Anc_15.jpg', N'Sun Worship & Heresy', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'In a time of religious upheaval, the Great Temple of Aten and the smaller Mansion of the Aten were constructed in the new city of Akhenaten (near modern day Tell el Amarna). The city was hastily built as the new religious and political capital of Egypt during the reign of Akhenaten, a pharaoh that attempted to change religious practices by elevating Aten, the sun god, above all others.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_16', N'powered by ITIANs', N'The Egyptian Museum', CAST(30.0473430000 AS Decimal(13, 10)), CAST(31.2336260000 AS Decimal(13, 10)), N'img/Anc_16.jpg', N'See the Artifacts of the Ancients', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'No Egypt tour is complete without a visit to the Egyptian Museum in Cairo. With over 120,000 artefacts, the museum houses an unbelievable exhibit depicting ancient Egypt''s glorious reign. Mummies, sarcophagi, pottery, jewellery and of course King Tutankhamen''s treasures, it’s all there. The boy-king''s death-mask – discovered in its tomb – is made of solid gold and it has been described as the most beautiful object ever made.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_17', N'powered by ITIANs', N'The Great Sphinx of Giza', CAST(29.9777300000 AS Decimal(13, 10)), CAST(31.1319460000 AS Decimal(13, 10)), N'img/Anc_17.jpg', N'Witness the Eternal Vigil', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The greatest mystery of Ancient Egyptian mysteries is also the largest monolithic statue and the oldest known monumental sculpture in the world. When was it built, for what purpose, which pharaoh does it represent, and who broke the nose? Any answer is a matter of conjecture. Egyptologists have not found any conclusive evidence. No matter. The Great Sphinx of Giza is a wonder to behold.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_18', N'powered by ITIANs', N'The Mummification Museum', CAST(25.7075720000 AS Decimal(13, 10)), CAST(32.6443170000 AS Decimal(13, 10)), N'img/Anc_18.jpg', N'Explore the Funerary Journey', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located in Luxor, the Mummification Museum presents the ancient Egyptian art of mummification and displays related artefacts and mummies. Here you’ll learn about the mummification techniques specialized to embalm many animal species, such as crocodiles, cats and fish. Mummified animals are unique to the collection of this museum. You’ll also see mummification tools, embalming materials, Canoptic jars, amulets and coffins. There are also ancient tablets that record the funeral journey from death to burial. The museum is divided into the Hall of Artefacts, Lecture Hall, Video Room and the cafeteria.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_19', N'powered by ITIANs', N'The Nilometer in Aswan', CAST(24.0838580000 AS Decimal(13, 10)), CAST(32.8865360000 AS Decimal(13, 10)), N'img/Anc_19.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'It''s not a speedometer or an odometer or a barometer or even a thermometer. It is 7000 years old and named a Nilometer!', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_2', N'powered by ITIANs', N'Temple of Edfu Sound and Light Show', CAST(24.9793130000 AS Decimal(13, 10)), CAST(32.8752850000 AS Decimal(13, 10)), N'img/Anc_2.jpg', N'', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Simply seeing the temple lit as you sail to reach it is wonderful sight. Hearing the music and listening to ancient tales as you tour this beautiful temple is out of this world. Combined, the experience is truly magical. Using the latest technology, the sound and light show at Edfu depicts the legends of the god Horus. Keep in mind that it can get cold at night, especially during winter.', 15)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_20', N'powered by ITIANs', N'The Osireion at Abydos', CAST(26.1844270000 AS Decimal(13, 10)), CAST(31.9186480000 AS Decimal(13, 10)), N'img/Anc_20.jpg', N'The Temple of Seti I', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Not to be missed on a Nile Valley trip is the Temple of Osireion at Abydos; just a little to the North of Luxor where the Ancient city of Abydos once stood, you can find this unique L shaped temple which served seven Gods of Ancient Egypt. Osiris, Isis, Horus, Amon Ra, Ra Horakhty, Ptah, and the deified Seti are each assigned a shrine and a chamber in which you can discover well preserved ancient art and writings; the temple is also the supposed resting place of the head of the God Osiris. At the back of the temple is a structure known as the Osireion, built in a way resembling the tombs in the Valley of the Kings, a dummy sarcophagus was found in the burial chamber of this building.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_21', N'powered by ITIANs', N'The Red Pyramid of Dahshur', CAST(29.8086730000 AS Decimal(13, 10)), CAST(31.2064430000 AS Decimal(13, 10)), N'img/Anc_21.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located at a short distance from Saqqara, the Red Pyramid at Dahshur was built for the pharaoh Snefru, father of Khufu for whom the Great Pyramid of Giza was built. The Red Pyramid is the first “true” pyramid and it has the second largest base of all Egyptian pyramids. What makes it yet more interesting is that it can be fully accessed, including the chance to see the burial chamber. There’s something special about exploring the cold and silent passageways inside the pyramid. Do not go inside if you are not comfortable in enclosed space.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_22', N'powered by ITIANs', N'The Solar Boat Museum', CAST(29.9777300000 AS Decimal(13, 10)), CAST(31.1319460000 AS Decimal(13, 10)), N'img/Anc_22.jpg', N'Cheops'' Solar Barge', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Ancient Egyptians used to bury a "solar barge" near the tomb of their pharaoh because they believed that their ruler needed transportation in the afterlife.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_23', N'powered by ITIANs', N'Seeing Doubles', CAST(24.4517700000 AS Decimal(13, 10)), CAST(32.9279240000 AS Decimal(13, 10)), N'img/Anc_23.jpg', N'The Temple of Kom Ombo', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Dedicated to Sobek and Horus the Elder, the Temple of Kom Ombo has two identical entrances, hypostyle halls and sanctuaries. The symmetry of the temple layout is a tribute to the mythical link the two gods shared. Built on an outcrop at a bend in the Nile where crocodiles used to gather in ancient times, the temple is a testament to the importance Ancient Egyptian priests placed in the natural cycles and crocodiles of the Nile. Visit the temple to see mummified crocodiles, clay coffins and spectacular reliefs on the walls.', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_24', N'powered by ITIANs', N'The Unfinished Obelisk', CAST(24.0771230000 AS Decimal(13, 10)), CAST(32.8954880000 AS Decimal(13, 10)), N'img/Anc_24.jpg', N'The Unfinished Obelisk', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'A great disappointment for Ancient Egyptian stonemasons but an awe-inspiring site for tourists, the Unfinished Obelisk inAswan is the heaviest monolith found in Egypt. About 42 meters long and weighing an estimated 1168 tons, the obelisk would have been the largest piece of Egyptian masonry had it been completed. The obelisk was probably left unfinished after it cracked at the middle. It is still attached to the parent rock in the Northern Quarries, where the Ancient Egyptian quarried granite. Note the tool marks and pictographs of dolphins and birds engraved on this colossal obelisk.', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_25', N'powered by ITIANs', N'Tomb of Banentiu', CAST(28.3512710000 AS Decimal(13, 10)), CAST(28.8809640000 AS Decimal(13, 10)), N'img/Anc_25.jpg', N'The Best Money Could Buy', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Wine from the Bahariya Oasis brought so much wealth in the past that a local merchant built himself a tomb fit for a pharaoh. The preserved Ancient Egyptian wall paintings within the tomb, which depict mythological celestial journeys and mortuary rituals, are still rich and earthy.', 6)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_26', N'powered by ITIANs', N'Tombs of Beni Hassan', CAST(27.9098990000 AS Decimal(13, 10)), CAST(30.8641570000 AS Decimal(13, 10)), N'img/Anc_26.jpg', N'Ancient Egyptian Cemetary Near Minya', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Known as the Tombs of Beni Hassan, this Ancient Egyptian cemetery was used and built for provincial governors during the Middle Kingdom over an older burial site used during the Old Kingdom period. The tombs reflect shifts in political power between the pharaoh and governors. When pharaohs were weak governors became powerful hereditary rulers and when the pharaoh centralized power, they appointed the governors.', 16)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_27', N'powered by ITIANs', N'Tombs of the Nobles', CAST(24.1026430000 AS Decimal(13, 10)), CAST(32.8903030000 AS Decimal(13, 10)), N'img/Anc_27.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The illuminated view of the northern hills of the west bank of the Nile is a truly magical one, not to be missed. From Aswan, you''ll have front row seats to witness this particularly beautiful and inspiring panorama.', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_28', N'powered by ITIANs', N'Valley of the Kings', CAST(25.7406380000 AS Decimal(13, 10)), CAST(32.6020660000 AS Decimal(13, 10)), N'img/Anc_28.jpg', N'Pharaohs of the Sedge & Bee', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Situated on the ancient site of Thebes, on Luxor''s West Bank, the Valley of Kings is the ancient burial ground of many of Egypt''s New Kingdom rulers.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_29', N'powered by ITIANs', N'Valley of the Queens', CAST(25.7277970000 AS Decimal(13, 10)), CAST(32.5935490000 AS Decimal(13, 10)), N'img/Anc_29.jpg', N'Queens, Princes, Princesses and Nobles', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located on the West Bank of Luxor near the Valley of the Kings, the Valley of the Queens is the place where wives of Pharaohs were buried in ancient times as well as princes, princesses and various members of the nobility.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_3', N'powered by ITIANs', N'Temple of Hatshepsut', CAST(25.7382350000 AS Decimal(13, 10)), CAST(32.6073320000 AS Decimal(13, 10)), N'img/Anc_3.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located beneath massive cliffs near the west bank of the Nile, the Mortuary Temple of Hatshepsut, also known as Deir el Bahri, is dedicated to Amon-Ra, the sun god. Designed by an architect named Senemut, the temple is unique because it was designed like classical architecture. Note the lengthy, colonnaded terrace some of which are 97 ft high, pylons, courts, and hypostyle hall. Inside you’ll see the sun court, chapel and sanctuary. Temple reliefs depict the tale of the divine birth of Hatshepsut and trade expeditions to the Land of Punt (a reference to modern Somalia or the Arabian Peninsula).', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_30', N'powered by ITIANs', N'Roman Mosaics', CAST(31.1945010000 AS Decimal(13, 10)), CAST(29.9039830000 AS Decimal(13, 10)), N'img/Anc_30.jpg', N'Villa of the Birds', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Visit a Roman villa dubbed the “Villa of the Birds” to see stunning mosaics of birds that adorn the walls and the floor. Located next to the Roman Amphitheatre of Kom El- Dikka, the villa is one the newest discoveries in Alexandria.', 3)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_31', N'powered by ITIANs', N'The Great Sphinx of Giza', CAST(29.9777300000 AS Decimal(13, 10)), CAST(31.1319460000 AS Decimal(13, 10)), N'img/Anc_31.jpg', N'Witness the Eternal Vigil', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The greatest mystery of Ancient Egyptian mysteries is also the largest monolithic statue and the oldest known monumental sculpture in the world. When was it built, for what purpose, which pharaoh does it represent, and who broke the nose? Any answer is a matter of conjecture. Egyptologists have not found any conclusive evidence. No matter. The Great Sphinx of Giza is a wonder to behold.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_32', N'powered by ITIANs', N'The Osireion at Abydos', CAST(26.1844270000 AS Decimal(13, 10)), CAST(31.9186480000 AS Decimal(13, 10)), N'img/Anc_32.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Not to be missed on a Nile Valley trip is the Temple of Osireion at Abydos; just a little to the North of Luxor where the Ancient city of Abydos once stood, you can find this unique L shaped temple which served seven Gods of Ancient Egypt. Osiris, Isis, Horus, Amon Ra, Ra Horakhty, Ptah, and the deified Seti are each assigned a shrine and a chamber in which you can discover well preserved ancient art and writings; the temple is also the supposed resting place of the head of the God Osiris. At the back of the temple is a structure known as the Osireion, built in a way resembling the tombs in the Valley of the Kings, a dummy sarcophagus was found in the burial chamber of this building.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_33', N'powered by ITIANs', N'Ramesseum', CAST(25.7272890000 AS Decimal(13, 10)), CAST(32.6108600000 AS Decimal(13, 10)), N'img/Anc_33.jpg', N'Ramses II’s Mortuary Temple', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The mortuary temple of Ramses II took around 20 years to build as a part of his funerary complex. The magnificent temple is onLuxor’s West Bank and in its day the Ramesseum was similar to Abu Simbel in grandeur and Medinet Habu in architecture. Ironically the Nile floods deteriorated the temple in which Ramses is portrayed as an eternal deity. Make sure to notice the images of the Battle of Qadesh in which the pharaoh is shown firing his arrows at the retreating enemy. The colossal statues of Ramses which once stood tall at the entrance are now in ruins but you can still see parts scattered around the temple.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_34', N'powered by ITIANs', N'Deir Al-Haggar', CAST(25.6460450000 AS Decimal(13, 10)), CAST(28.7943650000 AS Decimal(13, 10)), N'img/Anc_34.jpg', N'A Restored Roman Temple', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Deir Al-Haggar, literally "monastery of stone" is a restored roman sandstone temple built during the reign of Nero and dedicated to the Ancient Egyptian gods Amun, Mut and Khons. The paintings, cartouches and symbols etched on the walls of the temple are well preserved.', 7)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_35', N'powered by ITIANs', N'Aswan souk', CAST(24.0892000000 AS Decimal(13, 10)), CAST(32.8964960000 AS Decimal(13, 10)), N'img/Anc_35.jpg', N'Sharia as-Souk', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Aswan Souk is a colorful bazaar that offers Egyptian and African goods. Locally known as Sharia as-Souq, it is the cheapest place to buy souvenirs in Aswan.  Located about four blocks from the Nile and running about 7 blocks in parallel to the river, the bazaar has plenty of Egyptian and African goods. Traders sell a wide variety of goods such as perfumes, peanuts, henna powder, dried hibiscus flowers, spices, T-shirts and custom made Ancient Egyptian styled souvenirs.  In side alleys you’ll find traders selling Nubian artefacts such as skullcaps, talismans and baskets, Sudanese swords, spices and carpets, and stuffed animals such as crocodiles. ', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_36', N'powered by ITIANs', N'The Roman Amphitheatre of Kom el-Dikka', CAST(31.1946920000 AS Decimal(13, 10)), CAST(29.9039990000 AS Decimal(13, 10)), N'img/Anc_36.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Kom el-Dikka, which literally means a “pile of rubble,” was a slum until 1959 when a team of Poles excavated the site in search of the tomb of Alexander the Great. With 800 marble seats, graffiti of chariot team supporters, and two forecourts with mosaic flooring, the discovery was not a disappointment.', 3)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_37', N'powered by ITIANs', N'MOYXEION', CAST(31.2540500000 AS Decimal(13, 10)), CAST(29.9740880000 AS Decimal(13, 10)), N'img/Anc_37.jpg', N'The Graeco-Roman Museum', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Displaying a great collection of art and artefacts that date back to the origins and legacy of ancient Alexandria, the Graeco-Roman Museum has a neo-classical façade and the inscription-MOYXEION, which simply means museum in Greek. With 27 exhibits that showcase Hellenic statues, busts of Roman emperors, sarcophagi , mummies, Tangara figurines and early Christian artefacts, the museum captures over 2000 years of Graeco-Roman history in Egypt.', 3)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_39', N'powered by ITIANs', N'Qasr Ad-Dush', CAST(24.5690290000 AS Decimal(13, 10)), CAST(30.7021240000 AS Decimal(13, 10)), N'img/Anc_39.jpg', N'The Temple and Fortress of Dush', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'One of the oldest and most impressive fortresses of theWestern Desert lies about 100 km south of Kharga Oasis, in the very same location of the old town of Kysis which remains are still being excavated to this day by teams of archaeologists. Dating back to the 2nd century AD, the imposing fortress and temple of Dush were completed around 177 AD at the intersection of five major desert routes, including the road that lead to the temples of Esna and Edfu. The fortress’ solid architecture is a testimony to its importance; the building included as much as four storeys underground. The adjoining sandstone temple dates back to the 1st century and was dedicated to the Ancient Egyptian gods Isis and Serapis. Its walls were adorned with beautiful gold decorations in the past, most of these are sadly gone today but a few remain on the inner walls of the temple.', 10)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_4', N'powered by ITIANs', N'The Temple of Hibis', CAST(25.4764460000 AS Decimal(13, 10)), CAST(30.5551240000 AS Decimal(13, 10)), N'img/Anc_4.jpg', N'A Great Persian Landmark', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Temple of Hibis is a well preserved Persian temple set on a volcanic outcropping dating back to Ancient Egypt. Dedicated to the gods Amun, Mut and Khonsu, several additions to the temple were built up until the Ptolemaic reign over Egypt. The temple was originally built on the rim of a lake (now long gone).  Go ahead, walk along an avenue of sphinxes, past a series of gateways and the colonnade of Nectanebo', 10)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_40', N'powered by ITIANs', N'Mentuhotep II Mortuary temple', CAST(25.7368500000 AS Decimal(13, 10)), CAST(32.6052420000 AS Decimal(13, 10)), N'img/Anc_40.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The tomb of Mentuhotep I, who united Egypt under his reign during the 11th dynasty, is next to Deir al Bahri, on Luxor’s West Bank. Sadly the millennia have taken their toll on this once great temple, which includes architecture similar to that of the pyramids. The temple was located next to the cult site of the Goddess Hathor in ancient Thebes, and many Egyptologists believe that the construction’s shape was similar to that of nearby Deir al Bahri except with a pyramid shape protruding from the roof. The ruins are interesting and the core structure of the temple is still in good condition. Just a two minute walk from Deir al Bahri, it’s well worth the visit.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_41', N'powered by ITIANs', N'The Fortress of El-Gueita', CAST(25.2900490000 AS Decimal(13, 10)), CAST(30.5635210000 AS Decimal(13, 10)), N'img/Anc_41.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Situated on a summit, the sandstone fortress Qasr el-Gueita is a fortress that commands a strategic view of an ancient Western Desert trade route. Built and expanded by several Ancient Egyptian dynasties and Roman governors; the fortress contains a temple behind walls that take up to one-fifth of the space on the summit.', 10)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_42', N'powered by ITIANs', N'Pompey''s Pillar', CAST(31.1824900000 AS Decimal(13, 10)), CAST(29.8964210000 AS Decimal(13, 10)), N'img/Anc_42.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Has nothing to do with Pompey. The story behind the name refers to the possible burial ground of the Roman general when he fled to Egypt and was assassinated after losing a major battle in Greece against Caesar. The red granite column was probably built to honour the Emperor Diocletain. Today it stands 25 meters high and is the tallest ancient monument in Alexandria.', 3)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_43', N'powered by ITIANs', N'A Lone Monument of War', CAST(25.4841370000 AS Decimal(13, 10)), CAST(30.5671260000 AS Decimal(13, 10)), N'img/Anc_43.jpg', N'The Fortress of Ain Um Dabadib', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The stunning fortress of Ain Um Dabadib is an ancient high rise monument that sits below an escarpment but stands 220 meters above the desert floor. The fortress was occupied for thousands of years; scattered pots and ceramics that date back across Egyptian history can be found around the fort.', 10)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_44', N'powered by ITIANs', N'Hieroglyphics in Wadi Hammamat', CAST(26.0109110000 AS Decimal(13, 10)), CAST(33.6050610000 AS Decimal(13, 10)), N'img/Anc_44.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Before it became a modern road winding through the mountains of the Eastern Desert of Egypt from the coastal town of Quseir (135 km of Hurghada City) to Qibt in the Nile Valley, the trail was only known as Wadi Hammamat (The Valley of the many Baths), a dry river bed that served as a major trade route in Ancient times, and an area dotted with Bekhen quarries and gold mines, that were exploited from as far as the Old Kingdom up until the Roman era.', 9)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_45', N'powered by ITIANs', N'The Red Pyramid of Dahshur', CAST(29.8086730000 AS Decimal(13, 10)), CAST(31.2064430000 AS Decimal(13, 10)), N'img/Anc_45.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located at a short distance from Saqqara, the Red Pyramid at Dahshur was built for the pharaoh Snefru, father of Khufu for whom the Great Pyramid of Giza was built. The Red Pyramid is the first “true” pyramid and it has the second largest base of all Egyptian pyramids. What makes it yet more interesting is that it can be fully accessed, including the chance to see the burial chamber. There’s something special about exploring the cold and silent passageways inside the pyramid. Do not go inside if you are not comfortable in enclosed space.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_46', N'powered by ITIANs', N'Ramesseum', CAST(25.7272890000 AS Decimal(13, 10)), CAST(32.6108600000 AS Decimal(13, 10)), N'img/Anc_46.jpg', N'Ramses II’s Mortuary Temple', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The mortuary temple of Ramses II took around 20 years to build as a part of his funerary complex. The magnificent temple is onLuxor’s West Bank and in its day the Ramesseum was similar to Abu Simbel in grandeur and Medinet Habu in architecture. Ironically the Nile floods deteriorated the temple in which Ramses is portrayed as an eternal deity. Make sure to notice the images of the Battle of Qadesh in which the pharaoh is shown firing his arrows at the retreating enemy. The colossal statues of Ramses which once stood tall at the entrance are now in ruins but you can still see parts scattered around the temple.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_47', N'powered by ITIANs', N'Babylon Fortress', CAST(30.0061130000 AS Decimal(13, 10)), CAST(31.2297110000 AS Decimal(13, 10)), N'img/Anc_47.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located in Old Cairo, the fortress city known as the Babylon Fortress is the oldest part of Cairo. Built by the Romans, the fort was in a strategic position to dominate Egypt along the Nile. Persecution led Coptic Egyptians to take refuge within the fortifications. They built several churches and a monastery embedded within the fortress. Walk the length of the walls to see the fusion of Roman and Coptic architecture.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_48', N'powered by ITIANs', N'Gebel El-Mawta', CAST(29.2557470000 AS Decimal(13, 10)), CAST(25.5335710000 AS Decimal(13, 10)), N'img/Anc_48.jpg', N'Mountain of the Dead', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Gebel El-Mawta in Arabic means Mountain of the Dead; it is a cone shaped mount a little to the North of Shali Fortress, dotted with tombs from the 26th dynasty and the Ptolemaic period. Many of these tombs are still very well preserved. Moreover, from Gebel El-Mawta, you can catch amazing views of Siwa Oasis.', 13)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_49', N'powered by ITIANs', N'The Colossi of Memnon', CAST(25.7204850000 AS Decimal(13, 10)), CAST(32.6105030000 AS Decimal(13, 10)), N'img/Anc_49.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'On your way to visit the West Bank of Luxor, you will come across the two gigantic statues known as the Colossi of Memnon. These two gigantic figures of Amenhotep III were originally situated in front of his Mortuary temple, which seems to have been destroyed for unknown reasons. Each colossus is about 21 metres tall and represents King Amenhotep III seated on his throne. Legend has it that after an earthquake damaged it in 27 BC, one of the statues emitted strange sounds in the morning-perhaps due to the heat of the sun following the humidity of the night. However, the restoration which took place in 193-211 A.D, made the sound stop forever!', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_5', N'powered by ITIANs', N'The Temple of Luxor', CAST(25.7006050000 AS Decimal(13, 10)), CAST(32.6397980000 AS Decimal(13, 10)), N'img/Anc_5.jpg', N'Threads of Egyptian History', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Temple of Luxor is a must see site on any trip to Egypt; it is a testimony to the history of the continuous history of Egypt, beginning from the 18th dynasty of Ancient Egyptian rule to the 14th century AD when a mosque was built in the complex to commemorate Abu Al-Haggag, who is responsible for bringing Islam to Luxor.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_50', N'powered by ITIANs', N'Red Chapel of Hatshepsut', CAST(25.7205670000 AS Decimal(13, 10)), CAST(32.6575220000 AS Decimal(13, 10)), N'img/Anc_50.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The red and black walls of this ancient place of worship gave it its name; the temple was built by Hatshepsut and Thutmosis II in 1479 BC, and was intended to hold the barque of the God Amun; after the death of the Hatshepsut it was destroyed by her nephew Thutmose III. Luckily a group of archeologists found the ruins and reconstructed the shrine. The construction materials are black granite and red quartzite, and the beautiful engravings in the stone are filled with gold paint giving the shrine a magical feel. Right near the temple of Karnak – in the open-air museum area on the left of the main temple of Amun in Karnak – this is a building worth seeing.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_51', N'powered by ITIANs', N'Saqqara Step Pyramid', CAST(29.8713000000 AS Decimal(13, 10)), CAST(31.2163820000 AS Decimal(13, 10)), N'img/Anc_51.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Said to be the world’s oldest monumental masonry structure, the unique pyramid of Djoser in Saqqara is part of a mortuary complex for the 3rd Dynasty king Djoser. Created by the architect Imhotep, it is a unique stepped pyramid with 6 tiers. The blue tiles of Djoser’s tomb, the hieroglyphs in the pyramid of Pepi I, and the Doors of the Cats (Abwab el Qotat) are all breathtaking scenes.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_52', N'powered by ITIANs', N'Serabit el Khadem Temple', CAST(29.0376810000 AS Decimal(13, 10)), CAST(33.4474180000 AS Decimal(13, 10)), N'img/Anc_52.jpg', N'Serabit el Khadem Temple', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'On the western side of the Sinai is one of the rare traces left by the Ancient Egyptians in the peninsula. The Serabit el Khadem Temple of Hathor is located in the rocky mountainous landscape among an ancient turquoise mine settlement which provided the pharaohs with beautiful stones used to make ornaments, jewelry, and blue paint. You can see the huts of the miners, and the engravings they left on the walls, including images of the ships that transported the stones to the Nile Valley. Reaching the temple is an adventure; you will enjoy a 4X4 safari to get to the site, followed by a short hike. An example of how the pharaohs improved their structures throughout each dynasty, it is a beautiful and fascinating site with a stunning backdrop.', 12)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_53', N'powered by ITIANs', N'Seti I Temple', CAST(25.7327620000 AS Decimal(13, 10)), CAST(32.6280620000 AS Decimal(13, 10)), N'img/Anc_53.jpg', N'Seti I Temple Egypt''s Greatest Builder', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'In a scenic location surrounded by palm trees sits the Temple of Seti I, constructed by Seti I, son of Ramses I, and father of Ramses II. Seti I was one of Egypt’s greatest rulers and builders; he constructed one of the land’s most magnificent temples at Abydos, and the Great Hypostyle Hall in Karnak. The artwork and architecture commissioned during Seti’s rule is rich, no expense was spared and attention to detail is evident. Unfortunately he died before his temple was finished, and it was completed by his son Ramses II. The temple also includes a royal palace and a chapel for Ramses I.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_54', N'powered by ITIANs', N'Tell el Amarna Tombs', CAST(27.6511320000 AS Decimal(13, 10)), CAST(30.8985510000 AS Decimal(13, 10)), N'img/Anc_54.jpg', N'Akhetaten Today', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Where the city of Akhetaten once stood, we can still see the remains of the two temples dedicated to Aten (the sun god) whom the pharaoh Akenaten designated as the one true god of Egypt, as well as some royal tombs much like those in the Valley of the Kings. There are over 25 tombs on site, most are decorated with columns bearing the papyrus motif, and all with inscriptions of the “Hymn to the Sun” written by Akhenaten; the tombs also feature some lovely reliefs painted and engraved on the walls, which are some of the best preserved fromAncient Egypt. It is easy to get lost in the tombs so please visit them with a guide.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_55', N'powered by ITIANs', N'Temple Fort of An-Nadura', CAST(25.4658910000 AS Decimal(13, 10)), CAST(30.5610430000 AS Decimal(13, 10)), N'img/Anc_55.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Drive a little over a mile outside of Al-Kharga to the Roman temple and fortress ruins for a brilliant view of the desert. Note the paintings of women playing percussion instruments on the temple walls.', 10)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_56', N'powered by ITIANs', N'Temple of Alexander the Great', CAST(28.3421330000 AS Decimal(13, 10)), CAST(28.8221030000 AS Decimal(13, 10)), N'img/Anc_56.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Temple of Alexander was built during his reign on a site he may have visited during a journey he made to consult the oracle of Amun in Siwa Oasis. Located about 5km east of Bawati, the only town in Bahariya, the sandstone temple has many mud-brick rooms that serviced the priests and the principal diety.  Stand next to the bas-relief of Alexander, say cheese!', 6)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_57', N'powered by ITIANs', N'Temple of Amenhotep III', CAST(25.7208860000 AS Decimal(13, 10)), CAST(32.6100120000 AS Decimal(13, 10)), N'img/Anc_57.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'At its time the Mortuary temple of Amenhotep III was the largest in Egypt, it was built in a way in which the Nile would flood all of it except the burial chamber and then it would emerge from the river each year. This beautiful concept was not very successful when it came to the permanence of the mostly mud brick temple, another factor in its reduction was that several other pharaohs took building materials from it to build their own constructions. The main remnants of the temple are the Colossi of Memnon, two figures of Amenhotep III, which once sat at the temple’s doors. When exploring the site you will see a number of statues and figures as well as the structure and remaining column bases of the temple. ', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_58', N'powered by ITIANs', N'Temple of Amun', CAST(29.2519310000 AS Decimal(13, 10)), CAST(25.5214910000 AS Decimal(13, 10)), N'img/Anc_58.jpg', N'The Temple of Umm Ubayd', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Also known as the Temple of Umm Ubayd, this temple was once joined with the nearby Temple of the Oracle by a causeway. Both temples are dedicated to the God Amun and rituals to do with the oracle involved both sanctuaries. The temple of Amun was built in the 30th dynasty by Nectanebo II and – sadly – it is mostly destroyed. In the late 1890’s the governor of Siwa used gunpowder to demolish the temple in order to utilize the stones to build the local police station and other buildings. What’s left of it today is a wall covered with ancient inscriptions.', 13)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_6', N'powered by ITIANs', N'Temple of Khnum', CAST(25.2940780000 AS Decimal(13, 10)), CAST(32.5575190000 AS Decimal(13, 10)), N'img/Anc_6.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Dedicated to the ram-headed creator god, the Graeco-Roman Temple of Khnum was built during the reign of the Roman emperor Claudius. All that remains of the temple is the hypostyle hall, a roof supported by 24 columns ornately decorated with scenes of the countryside and hymns to Khnum, was excavated in the 1840s. Note the Roman floral and celestial scenes carved alongside hieroglyphs depicting temple rituals.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_7', N'powered by ITIANs', N'Mandulis Unveiled', CAST(23.9608390000 AS Decimal(13, 10)), CAST(32.8673160000 AS Decimal(13, 10)), N'img/Anc_7.jpg', N'The Temple of Kalabsha', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Walk along an imposing stone causeway that leads from the banks of the lake to the first pylon of the temple, pass a colonnaded court and into the eight columned hypostyle hall. Note the hieroglyphs and the reliefs of Greek pharaohs paying homage to Ancient Egyptian deities. Look for Mandulis, the god clad in the vulture feathered cloak.', 2)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_8', N'powered by ITIANs', N'Temple of Horus', CAST(24.9792420000 AS Decimal(13, 10)), CAST(32.8753360000 AS Decimal(13, 10)), N'img/Anc_8.jpg', N'200 Years in the Making', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The well preserved Ptolemaic Temple of Horus is the second biggest temple in Egypt. Second only to the Temple of Karnak in sheer size, the temple was one of the last attempts by the Ptolemaic dynasty at building in the style and grandeur of their predecessors. Construction on the site took about 200 years. Well preserved hieroglyphs have shed light on the practices of the cult of Horus and associated temples. The temple is believed to have been built on the site of the great Horus-Seth battle.', 4)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Anc_9', N'powered by ITIANs', N'The Temple of Hibis', CAST(25.4764460000 AS Decimal(13, 10)), CAST(30.5551240000 AS Decimal(13, 10)), N'img/Anc_9.jpg', N'A Great Persian Landmark', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Temple of Hibis is a well preserved Persian temple set on a volcanic outcropping dating back to Ancient Egypt. Dedicated to the gods Amun, Mut and Khonsu, several additions to the temple were built up until the Ptolemaic reign over Egypt. The temple was originally built on the rim of a lake (now long gone).  Go ahead, walk along an avenue of sphinxes, past a series of gateways and the colonnade of Nectanebo', 10)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'geo_3', N'powered by ITIANs', N'The ITI Building', CAST(30.0709602000 AS Decimal(13, 10)), CAST(31.0211742000 AS Decimal(13, 10)), N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', N'The Location of the ITI Institute', N'classic', 0, 0, 0, 1, N'geo', N'http://localhost:8080/ganna.mp3', N'A great disappointment for Ancient Egyptian stonemasons but an awe-inspiring site for tourists, the Unfinished Obelisk inAswan is the heaviest monolith found in Egypt. About 42 meters long and weighing an estimated 1168 tons, the obelisk would have been the largest piece of Egyptian masonry had it been completed. The obelisk was probably left unfinished after it cracked at the middle. It is still attached to the parent rock in the Northern Quarries, where the Ancient Egyptian quarried granite. Note the tool marks and pictographs of dolphins and birds engraved on this colossal obelisk.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'geo_97', N'powered by ITIANs', N'ATG Group', CAST(30.0718469000 AS Decimal(13, 10)), CAST(31.0205974000 AS Decimal(13, 10)), N'http://www.nysun.com/pics/8226.jpg', N'Telal Abu Gazala Group (ATG)', N'classic', 0, 0, 0, 1, N'geo', N'http://localhost:8080/ganna.mp3', NULL, 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'geo_98', N'جميع الحقوق محفوظه 2012', N'فودافون القريه الذكيه', CAST(30.0732721000 AS Decimal(13, 10)), CAST(31.0177597000 AS Decimal(13, 10)), N'http://farm4.staticflickr.com/3269/2481315696_e6069359f9_z.jpg', N'مبني فودافون القريه الذكيه 6 اكتوبر.', N'classic', 0, 0, 0, 1, N'geo', N'http://localhost:8080/ganna.mp3', NULL, 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'geo_99', N'powered by ITIANs', N'The NTI Building', CAST(30.0719653000 AS Decimal(13, 10)), CAST(31.0217535000 AS Decimal(13, 10)), N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', N'The Location of the national Telecommunication Institute.', N'classic', 0, 0, 0, 1, N'geo', N'http://localhost:8080/ganna.mp3', NULL, 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_1', N'powered by ITIANs', N'Al-Amir Shaykhu al-''Umari Mosque and Khanqah ', CAST(30.0307550000 AS Decimal(13, 10)), CAST(31.2529000000 AS Decimal(13, 10)), N'img/Isl_1.png', N'Twins on Al-Saliba Street in Cairo', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Two almost identical buildings face each other on Al-Saliba Street in Cairo; these are the mosque and Khanqah of Shaykhu or Amir Shaykhu Al-''Umari. The mosque was built in 1349, followed five years later by the Khanqah.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_10', N'powered by ITIANs', N'Amir Akhur Qanibay Madrasa and Mosque', CAST(30.0331380000 AS Decimal(13, 10)), CAST(31.2538880000 AS Decimal(13, 10)), N'img/Isl_10.png', N'On a large hill overlooking the gigantic Madrasa Mosque of Sultan Hassan, in lavishing green surroundings, lies the small mosque and madrasa complex of Amir Akhur Qanibay', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'On a large hill overlooking the gigantic Madrasa Mosque of Sultan Hassan, in lavishing green surroundings, lies the small mosque and madrasa complex of Amir Akhur Qanibay, who was also known as “Al-Rammah” (the lancer) back at the time when he served Sultan al-Ghuri as his grand master of the horses. In 1503, Amir Akhur Qanibay decided to build this complex in this very location to be close to the horse market and citadel stables which were located a few metres away from the citadel square.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_11', N'powered by ITIANs', N'Amir Altunbugha Al-Maridani Mosque ', CAST(30.0398480000 AS Decimal(13, 10)), CAST(31.2591870000 AS Decimal(13, 10)), N'img/Isl_11.png', N'Set amidst the hustle and bustle of Cairo, in the Darb Al-Ahmar district', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Set amidst the hustle and bustle of Cairo, in the Darb Al-Ahmar district, lies the very serene 14th century mosque of Al-Maridani which houses an interior garden with trees and running water, where birds come to play in the heat of the city.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_12', N'powered by ITIANs', N'Amir Sarghitmish Madrassa and Mausoleum', CAST(30.0297670000 AS Decimal(13, 10)), CAST(31.2491710000 AS Decimal(13, 10)), N'img/Isl_12.png', N'Famous for its beautiful lanterns, this small institution is located right next to the Mosque of Ibn Tulun on Al-Saliba Street in Cairo.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Famous for its beautiful lanterns, this small institution is located right next to the Mosque of Ibn Tulun on Al-Saliba Street in Cairo.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_13', N'powered by ITIANs', N'Bab Al-Futuh ', CAST(30.0554500000 AS Decimal(13, 10)), CAST(31.2633960000 AS Decimal(13, 10)), N'img/Isl_13.png', N'The Gate of Conquest or Bab Al-Futuh is the northernmost of the three remaining old gates of Cairo. It once served as the northern entrance to the city.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Gate of Conquest or Bab Al-Futuh is the northernmost of the three remaining old gates of Cairo. It once served as the northern entrance to the city.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_14', N'powered by ITIANs', N'Bab Al-Nasr', CAST(30.0542120000 AS Decimal(13, 10)), CAST(31.2649640000 AS Decimal(13, 10)), N'img/Isl_14.png', N'Meaning Gate of victory, Bab Al-Nasr is one of Cairo’s old gates. ', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Meaning Gate of victory, Bab Al-Nasr is one of Cairo’s old gates. Built in 1087, it served as one of the northern gates to the Fatimid Cairo. Unlike the cylindrical towers of Bab Zuweila and Bab Al-Futuh, the two towers of Bab Al-Nasr are rectangular in shape and you can see some Byzantine influence in their architecture. Many of the stones used to erect these gates were taken from Pharaonic monuments and if you look closely enough you might even spot some hieroglyphs.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_15', N'powered by ITIANs', N'Bab Zuweila ', CAST(30.0427520000 AS Decimal(13, 10)), CAST(31.2577220000 AS Decimal(13, 10)), N'img/Isl_15.png', N'Being one of the three ancient gates of Cairo that still stand, Bab Zuweila is a stunning example of Fatimid architecture; it marks the southernmost end of the old Fatimid city.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Being one of the three ancient gates of Cairo that still stand, Bab Zuweila is a stunning example of Fatimid architecture; it marks the southernmost end of the old Fatimid city.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_16', N'powered by ITIANs', N'Beyt Al-Harrawi', CAST(30.0445280000 AS Decimal(13, 10)), CAST(31.2629660000 AS Decimal(13, 10)), N'img/Isl_16.png', N'While it’s a historic building, Beyt Al-Harrawi is also all about music. So much so that it also goes by the moniker Beyt Al-Oud (house of the lute). ', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'While it’s a historic building, Beyt Al-Harrawi is also all about music. So much so that it also goes by the moniker Beyt Al-Oud (house of the lute). In fact, the director of the house is one of the Arab world''s foremost Oud players, Naseer Shamma, known for his innovations when it comes to the instrument. The space has been dedicated to music and visitors can enjoy concerts in its open-air courtyard.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_17', N'powered by ITIANs', N'Beyt El-Suhaimi ', CAST(30.0522620000 AS Decimal(13, 10)), CAST(31.2626020000 AS Decimal(13, 10)), N'img/Isl_17.png', N'The Finest Example of Traditional Residences', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Gazing at the typically plain and maybe – to some – even dull façade of Beyt el-Suhaimi, you cannot imagine the architectural treasures you will witness inside.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_18', N'powered by ITIANs', N'Beyt Zeinab Al-Khatoun', CAST(30.0450490000 AS Decimal(13, 10)), CAST(31.2634600000 AS Decimal(13, 10)), N'img/Isl_18.png', N'This is a house marked by women – from its elite 19th century female owner after which it is named', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'This is a house marked by women – from its elite 19th century female owner after which it is named, to the girls vocational school that operated there in the 1980’s, to its recent reopening after renovation by Egypt''s first Lady, Mrs. Suzanne Mubarak.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_19', N'powered by ITIANs', N'Gawhar al-Lala Mosque', CAST(30.0324250000 AS Decimal(13, 10)), CAST(31.2536990000 AS Decimal(13, 10)), N'img/Isl_19.png', N'At a short walking distance from the Citadel square, niched in a small street emerging from behind the grand Al-Rifai Mosque, lies the Gawhar al-Lala mosque. ', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'At a short walking distance from the Citadel square, niched in a small street emerging from behind the grand Al-Rifai Mosque, lies the Gawhar al-Lala mosque. This relatively small mosque covers a surface of a little less than 200 square meters. Dating back to 1430 AD, the mosque holds the name of its founder, Amir Gawhar al-Lala, a slave who was raised to the rank of an important servant in Sultan Barsbay’s court. He earned his title “Al-Lala” by serving as a private tutor to the Sultan’s sons. ', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_2', N'powered by ITIANs', N'Al-Aqmar Mosque ', CAST(30.0516020000 AS Decimal(13, 10)), CAST(31.2620130000 AS Decimal(13, 10)), N'img/Isl_2.png', N'A rather small mosque, Al-Aqmar mosque makes up for its size with its architectural and historical significance. Located near Al-Qalawun Complex Al-Aqmar is one of the only remaining Fatimid mosques in Cairo.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'A rather small mosque, Al-Aqmar mosque makes up for its size with its architectural and historical significance. Located near Al-Qalawun Complex Al-Aqmar is one of the only remaining Fatimid mosques in Cairo.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_20', N'powered by ITIANs', N'Khan El-Khalili', CAST(30.0476790000 AS Decimal(13, 10)), CAST(31.2609610000 AS Decimal(13, 10)), N'img/Isl_20.png', N'A Charming Labyrinth of Narrow Alleys', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'No visit to Cairo is Complete without a stop at the Khan El-Khalili bazaar, where you will be transported back in time to an old Arab souk.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_21', N'powered by ITIANs', N'Madrassa and Mausoleum of Sultan al-Zahir Barquq', CAST(30.0502850000 AS Decimal(13, 10)), CAST(31.2612950000 AS Decimal(13, 10)), N'img/Isl_21.png', N'Closely linked to Khan el-Khalili', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'In the late 14th century, Sultan Barquq decided to build a complex in the coveted surroundings of the Qalawunid monuments on the famous Mo''ezz Li Din-Illah Street in Cairo. Barquq was of Circassian origins, he was acquired by the Turkish Bahri Mamluks and after regaining his freedom in 1363, he seized power over Egypt by taking advantage Sultan Al-Nassir Muhammad''s descendants'' weakness.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_22', N'powered by ITIANs', N'Mohamed Ali Mosque ', CAST(30.0291350000 AS Decimal(13, 10)), CAST(31.2593310000 AS Decimal(13, 10)), N'img/Isl_22.png', N'The Alabaster Mosque', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Visiting Cairo, you will easily locate the Mohamed Ali Mosque, due to its prominent features: its dome rises up to 52 metres high and two east side minarets reach not less than 84 meters. While wandering around the mosque, you will soon discover why it also holds the name of the “Alabaster Mosque.” Its interior and exterior walls are amazingly coated with alabaster to the height of 11 metres. The Mohammed Ali Mosque crowns the Citadel of Salah el-Din in Cairo. It was built between 1830 and 1848 by the architect Yousef Bushnak and upon Mohamed Ali Pasha’s request.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_23', N'powered by ITIANs', N'Mosque of Abu Al-Dahab ', CAST(30.0460290000 AS Decimal(13, 10)), CAST(31.2618050000 AS Decimal(13, 10)), N'img/Isl_23.png', N'A Mix of Mamluk and Ottoman Influences', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Through its architecture, this exquisite mosque conveys the period of struggle for Cairo, between the Ottomans and Mamluks. Sadly, it is overshadowed by its more prominent neighbor, Al-Azhar Mosque.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_24', N'powered by ITIANs', N'Mosque of Al-Salih Tala''i ', CAST(30.0423060000 AS Decimal(13, 10)), CAST(31.2580540000 AS Decimal(13, 10)), N'img/Isl_24.png', N'Arabesques and Beautiful Calligraphy', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Also known as the "hanging mosque" as it is located above street level, it is the "most recently" built Fatimid mosque in Cairo. Located near Bab Zuweila, it boasts beautiful Kufic Koranic inscriptions on it walls and pillars. Under it, there are shops which pay for the mosque’s financial needs.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_25', N'powered by ITIANs', N'Mosque of Amr Ibn Al-''As ', CAST(30.0101040000 AS Decimal(13, 10)), CAST(31.2331190000 AS Decimal(13, 10)), N'img/Isl_25.png', N'First Mosque in Egypt and Africa', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Built in 642 AD with palm trunks and fronds, it was the first mosque erected in Egypt and all Africa.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_26', N'powered by ITIANs', N'Mosque of Ibn Tulun ', CAST(30.0287270000 AS Decimal(13, 10)), CAST(31.2495790000 AS Decimal(13, 10)), N'img/Isl_26.png', N'The Largest Mosque in Cairo', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Looking more like a fortress with its imposing wall full of beautiful crenulations, the Mosque of Ibn Tulun is believed to be the oldest mosque in Cairo, and the city’s largest mosque in terms of land area, which covers no less than 26.300 sq. m.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_27', N'powered by ITIANs', N'Museum of Islamic Art ', CAST(30.0442940000 AS Decimal(13, 10)), CAST(31.2524690000 AS Decimal(13, 10)), N'img/Isl_27.png', N'The Muslim Heritage of Egypt', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Displaying over 10,000 articles dating back to the Islamic era in Egypt, this is one place you don’t want to miss while in Cairo.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_28', N'powered by ITIANs', N'Nilometer on Rhoda Island ', CAST(30.0073880000 AS Decimal(13, 10)), CAST(31.2250520000 AS Decimal(13, 10)), N'img/Isl_28.png', N'The Nile''s Soothsayer', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located on the lower end of Rhoda Island, in Cairo, the Nilometer was used to measure the level of the river in times past. The structure consists of a measuring device, or a graduated column sitting below the Nile''s level, reached by steps that curl around the chamber housing the column.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_29', N'powered by ITIANs', N'Sabil Umm Abbas', CAST(30.0308190000 AS Decimal(13, 10)), CAST(31.2523690000 AS Decimal(13, 10)), N'img/Isl_29.png', N'One of the “Newest” Sabils', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'A rather “recent” landmark stands tucked in between the old Mamluk mosques and buildings on Saliba Street in Cairo: it’s Sabil Umm Abbas, a fountain that was built in the mid-19th century by the mother of Abbas II, the last Khedive of Egypt who ruled the country from 1892 to 1914.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_3', N'powered by ITIANs', N'Al-Azhar Mosque ', CAST(30.0449990000 AS Decimal(13, 10)), CAST(31.2640200000 AS Decimal(13, 10)), N'img/Isl_3.png', N'Located at the heart of Islamic Cairo, the Al-Azhar complex, mosque and university, does not only house the oldest university in the world but it is also the place where the graduation black gowns originated from. ', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located at the heart of Islamic Cairo, the Al-Azhar complex, mosque and university, does not only house the oldest university in the world but it is also the place where the graduation black gowns originated from. The costume worn by students all around the world during their graduation seems to have been inspired by the flowing robes of the Islamic Scholars “graduating” from Al-Azhar.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_30', N'powered by ITIANs', N'Sabil-Kuttab Abd El-Rahman Katkhuda', CAST(30.0503950000 AS Decimal(13, 10)), CAST(31.2614020000 AS Decimal(13, 10)), N'img/Isl_30.png', N'A True Architectural Gem', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Described as a "treasure of Ottoman architecture", this Sabil Kuttab was built in 1744 by Abd El-Rahman Katkhuda, a renowned architect. Its purpose was to provide the thirsty with the blessing of water, and children with the blessing of education. The edifice was constructed in the Mamluk style with impressive and intricate designs on all its facades, whether in the coloured marble or the wooden mashrabiyyas.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_31', N'powered by ITIANs', N'Salah El-Din Citadel in Cairo', CAST(30.0291850000 AS Decimal(13, 10)), CAST(31.2619360000 AS Decimal(13, 10)), N'img/Isl_31.png', N'Bastion of Islam', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'When in Cairo, it is basically impossible to miss the Salah El-Din Citadel , one of the world’s greatest monuments to medieval warfare. Resembling a typical early medieval fortress, with large imposing gateways, towers and high defending walls, the Citadel is one of Cairo''s main attractions and probably the most popular non-pharaonic monument in the Egyptian capital.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_32', N'powered by ITIANs', N'Salih Najm al-Din Ayyub Funerary Complex', CAST(30.0493200000 AS Decimal(13, 10)), CAST(31.2612150000 AS Decimal(13, 10)), N'img/Isl_32.png', N'First Mausoleum-Madrassa Complex in Cairo', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located on the famous Moezz Street in Cairo, the Funerary complex of al-Malik al-Salih Najm al-Din Ayyub dates back to the mid-13th century. Named after its builder, the Ayyubid ruler who reigned over Egypt from 1240 till 1249, it was the first complex to be built in what became a typical Mamluk style: a tomb linked up to a “Madrassa” (a theological school). Way back in the Mamluk period, the building played the role of a courthouse, where religious judges, referred to as “qadi” heard cases that were referred to them from lower tribunals.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_33', N'powered by ITIANs', N'Sulayman Agha Al-Silahdar Mosque', CAST(30.0525760000 AS Decimal(13, 10)), CAST(31.2619070000 AS Decimal(13, 10)), N'img/Isl_33.png', N'It is renowned for its "pencil-like" minaret: a tall and thin structure built in the Ottoman style.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Named after its founder, Sulayman Agha Al-Silahdar, one of Mohamed Ali''s lieutenants, this beautiful mosque located on Al-Mo''ez Street in Cairo dates back to 1839.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_34', N'powered by ITIANs', N'Sultan Hassan Mosque and Madrassa ', CAST(30.0320710000 AS Decimal(13, 10)), CAST(31.2564670000 AS Decimal(13, 10)), N'img/Isl_34.png', N'Splendid Mamluk Architecture in Cairo', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Mosque-Madrassa of Sultan Hassan is regarded as the finest piece of early-Mamluk architecture era in Cairo, an age of architectural splendour.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_35', N'powered by ITIANs', N'The Blue Mosque ', CAST(30.0359810000 AS Decimal(13, 10)), CAST(31.2599860000 AS Decimal(13, 10)), N'img/Isl_35.png', N'The Aqsunkur Mosque', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Mamluk style Blue Mosque also known as the Aqsunqur Mosque is located in old Cairo, and was built in 1347. It boasts beautiful bluish marble on its outer walls, and inside is flowery tiling in Ottoman fashion also in blue, giving the mosque its name; these tiles were added during a 1652 restoration.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_36', N'powered by ITIANs', N'The Gayer Anderson Museum ', CAST(30.0284650000 AS Decimal(13, 10)), CAST(31.2506470000 AS Decimal(13, 10)), N'img/Isl_36.png', N'It consists of an amazing patchwork of Islamic styles and artefacts packed into two wonderful ancient residences: Beit el-Kiridiliya  and Beit Amna Bent Salim.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Gayer Anderson Museum in Cairo is a must-see on your exploration tour of Islamic Cairo. It consists of an amazing patchwork of Islamic styles and artefacts packed into two wonderful ancient residences: Beit el-Kiridiliya (1632) and Beit Amna Bent Salim (1540).', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_4', N'powered by ITIANs', N'Al-Ghouri Complex ', CAST(30.0467450000 AS Decimal(13, 10)), CAST(31.2591590000 AS Decimal(13, 10)), N'img/Isl_4.png', N'Sultan Qansuh Al-Ghouri was a Mamluk sultan', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Sultan Qansuh Al-Ghouri was a Mamluk sultan, who had reigned from 1501 to 1516, before dying in a battle against the Ottomans in Aleppo, which resulted in a complete defeat for the Mamluks, due to which they lost their prominence in Egypt.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_5', N'powered by ITIANs', N'Al-Hakim Mosque ', CAST(30.0542100000 AS Decimal(13, 10)), CAST(31.2644230000 AS Decimal(13, 10)), N'img/Isl_5.png', N'Located near Bab al-Futuh, at the beginning of Al-Mo’ez Street, you’ll find one of the largest Fatimid mosques in Cairo: the Al-Hakim Mosque.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located near Bab al-Futuh, at the beginning of Al-Mo’ez Street, you’ll find one of the largest Fatimid mosques in Cairo: the Al-Hakim Mosque.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_6', N'powered by ITIANs', N'Al-Hussein Mosque ', CAST(30.0477850000 AS Decimal(13, 10)), CAST(31.2630430000 AS Decimal(13, 10)), N'img/Isl_6.png', N'Located near the famous Khan el-Khalili bazaar in ', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located near the famous Khan el-Khalili bazaar in Cairo and – as it was discovered during works on the mosque’s foundations in the 1900’s – on the remains of the Fatimid Caliphs cemetery in Cairo, the Al-Hussein mosque is considered as one of the holiest mosques in Egypt.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_7', N'powered by ITIANs', N'Al-Nassir Mohamed Mosque ', CAST(30.0496990000 AS Decimal(13, 10)), CAST(31.2609730000 AS Decimal(13, 10)), N'img/Isl_7.png', N'One of the three mosques in the Cairo Citadel, Al-Nassir Mohamed mosque may seem minuscule next to the Mohamed Ali Mosque', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'One of the three mosques in the Cairo Citadel, Al-Nassir Mohamed mosque may seem minuscule next to the Mohamed Ali Mosque, it is however the only remaining Mamluk contribution to the Citadel.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_8', N'powered by ITIANs', N'Al-Qalawun Complex ', CAST(30.0291390000 AS Decimal(13, 10)), CAST(31.2608550000 AS Decimal(13, 10)), N'img/Isl_8.png', N'Located on Al-Mo’ez Street in Cairo, the Qalawun complex was built by the Mamluk Sultan Al-Nassir in 1304 AD in honour of his father Qalawun.', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Located on Al-Mo’ez Street in Cairo, the Qalawun complex was built by the Mamluk Sultan Al-Nassir in 1304 AD in honour of his father Qalawun.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Isl_9', N'powered by ITIANs', N'Al-Rifa''i Mosque ', CAST(30.0327390000 AS Decimal(13, 10)), CAST(31.2569280000 AS Decimal(13, 10)), N'img/Isl_9.png', N'Al-Rifa''i Mosque is one of Cairo''s largest mosques. It took 43 years to build this beautiful sanctuary that combines different Islamic architectural styles. ', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Al-Rifa''i Mosque is one of Cairo''s largest mosques. It took 43 years to build this beautiful sanctuary that combines different Islamic architectural styles. Located in "Midan Al-Qalaa" or the Citadel Square, it faces the Mosque-Madrassa of Sultan Hassan which it rivals in size and grandeur.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_1', N'powered by ITIANs', N'Abu Gallum', CAST(28.7293300000 AS Decimal(13, 10)), CAST(34.6258950000 AS Decimal(13, 10)), N'img/Nat_1.jpg', N'Sinai Protected Area', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Abu Gallum is the third of the five protected areas of South Sinai and is located to the north of Nabq. High coastal mountains are represented as well as the coral reefs for which the Red Sea is famous. Along the Gulf of Aqaba the coastal plain is narrow and this protected area plays an important role in regulating land use. The Nubian Ibex,Capra nubiana, is found on virtually inaccessible peaks and the reefs, which are among the finest in the world, are still in pristine condition.', 12)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_10', N'powered by ITIANs', N'Wadi El Hitan', CAST(29.3850470000 AS Decimal(13, 10)), CAST(30.2325440000 AS Decimal(13, 10)), N'img/Nat_10.jpg', N'Visit the Valley of the Whales in Egypt', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Within two hours driving distance from Cairo, in the Fayoum area, you’ll discover a yet more impressive meaning of “Ancient” Egypt. The remote valley of Wadi El-Hitan (Valley of the Whales) is more of an open-air museum displaying rare gigantic fossils of ancient whales and sharks proving that some 40 to 50 million years ago, the area was submerged in the waters of what is known as the Tethys Sea.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_11', N'powered by ITIANs', N'Wadi Natrun', CAST(30.4189680000 AS Decimal(13, 10)), CAST(30.3333950000 AS Decimal(13, 10)), N'img/Nat_11.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Wadi Natrun is a valley located along the desert road betweenCairo and Alexandria that is close to a number of lakes and marshes. Here you’ll find Sandplover, Rufous Bushchats and Blue-cheecked Bee-eaters.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_12', N'powered by ITIANs', N'Wadi el-Rayan', CAST(29.4410330000 AS Decimal(13, 10)), CAST(30.8761600000 AS Decimal(13, 10)), N'img/Nat_12.jpg', N'Lakes and Waterfalls in the Egyptian desert', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The natural protected area of Wadi El-Rayan is a small valley southwest of the Fayoum oasis, the closest Western Desertoasis to Cairo (2 hours driving distance). Two man-made lakes, created by agricultural run-off water from the Fayoum oasis, are joined by a channel and charming waterfalls. The upper lake is densely vegetated whereas the lower lake is brackish and its shores are poorly vegetated. The lakes are wintering habitat for water birds migrating from south and north to Egypt.', 1)
GO
print 'Processed 100 total records'
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_2', N'powered by ITIANs', N'Cairo Giza Zoo and Garden', CAST(30.0320360000 AS Decimal(13, 10)), CAST(31.2124440000 AS Decimal(13, 10)), N'img/Nat_2.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'There are plenty of bird watching opportunities in Cairo, especially in and around the Giza Zoo. Here you’ll find caged birds in the zoo and free roaming birds in the gardens such asNile Valley Sunbirds and Cattle Egrets as well as many migrating songbird species.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_3', N'powered by ITIANs', N'Fayoum Oasis', CAST(29.4314650000 AS Decimal(13, 10)), CAST(30.8486940000 AS Decimal(13, 10)), N'img/Nat_3.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Fayoum Oasis is a wonderful bird watching destination, well known for its delicious fruits and vegetables. Birds migrate to the oasis for the lush plants and the waters of Lake Quarun, the largest salt water lake in Egypt. Here you’ll find Grebes, coots, ducks and shorebirds during winter.', 17)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_4', N'powered by ITIANs', N'Gezira Island', CAST(30.0509790000 AS Decimal(13, 10)), CAST(31.2168290000 AS Decimal(13, 10)), N'img/Nat_4.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'From the soaring edifice of Cairo Tower to the cultural powerhouse that’s Cairo Opera House and the exclusive Gezira Sporting Club, Gezira Island is a great escape from the touristic commotion of the typical Cairo destinations. The upscale residential area is a pleasant place to walk and there are plenty of good restaurants and cafes to choose from. ', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_5', N'powered by ITIANs', N'Maadi Petrified Forest', CAST(29.9683790000 AS Decimal(13, 10)), CAST(31.6033170000 AS Decimal(13, 10)), N'img/Nat_5.jpg', N'A Protectorate in the City', N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Maadi Petrified Forest is the remnants of a forest that grew 35 million years ago during a wetter period in Egypt. At the same period, great geological upheavals were taking place and theRed Sea was formed by the separation of the African and Arabian tectonic plates. The wadi is moderately vegetated and among the wildlife is Cape Hare, Lepus capensis, and small rodents like the Cairo Spiny Mouse, Acomys cahirinus. Birds are generally those of the Eastern Desert including Mourning Wheatear, Oenanthe lugens. Among reptiles there is the Pale Agama, Trapelus pallidus.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_6', N'powered by ITIANs', N'Lake Qarun', CAST(29.4254240000 AS Decimal(13, 10)), CAST(30.4924390000 AS Decimal(13, 10)), N'img/Nat_6.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Lake Qarun has been a source of fish and a habitat for waterfowl since time immemorial. The lake''s main water source is drainage from agricultural land, which enters through two major drains called el-Batts and el-Wadi. This water has become increasingly saline as agriculture has intensified and the water is now more saline than seawater. Freshwater fish and invertebrates have largely disappeared and marine species have been introduced. This lake is of international importance for wintering waterbirds including Black-necked Grebe, Podiceps nigricollis, and Northern Shoveller, Anas clypeata.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_7', N'powered by ITIANs', N'Sannur Cave', CAST(28.8668720000 AS Decimal(13, 10)), CAST(31.0832260000 AS Decimal(13, 10)), N'img/Nat_7.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Sannur Cave is a classic karst cave created by groundwater percolating through the Eocene limestone of the Galala Plateau. It is the best example of this type of cave in Egypt. As the water percolates downwards, excess calcium carbonates are deposited on the roof and floor of the cave forming spectacular stalactites and stalagmites of various forms. When a light is shone on them, they glitter like a wonderland. Above ground, there are deposits of the red soil (terra rossa) associated with such formations, as well as several swallow-holes (dolines).', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_8', N'powered by ITIANs', N'Suez Bird Watching And Canal Gazing', CAST(29.9852930000 AS Decimal(13, 10)), CAST(32.5250100000 AS Decimal(13, 10)), N'img/Nat_8.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'The Suez is a major concentration point for migratory birds of prey. While taking in the sight of the Suez Canal, you’ll spot shorebirds, White-eyed Gulls and Lesser Crested Terns.', 1)
INSERT [dbo].[layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (N'Nat_9', N'powered by ITIANs', N'Wadi Digla - Degla Valley', CAST(29.6764800000 AS Decimal(13, 10)), CAST(31.6912080000 AS Decimal(13, 10)), N'img/Nat_9.jpg', NULL, N'classic', 0, 0, 0, NULL, N'geo', N'http://localhost:8080/ganna.mp3', N'Wadi Digla rises in the mountains of the Eastern Desert and runs northwest to the Nile Valley just south of Cairo at Maadi. It runs through limestone terrain cutting a deep winding canyon; in parts, floodwaters have carved the rock into spectacular shapes. There are numerous fossils in the rock formations and scattered patches of petrified wood. After rain, ephemeral plants carpet the wadi. Dorcas Gazelle, Gazella dorcas, and Nubian Ibex, Capra nubiana, have been reported in recent years; Lesser Mouse-tailed Bats, Rhinopoma hardwickii, live inthe caves in the wadi sides.', 1)
/****** Object:  Table [dbo].[UploadedPhotos]    Script Date: 07/27/2012 19:49:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UploadedPhotos](
	[TouristID] [int] NOT NULL,
	[hotspotID] [varchar](255) NOT NULL,
	[ImageUrl] [varchar](255) NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_UploadedPhotos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[UploadedPhotos] ON
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (8, N'isl_15', N'/UploadedImages/ranaisl_15/UploadedImages/ranaisl_15/06_11_2012_17_05_34.jpg', 1)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (7, N'isl_15', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', 2)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (7, N'isl_14', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', 3)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (7, N'isl_13', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', 4)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (8, N'isl_15', N'/UploadedImages/ranaisl_15/06_15_2012_21_23_32.jpg', 5)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (8, N'isl_15', N'/UploadedImages/ranaisl_15/06_15_2012_21_26_23.jpg', 6)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (8, N'isl_15', N'/UploadedImages/ranaisl_15/06_15_2012_21_29_37.jpg', 7)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (8, N'isl_15', N'/UploadedImages/ranaisl_15/06_15_2012_21_31_45.jpg', 8)
INSERT [dbo].[UploadedPhotos] ([TouristID], [hotspotID], [ImageUrl], [ID]) VALUES (8, N'isl_15', N'/UploadedImages/ranaisl_15/06_23_2012_15_46_25.jpg', 9)
SET IDENTITY_INSERT [dbo].[UploadedPhotos] OFF
/****** Object:  Table [dbo].[Monuments_Videos]    Script Date: 07/27/2012 19:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monuments_Videos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Monuments_Videos](
	[hostSpotID] [varchar](255) NOT NULL,
	[video] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[VideoLength] [nvarchar](10) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Monuments_Videos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Monuments_Videos] ON
INSERT [dbo].[Monuments_Videos] ([hostSpotID], [video], [Description], [VideoLength], [ID]) VALUES (N'geo_3', N'http://abdelrady.110mb.com/Utour/Karnak_and_Luxor_temple_of_ancient_egypt.flv', NULL, NULL, 1)
INSERT [dbo].[Monuments_Videos] ([hostSpotID], [video], [Description], [VideoLength], [ID]) VALUES (N'geo_3', N'http://fcis.somee.com/videos/Karnak_and_Luxor_temple_of_ancient_egypt.flv', NULL, NULL, 2)
INSERT [dbo].[Monuments_Videos] ([hostSpotID], [video], [Description], [VideoLength], [ID]) VALUES (N'isl_15', N'http://abdelrady.110mb.com/Utour/Karnak_and_Luxor_temple_of_ancient_egypt.flv', N'test Description', N'12:23', 3)
SET IDENTITY_INSERT [dbo].[Monuments_Videos] OFF
/****** Object:  Table [dbo].[Monuments_Reviews]    Script Date: 07/27/2012 19:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Monuments_Reviews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tourist_ID] [int] NOT NULL,
	[hotSpotID] [varchar](255) NOT NULL,
	[Review] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Monuments_Reviews_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Monuments_Reviews] ON
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (1, 4, N'geo_3', N'wooooooooooooow')
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (2, 4, N'geo_3', N'fantastic')
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (3, 4, N'geo_3', N'ya a7med daah test')
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (4, 4, N'geo_3', N'ya a7med daah test')
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (5, 4, N'isl_15', N'test review 1')
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (7, 5, N'isl_15', N'user 2 review')
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (9, 6, N'isl_15', N'test reivew 3 from user 6')
INSERT [dbo].[Monuments_Reviews] ([ID], [Tourist_ID], [hotSpotID], [Review]) VALUES (10, 4, N'geo_3', N'ya a7med daah test')
SET IDENTITY_INSERT [dbo].[Monuments_Reviews] OFF
/****** Object:  Table [dbo].[Monuments_Photos]    Script Date: 07/27/2012 19:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monuments_Photos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Monuments_Photos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[hostSpotID] [varchar](255) NOT NULL,
	[ImageUrl] [varchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_Monuments_Photos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Monuments_Photos] ON
INSERT [dbo].[Monuments_Photos] ([ID], [hostSpotID], [ImageUrl], [Description]) VALUES (1, N'geo_3', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', NULL)
INSERT [dbo].[Monuments_Photos] ([ID], [hostSpotID], [ImageUrl], [Description]) VALUES (2, N'geo_3', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', N'simple description')
INSERT [dbo].[Monuments_Photos] ([ID], [hostSpotID], [ImageUrl], [Description]) VALUES (3, N'geo_3', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', N'simple description')
INSERT [dbo].[Monuments_Photos] ([ID], [hostSpotID], [ImageUrl], [Description]) VALUES (4, N'geo_3', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', N'asd')
INSERT [dbo].[Monuments_Photos] ([ID], [hostSpotID], [ImageUrl], [Description]) VALUES (5, N'isl_15', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', N'desc')
INSERT [dbo].[Monuments_Photos] ([ID], [hostSpotID], [ImageUrl], [Description]) VALUES (6, N'isl_15', N'http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG', N'desc2')
SET IDENTITY_INSERT [dbo].[Monuments_Photos] OFF
/****** Object:  Table [dbo].[Monument_Ratings]    Script Date: 07/27/2012 19:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Monument_Ratings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tourist_ID] [int] NOT NULL,
	[hotSpotID] [varchar](255) NOT NULL,
	[Rate] [float] NULL,
 CONSTRAINT [PK_Monument_Ratings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Monument_Ratings] ON
INSERT [dbo].[Monument_Ratings] ([ID], [Tourist_ID], [hotSpotID], [Rate]) VALUES (1, 4, N'isl_15', 4)
INSERT [dbo].[Monument_Ratings] ([ID], [Tourist_ID], [hotSpotID], [Rate]) VALUES (2, 4, N'isl_15', 3)
INSERT [dbo].[Monument_Ratings] ([ID], [Tourist_ID], [hotSpotID], [Rate]) VALUES (3, 5, N'isl_15', 3)
INSERT [dbo].[Monument_Ratings] ([ID], [Tourist_ID], [hotSpotID], [Rate]) VALUES (4, 7, N'isl_15', 2)
INSERT [dbo].[Monument_Ratings] ([ID], [Tourist_ID], [hotSpotID], [Rate]) VALUES (5, 8, N'isl_15', 1)
SET IDENTITY_INSERT [dbo].[Monument_Ratings] OFF
/****** Object:  StoredProcedure [dbo].[GetHotSpotsWithinDistance]    Script Date: 07/27/2012 19:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetHotSpotsWithinDistance]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetHotSpotsWithinDistance]
	-- Add the parameters for the stored procedure here
	@Lat float ,
	@Lon float,
	@Distance Float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select L.* from dbo.layerhotspots L
	where [dbo].[GetDistance](CAST(L.lat  AS float), CAST(L.lon  AS float), @Lat , @Lon)*1000 < @Distance
END
' 
END
GO
/****** Object:  ForeignKey [FK_layerhotspots_city]    Script Date: 07/27/2012 19:49:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_layerhotspots_city]') AND parent_object_id = OBJECT_ID(N'[dbo].[layerhotspots]'))
ALTER TABLE [dbo].[layerhotspots]  WITH CHECK ADD  CONSTRAINT [FK_layerhotspots_city] FOREIGN KEY([CityID])
REFERENCES [dbo].[city] ([CityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_layerhotspots_city]') AND parent_object_id = OBJECT_ID(N'[dbo].[layerhotspots]'))
ALTER TABLE [dbo].[layerhotspots] CHECK CONSTRAINT [FK_layerhotspots_city]
GO
/****** Object:  ForeignKey [FK_UploadedPhotos_layerhotspots]    Script Date: 07/27/2012 19:49:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos]  WITH CHECK ADD  CONSTRAINT [FK_UploadedPhotos_layerhotspots] FOREIGN KEY([hotspotID])
REFERENCES [dbo].[layerhotspots] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos] CHECK CONSTRAINT [FK_UploadedPhotos_layerhotspots]
GO
/****** Object:  ForeignKey [FK_UploadedPhotos_Tourists]    Script Date: 07/27/2012 19:49:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos]  WITH CHECK ADD  CONSTRAINT [FK_UploadedPhotos_Tourists] FOREIGN KEY([TouristID])
REFERENCES [dbo].[Tourists] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UploadedPhotos_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[UploadedPhotos]'))
ALTER TABLE [dbo].[UploadedPhotos] CHECK CONSTRAINT [FK_UploadedPhotos_Tourists]
GO
/****** Object:  ForeignKey [FK_Monuments_Videos_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Videos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Videos]'))
ALTER TABLE [dbo].[Monuments_Videos]  WITH CHECK ADD  CONSTRAINT [FK_Monuments_Videos_layerhotspots] FOREIGN KEY([hostSpotID])
REFERENCES [dbo].[layerhotspots] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Videos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Videos]'))
ALTER TABLE [dbo].[Monuments_Videos] CHECK CONSTRAINT [FK_Monuments_Videos_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monuments_Reviews_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Monuments_Reviews_layerhotspots] FOREIGN KEY([hotSpotID])
REFERENCES [dbo].[layerhotspots] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews] CHECK CONSTRAINT [FK_Monuments_Reviews_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monuments_Reviews_Tourists]    Script Date: 07/27/2012 19:49:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Monuments_Reviews_Tourists] FOREIGN KEY([Tourist_ID])
REFERENCES [dbo].[Tourists] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Reviews]'))
ALTER TABLE [dbo].[Monuments_Reviews] CHECK CONSTRAINT [FK_Monuments_Reviews_Tourists]
GO
/****** Object:  ForeignKey [FK_Monuments_Photos_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Photos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Photos]'))
ALTER TABLE [dbo].[Monuments_Photos]  WITH CHECK ADD  CONSTRAINT [FK_Monuments_Photos_layerhotspots] FOREIGN KEY([hostSpotID])
REFERENCES [dbo].[layerhotspots] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monuments_Photos_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monuments_Photos]'))
ALTER TABLE [dbo].[Monuments_Photos] CHECK CONSTRAINT [FK_Monuments_Photos_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monument_Ratings_layerhotspots]    Script Date: 07/27/2012 19:49:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Monument_Ratings_layerhotspots] FOREIGN KEY([hotSpotID])
REFERENCES [dbo].[layerhotspots] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_layerhotspots]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings] CHECK CONSTRAINT [FK_Monument_Ratings_layerhotspots]
GO
/****** Object:  ForeignKey [FK_Monument_Ratings_Tourists]    Script Date: 07/27/2012 19:49:43 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Monument_Ratings_Tourists] FOREIGN KEY([Tourist_ID])
REFERENCES [dbo].[Tourists] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Monument_Ratings_Tourists]') AND parent_object_id = OBJECT_ID(N'[dbo].[Monument_Ratings]'))
ALTER TABLE [dbo].[Monument_Ratings] CHECK CONSTRAINT [FK_Monument_Ratings_Tourists]
GO
