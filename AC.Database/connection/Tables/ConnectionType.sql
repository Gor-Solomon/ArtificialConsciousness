CREATE TABLE [connection].[ConnectionType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
	[ConnectionRule] nvarchar (2000)
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

