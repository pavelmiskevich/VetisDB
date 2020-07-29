CREATE TYPE [dbo].[BusinessEntitiesUDT] AS TABLE
(
	[uuid] UNIQUEIDENTIFIER, 
    [guid] UNIQUEIDENTIFIER, 
    [active] BIT, 
    [last] BIT, 
    [status] NVARCHAR(50), 
    [createDate] DATETIME, 
    [updateDate] DATETIME, 
    [previous] UNIQUEIDENTIFIER NULL, 
    [next] UNIQUEIDENTIFIER NULL, 
    [type] NVARCHAR(50), 
    [name] NVARCHAR(255) NULL
)
