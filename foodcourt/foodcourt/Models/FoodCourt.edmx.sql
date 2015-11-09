
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/09/2015 15:47:48
-- Generated from EDMX file: C:\Users\elgut\Documents\GitHubVisualStudio\foodcourt\foodcourt\foodcourt\Models\FoodCourt.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [foodcourt];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Order_Dish]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Dish];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Dish]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dish];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Dish'
CREATE TABLE [dbo].[Dish] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Description] varchar(150)  NULL,
    [Price] float  NULL
);
GO

-- Creating table 'Order'
CREATE TABLE [dbo].[Order] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NULL,
    [DishId] int  NOT NULL,
    [State] int  NULL,
    [Date] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Dish'
ALTER TABLE [dbo].[Dish]
ADD CONSTRAINT [PK_Dish]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [PK_Order]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DishId] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_Order_Dish]
    FOREIGN KEY ([DishId])
    REFERENCES [dbo].[Dish]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Dish'
CREATE INDEX [IX_FK_Order_Dish]
ON [dbo].[Order]
    ([DishId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------