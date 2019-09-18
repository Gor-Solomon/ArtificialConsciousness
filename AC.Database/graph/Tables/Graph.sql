CREATE TABLE [graph].[Graph]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [Name] NVARCHAR(50) NULL, 
    [EnteryNodeId] INT, 
    [Description] NVARCHAR(150) NULL, 
    [IsAbstract] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Graph_ToNode] FOREIGN KEY ([EnteryNodeId]) REFERENCES [node].[Node]([Id])
)

