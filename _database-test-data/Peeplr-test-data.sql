
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/08/2014 01:30:20
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
    [Email] nvarchar(max)  NOT NULL,
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

-- Creating table 'ContactTag'
CREATE TABLE [dbo].[ContactTag] (
    [Contacts_Id] int  NOT NULL,
    [Tags_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Inserting data
-- --------------------------------------------------

SET IDENTITY_INSERT [dbo].[Contacts] ON 

GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Email], [StreetAddress], [City], [Company]) VALUES (1, N'Jure', N'Graniæ Skender', N'jure@dump.hr', N'Podluka 13a', N'Baška Voda', N'DUMP')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Email], [StreetAddress], [City], [Company]) VALUES (2, N'Antonio', N'Pavlinoviæ', N'antonio@dump.hr', N'Neka ulica 10', N'Kaštel Gomilica', N'HR Cloud')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Email], [StreetAddress], [City], [Company]) VALUES (3, N'Marko', N'Marinoviæ', N'markom@dump.hr', N'Kalajžiæeva 14', N'Split', N'HR Cloud')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Email], [StreetAddress], [City], [Company]) VALUES (4, N'Ivan', N'Maèek', N'ivan@dump.hr', N'Ne da mi se pisati adrese 13', N'Split', N'HR Cloud')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Email], [StreetAddress], [City], [Company]) VALUES (5, N'Mirko', N'Mirkiæ', N'mirko.mirkic@gmail.com', N'Mirkiæeva 1', N'Mirkiæi', N'Mirko d.o.o.')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Email], [StreetAddress], [City], [Company]) VALUES (6, N'Mirkov', N'Brat', N'mirkov.brat@gmail.com', N'Where', N'do we', N'go now?')
GO
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (1, 1)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (2, 1)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (3, 1)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (4, 1)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (1, 2)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (1, 3)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (2, 3)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (2, 4)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (5, 5)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (5, 6)
GO
SET IDENTITY_INSERT [dbo].[Numbers] ON 

GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (1, N'mobile', N'+385 91 161 4291', 1)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (2, N'home', N'021 620 438', 1)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (3, N'work', N'021 123 456', 1)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (4, N'mobile', N'+385 95 9136 380', 2)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (6, N'mobile', N'021 647 566', 5)
GO
SET IDENTITY_INSERT [dbo].[Numbers] OFF
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (1, N'developer')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (2, N'business')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (3, N'sports')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (4, N'designer')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (5, N'balkan')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (6, N'biznismen')
GO
SET IDENTITY_INSERT [dbo].[Tags] OFF
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------