CREATE PROCEDURE [dbo].[BusinessEntityInsert]
	 @uuid as UNIQUEIDENTIFIER
    ,@guid AS UNIQUEIDENTIFIER
    ,@active AS BIT
    ,@last AS BIT
    ,@status AS NVARCHAR(50)
    ,@createDate AS DATETIME
    ,@updateDate AS DATETIME
    ,@previous AS UNIQUEIDENTIFIER = NULL
    ,@next AS UNIQUEIDENTIFIER = NULL
    ,@type AS NVARCHAR(50)
    ,@name AS NVARCHAR(255) = NULL
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
     VALUES
           (@uuid
           ,@guid
           ,@active
           ,@last
           ,@status
           ,@createDate
           ,@updateDate
           ,@previous
           ,@next
           ,@type
           ,@name)
	SELECT @@IDENTITY
--	SET NOCOUNT OFF;
END
--RETURN 0
