CREATE TABLE [connection].[Connection] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [NodeAId]               INT             NOT NULL,
    [NodeBId]               INT             NOT NULL,
    [A2BConnectionTypeId]   INT             NOT NULL,
    [B2AConnectionTypeId]   INT             NOT NULL,
    [WeightA2B]                DECIMAL (19, 5) DEFAULT ((0)) NOT NULL,
	[WeightB2A]                DECIMAL (19, 5) DEFAULT ((0)) NOT NULL,
    [OverwritesId] INT NULL, 
    [GraphId] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Connection_A2BConnectionType] FOREIGN KEY ([A2BConnectionTypeId]) REFERENCES [connection].[ConnectionType] ([Id]),
    CONSTRAINT [FK_Connection_B2AConnectionType] FOREIGN KEY ([B2AConnectionTypeId]) REFERENCES [connection].[ConnectionType] ([Id]),
    CONSTRAINT [FK_Connection_ToNodeA] FOREIGN KEY ([NodeAId]) REFERENCES [node].[Node] ([Id]),
    CONSTRAINT [FK_Connection_ToNodeB] FOREIGN KEY ([NodeBId]) REFERENCES [node].[Node] ([Id]), 
    CONSTRAINT [FK_Connection_ToConncection] FOREIGN KEY ([OverwritesId]) REFERENCES [connection].[Connection]([Id]), 
    CONSTRAINT [FK_Connection_ToGraph] FOREIGN KEY ([GraphId]) REFERENCES [graph].[Graph]([Id])
);

