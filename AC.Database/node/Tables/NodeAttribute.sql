CREATE TABLE [node].[NodeAttribute] (
    [NodeID]      INT NOT NULL,
    [AttributeID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([AttributeID], [NodeID]),
    CONSTRAINT [FK_NodeAttribute_ToAttribute] FOREIGN KEY ([AttributeID]) REFERENCES [attributes].[AttributeDescription] ([Id]),
    CONSTRAINT [FK_NodeID_ToNode] FOREIGN KEY ([NodeID]) REFERENCES [node].[Node] ([Id])
);

