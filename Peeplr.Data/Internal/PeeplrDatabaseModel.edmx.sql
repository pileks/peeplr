
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/10/2014 19:58:57
-- Generated from EDMX file: D:\Dev\peeplr\Peeplr.Data\Internal\PeeplrDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Peeplr];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ContactNumbers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Numbers] DROP CONSTRAINT [FK_ContactNumbers];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactTag_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactTag] DROP CONSTRAINT [FK_ContactTag_Contact];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactTag_Tag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactTag] DROP CONSTRAINT [FK_ContactTag_Tag];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Numbers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Numbers];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[ContactTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactTag];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [StreetAddress] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [Company] nvarchar(max)  NULL
);
GO

-- Creating table 'Numbers'
CREATE TABLE [dbo].[Numbers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [NumberString] nvarchar(max)  NOT NULL,
    [ContactId] int  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Emails'
CREATE TABLE [dbo].[Emails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ContactId] int  NOT NULL,
    [EmailString] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ContactTag'
CREATE TABLE [dbo].[ContactTag] (
    [Contacts_Id] int  NOT NULL,
    [Tags_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Numbers'
ALTER TABLE [dbo].[Numbers]
ADD CONSTRAINT [PK_Numbers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [PK_Emails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Contacts_Id], [Tags_Id] in table 'ContactTag'
ALTER TABLE [dbo].[ContactTag]
ADD CONSTRAINT [PK_ContactTag]
    PRIMARY KEY CLUSTERED ([Contacts_Id], [Tags_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ContactId] in table 'Numbers'
ALTER TABLE [dbo].[Numbers]
ADD CONSTRAINT [FK_ContactNumbers]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactNumbers'
CREATE INDEX [IX_FK_ContactNumbers]
ON [dbo].[Numbers]
    ([ContactId]);
GO

-- Creating foreign key on [Contacts_Id] in table 'ContactTag'
ALTER TABLE [dbo].[ContactTag]
ADD CONSTRAINT [FK_ContactTag_Contact]
    FOREIGN KEY ([Contacts_Id])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tags_Id] in table 'ContactTag'
ALTER TABLE [dbo].[ContactTag]
ADD CONSTRAINT [FK_ContactTag_Tag]
    FOREIGN KEY ([Tags_Id])
    REFERENCES [dbo].[Tags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactTag_Tag'
CREATE INDEX [IX_FK_ContactTag_Tag]
ON [dbo].[ContactTag]
    ([Tags_Id]);
GO

-- Creating foreign key on [ContactId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [FK_ContactEmail]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactEmail'
CREATE INDEX [IX_FK_ContactEmail]
ON [dbo].[Emails]
    ([ContactId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------