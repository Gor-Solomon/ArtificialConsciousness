CREATE TABLE [connection].[ConnectionTypeAttributes]
(
	[ConnectionTypeId]      INT NOT NULL,
    [AttributeID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([AttributeID], [ConnectionTypeId]),
    CONSTRAINT [FK_ConnectionTypeAttributes_ToAttribute] FOREIGN KEY ([AttributeID]) REFERENCES [attributes].[AttributeDescription] ([Id]),
    CONSTRAINT [FK_ConnectionTypeAttributes_ToConnectionType] FOREIGN KEY ([ConnectionTypeId]) REFERENCES [connection].[ConnectionType] ([Id])
)
