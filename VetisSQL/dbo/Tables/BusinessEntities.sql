CREATE TABLE [dbo].[BusinessEntities]
(
	[uuid] UNIQUEIDENTIFIER NOT NULL , 
    [guid] UNIQUEIDENTIFIER NOT NULL, 
    [active] BIT NOT NULL, 
    [last] BIT NOT NULL, 
    [status] NVARCHAR(50) NOT NULL, 
    [createDate] DATETIME NOT NULL, 
    [updateDate] DATETIME NOT NULL, 
    [previous] UNIQUEIDENTIFIER NULL, 
    [next] UNIQUEIDENTIFIER NULL, 
    [type] NVARCHAR(50) NOT NULL, 
    [name] NVARCHAR(255) NULL, 
    PRIMARY KEY ([uuid])
	--, 
 --   CONSTRAINT [FK_BusinessEntities_BusinessEntitiesNext] FOREIGN KEY ([next]) REFERENCES [BusinessEntities]([uuid]), 
 --   CONSTRAINT [FK_BusinessEntities_BusinessEntitiesPrevious] FOREIGN KEY ([previous]) REFERENCES [BusinessEntities]([uuid])
)
