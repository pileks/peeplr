
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
-- Inserting data
-- --------------------------------------------------

SET IDENTITY_INSERT [dbo].[Contacts] ON 

GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [StreetAddress], [City], [Company]) VALUES (1, N'Jure', N'Graniæ Skender', N'Podluka 13a', N'Baška Voda', N'Assemblio')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [StreetAddress], [City], [Company]) VALUES (2, N'Antonio', N'Pavlinoviæ', N'Neka u Kaštelima 12', N'Kaštel Gomilica', N'HR Cloud')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [StreetAddress], [City], [Company]) VALUES (3, N'Marko', N'Marinoviæ', N'Neka koja poèinje s K ili V 14', N'Split', N'AuThink')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [StreetAddress], [City], [Company]) VALUES (4, N'Ivan', N'Maèek', N'Tamo iznad Geniusa 22', N'Split', N'HR Cloud')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [StreetAddress], [City], [Company]) VALUES (5, N'Mirko', N'Mirkoviæ', N'Mirkoviæeva 1', N'Mirkoviæi', N'Mirko d.o.o.')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [StreetAddress], [City], [Company]) VALUES (6, N'Mirkovo', N'Kumèe', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (1, 1)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (4, 1)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (1, 2)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (2, 2)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (1, 3)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (2, 3)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (1, 4)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (2, 5)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (5, 6)
GO
INSERT [dbo].[ContactTag] ([Contacts_Id], [Tags_Id]) VALUES (6, 7)
GO
SET IDENTITY_INSERT [dbo].[Emails] ON 

GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (1, 1, N'jure@dump.hr')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (2, 1, N'jure@assemblio.hr')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (3, 1, N'jgranics@fesb.hr')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (4, 2, N'antonio@dump.hr')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (5, 2, N'antonio@assemblio.hr')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (6, 3, N'markom@dump.hr')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (7, 4, N'ivan@dump.hr')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (8, 5, N'mirko.mirkovic@gmail.com')
GO
INSERT [dbo].[Emails] ([Id], [ContactId], [EmailString]) VALUES (9, 6, N'mirkovo.kumce@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Emails] OFF
GO
SET IDENTITY_INSERT [dbo].[Numbers] ON 

GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (1, N'mobile', N'+385 91 1614 291', 1)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (2, N'home', N'021 620 438', 1)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (3, N'mobile', N'+385 95 9136 380', 2)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (4, N'mobile', N'+385915149878', 3)
GO
INSERT [dbo].[Numbers] ([Id], [Type], [NumberString], [ContactId]) VALUES (5, N'mobile', N'091 2345 678', 5)
GO
SET IDENTITY_INSERT [dbo].[Numbers] OFF
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (1, N'developer')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (2, N'sports')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (3, N'business')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (4, N'gaming')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (5, N'designer')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (6, N'balkanski biznismen')
GO
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (7, N'fizikala')
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