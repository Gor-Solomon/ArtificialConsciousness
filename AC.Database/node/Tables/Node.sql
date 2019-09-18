CREATE TABLE [node].[Node] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) NOT NULL,
	[GraphId] INT NOT null,
    [Data]    VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Node_ToGraph] FOREIGN KEY ([GraphId]) REFERENCES [graph].[Graph]([Id])
);

