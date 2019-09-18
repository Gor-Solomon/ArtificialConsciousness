CREATE TABLE [attributes].[AttributeDescription] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NULL unique,
    [Description] NVARCHAR(150) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

