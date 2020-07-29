CREATE PROCEDURE [dbo].[BusinessEntityInsertUDT]
	@table BusinessEntitiesUDT READONLY
AS
BEGIN
--	SET NOCOUNT ON;
	INSERT INTO [dbo].[BusinessEntities]
           ([uuid]
           ,[guid]
           ,[active]
           ,[last]
           ,[status]
           ,[createDate]
           ,[updateDate]
           ,[previous]
           ,[next]
           ,[type]
           ,[name])
     SELECT [uuid]
           ,[guid]
           ,[active]
           ,[last]
           ,[status]
           ,[createDate]
           ,[updateDate]
           ,[previous]
           ,[next]
           ,[type]
           ,[name] FROM @table;
	--SELECT @@IDENTITY
	-- сделать возврат количества добавленных записей
--	SET NOCOUNT OFF;
END
--RETURN @@ROWCOUNT
select @@ROWCOUNT
