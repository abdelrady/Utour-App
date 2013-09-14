
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/14/2013 15:14:59
-- Generated from EDMX file: C:\Users\aelagamx\Desktop\TryError\Utour\SourceControlSolution\Ahmed_Last_Solution\ITI_NLayered\Infrastructure.Data.Utour\Model\UTourModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [UTOURDB3];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_layerhotspots_city]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[layerhotspots] DROP CONSTRAINT [FK_layerhotspots_city];
GO
IF OBJECT_ID(N'[dbo].[FK_Monument_Ratings_layerhotspots]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Monument_Ratings] DROP CONSTRAINT [FK_Monument_Ratings_layerhotspots];
GO
IF OBJECT_ID(N'[dbo].[FK_Monument_Ratings_Tourists]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Monument_Ratings] DROP CONSTRAINT [FK_Monument_Ratings_Tourists];
GO
IF OBJECT_ID(N'[dbo].[FK_Monuments_Photos_layerhotspots]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Monuments_Photos] DROP CONSTRAINT [FK_Monuments_Photos_layerhotspots];
GO
IF OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_layerhotspots]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Monuments_Reviews] DROP CONSTRAINT [FK_Monuments_Reviews_layerhotspots];
GO
IF OBJECT_ID(N'[dbo].[FK_Monuments_Reviews_Tourists]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Monuments_Reviews] DROP CONSTRAINT [FK_Monuments_Reviews_Tourists];
GO
IF OBJECT_ID(N'[dbo].[FK_Monuments_Videos_layerhotspots]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Monuments_Videos] DROP CONSTRAINT [FK_Monuments_Videos_layerhotspots];
GO
IF OBJECT_ID(N'[dbo].[FK_UploadedPhotos_layerhotspots]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UploadedPhotos] DROP CONSTRAINT [FK_UploadedPhotos_layerhotspots];
GO
IF OBJECT_ID(N'[dbo].[FK_UploadedPhotos_Tourists]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UploadedPhotos] DROP CONSTRAINT [FK_UploadedPhotos_Tourists];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin_Users];
GO
IF OBJECT_ID(N'[dbo].[city]', 'U') IS NOT NULL
    DROP TABLE [dbo].[city];
GO
IF OBJECT_ID(N'[dbo].[layerhotspots]', 'U') IS NOT NULL
    DROP TABLE [dbo].[layerhotspots];
GO
IF OBJECT_ID(N'[dbo].[Monument_Ratings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Monument_Ratings];
GO
IF OBJECT_ID(N'[dbo].[Monuments_Photos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Monuments_Photos];
GO
IF OBJECT_ID(N'[dbo].[Monuments_Reviews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Monuments_Reviews];
GO
IF OBJECT_ID(N'[dbo].[Monuments_Videos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Monuments_Videos];
GO
IF OBJECT_ID(N'[dbo].[Tourists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tourists];
GO
IF OBJECT_ID(N'[dbo].[UploadedPhotos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UploadedPhotos];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admin_Users'
CREATE TABLE [dbo].[Admin_Users] (
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [ID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'cities'
CREATE TABLE [dbo].[cities] (
    [CityName] nvarchar(50)  NULL,
    [CityID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'layerhotspots'
CREATE TABLE [dbo].[layerhotspots] (
    [Id] varchar(255)  NOT NULL,
    [footnote] nvarchar(150)  NULL,
    [title] nvarchar(150)  NULL,
    [lat] decimal(13,10)  NULL,
    [lon] decimal(13,10)  NULL,
    [imageurl] varchar(255)  NULL,
    [description] nvarchar(255)  NULL,
    [biwStyle] varchar(50)  NULL,
    [alt] int  NULL,
    [doNotIndex] tinyint  NULL,
    [showSmallBiw] tinyint  NULL,
    [showBiwOnClick] tinyint  NULL,
    [poiType] varchar(50)  NULL,
    [Monument_Audio] nvarchar(255)  NULL,
    [Long_Description] nvarchar(max)  NULL,
    [CityID] int  NULL
);
GO

-- Creating table 'Monument_Ratings'
CREATE TABLE [dbo].[Monument_Ratings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Tourist_ID] int  NOT NULL,
    [hotSpotID] varchar(255)  NOT NULL,
    [Rate] float  NULL
);
GO

-- Creating table 'Monuments_Photos'
CREATE TABLE [dbo].[Monuments_Photos] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [hostSpotID] varchar(255)  NOT NULL,
    [ImageUrl] varchar(255)  NOT NULL,
    [Description] nvarchar(255)  NULL
);
GO

-- Creating table 'Monuments_Reviews'
CREATE TABLE [dbo].[Monuments_Reviews] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Tourist_ID] int  NOT NULL,
    [hotSpotID] varchar(255)  NOT NULL,
    [Review] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'Monuments_Videos'
CREATE TABLE [dbo].[Monuments_Videos] (
    [hostSpotID] varchar(255)  NOT NULL,
    [video] nvarchar(max)  NOT NULL,
    [Description] nvarchar(255)  NULL,
    [VideoLength] nvarchar(10)  NULL,
    [ID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Tourists'
CREATE TABLE [dbo].[Tourists] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [First_Name] nvarchar(50)  NOT NULL,
    [Last_Name] nvarchar(50)  NULL,
    [Gender] bit  NULL,
    [Nationality] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Preferred_Language] nchar(10)  NULL
);
GO

-- Creating table 'UploadedPhotos'
CREATE TABLE [dbo].[UploadedPhotos] (
    [TouristID] int  NOT NULL,
    [hotspotID] varchar(255)  NOT NULL,
    [ImageUrl] varchar(255)  NOT NULL,
    [ID] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Admin_Users'
ALTER TABLE [dbo].[Admin_Users]
ADD CONSTRAINT [PK_Admin_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [CityID] in table 'cities'
ALTER TABLE [dbo].[cities]
ADD CONSTRAINT [PK_cities]
    PRIMARY KEY CLUSTERED ([CityID] ASC);
GO

-- Creating primary key on [Id] in table 'layerhotspots'
ALTER TABLE [dbo].[layerhotspots]
ADD CONSTRAINT [PK_layerhotspots]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'Monument_Ratings'
ALTER TABLE [dbo].[Monument_Ratings]
ADD CONSTRAINT [PK_Monument_Ratings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Monuments_Photos'
ALTER TABLE [dbo].[Monuments_Photos]
ADD CONSTRAINT [PK_Monuments_Photos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Monuments_Reviews'
ALTER TABLE [dbo].[Monuments_Reviews]
ADD CONSTRAINT [PK_Monuments_Reviews]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Monuments_Videos'
ALTER TABLE [dbo].[Monuments_Videos]
ADD CONSTRAINT [PK_Monuments_Videos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Tourists'
ALTER TABLE [dbo].[Tourists]
ADD CONSTRAINT [PK_Tourists]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UploadedPhotos'
ALTER TABLE [dbo].[UploadedPhotos]
ADD CONSTRAINT [PK_UploadedPhotos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CityID] in table 'layerhotspots'
ALTER TABLE [dbo].[layerhotspots]
ADD CONSTRAINT [FK_layerhotspots_city]
    FOREIGN KEY ([CityID])
    REFERENCES [dbo].[cities]
        ([CityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_layerhotspots_city'
CREATE INDEX [IX_FK_layerhotspots_city]
ON [dbo].[layerhotspots]
    ([CityID]);
GO

-- Creating foreign key on [hotSpotID] in table 'Monument_Ratings'
ALTER TABLE [dbo].[Monument_Ratings]
ADD CONSTRAINT [FK_Monument_Ratings_layerhotspots]
    FOREIGN KEY ([hotSpotID])
    REFERENCES [dbo].[layerhotspots]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Monument_Ratings_layerhotspots'
CREATE INDEX [IX_FK_Monument_Ratings_layerhotspots]
ON [dbo].[Monument_Ratings]
    ([hotSpotID]);
GO

-- Creating foreign key on [hostSpotID] in table 'Monuments_Photos'
ALTER TABLE [dbo].[Monuments_Photos]
ADD CONSTRAINT [FK_Monuments_Photos_layerhotspots]
    FOREIGN KEY ([hostSpotID])
    REFERENCES [dbo].[layerhotspots]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Monuments_Photos_layerhotspots'
CREATE INDEX [IX_FK_Monuments_Photos_layerhotspots]
ON [dbo].[Monuments_Photos]
    ([hostSpotID]);
GO

-- Creating foreign key on [hotSpotID] in table 'Monuments_Reviews'
ALTER TABLE [dbo].[Monuments_Reviews]
ADD CONSTRAINT [FK_Monuments_Reviews_layerhotspots]
    FOREIGN KEY ([hotSpotID])
    REFERENCES [dbo].[layerhotspots]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Monuments_Reviews_layerhotspots'
CREATE INDEX [IX_FK_Monuments_Reviews_layerhotspots]
ON [dbo].[Monuments_Reviews]
    ([hotSpotID]);
GO

-- Creating foreign key on [hostSpotID] in table 'Monuments_Videos'
ALTER TABLE [dbo].[Monuments_Videos]
ADD CONSTRAINT [FK_Monuments_Videos_layerhotspots]
    FOREIGN KEY ([hostSpotID])
    REFERENCES [dbo].[layerhotspots]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Monuments_Videos_layerhotspots'
CREATE INDEX [IX_FK_Monuments_Videos_layerhotspots]
ON [dbo].[Monuments_Videos]
    ([hostSpotID]);
GO

-- Creating foreign key on [hotspotID] in table 'UploadedPhotos'
ALTER TABLE [dbo].[UploadedPhotos]
ADD CONSTRAINT [FK_UploadedPhotos_layerhotspots]
    FOREIGN KEY ([hotspotID])
    REFERENCES [dbo].[layerhotspots]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UploadedPhotos_layerhotspots'
CREATE INDEX [IX_FK_UploadedPhotos_layerhotspots]
ON [dbo].[UploadedPhotos]
    ([hotspotID]);
GO

-- Creating foreign key on [Tourist_ID] in table 'Monument_Ratings'
ALTER TABLE [dbo].[Monument_Ratings]
ADD CONSTRAINT [FK_Monument_Ratings_Tourists]
    FOREIGN KEY ([Tourist_ID])
    REFERENCES [dbo].[Tourists]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Monument_Ratings_Tourists'
CREATE INDEX [IX_FK_Monument_Ratings_Tourists]
ON [dbo].[Monument_Ratings]
    ([Tourist_ID]);
GO

-- Creating foreign key on [Tourist_ID] in table 'Monuments_Reviews'
ALTER TABLE [dbo].[Monuments_Reviews]
ADD CONSTRAINT [FK_Monuments_Reviews_Tourists]
    FOREIGN KEY ([Tourist_ID])
    REFERENCES [dbo].[Tourists]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Monuments_Reviews_Tourists'
CREATE INDEX [IX_FK_Monuments_Reviews_Tourists]
ON [dbo].[Monuments_Reviews]
    ([Tourist_ID]);
GO

-- Creating foreign key on [TouristID] in table 'UploadedPhotos'
ALTER TABLE [dbo].[UploadedPhotos]
ADD CONSTRAINT [FK_UploadedPhotos_Tourists]
    FOREIGN KEY ([TouristID])
    REFERENCES [dbo].[Tourists]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UploadedPhotos_Tourists'
CREATE INDEX [IX_FK_UploadedPhotos_Tourists]
ON [dbo].[UploadedPhotos]
    ([TouristID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------