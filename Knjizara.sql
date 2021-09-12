CREATE DATABASE [SalonLepote]
GO
USE [SalonLepote]
GO
CREATE TABLE [dbo].[Kategorija] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Naziv] VARCHAR (200)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Usluga] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Naziv]      VARCHAR (200)    NOT NULL,
    [Cena]       FLOAT (53)       NOT NULL,
    [Kategorija] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Usluga_ToKategorija] FOREIGN KEY ([Kategorija]) REFERENCES [dbo].[Kategorija] ([Id])
);
GO