/*
 Шаблон скрипта после развертывания							
--------------------------------------------------------------------------------------
 В данном файле содержатся инструкции SQL, которые будут исполнены перед скриптом построения.	
 Используйте синтаксис SQLCMD для включения файла в скрипт, выполняемый перед развертыванием.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте перед развертыванием.		
 Пример:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessEntityInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BusinessEntityInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessEntityInsertUDT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BusinessEntityInsertUDT]
GO

IF exists (select 1
   FROM sys.table_types
   where name = 'BusinessEntitiesUDT')
DROP TYPE [dbo].[BusinessEntitiesUDT]
GO

IF EXISTS (SELECT 1
   FROM sys.sysreferences r JOIN sys.sysobjects o ON (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = OBJECT_ID('BusinessEntities') AND o.name = 'FK_BusinessEntities_BusinessEntitiesNext')
		ALTER TABLE [dbo].[BusinessEntities] DROP CONSTRAINT [FK_BusinessEntities_BusinessEntitiesNext]
GO

IF exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BusinessEntities') and o.name = 'FK_BusinessEntities_BusinessEntitiesPrevious')
		ALTER TABLE [dbo].[BusinessEntities] DROP CONSTRAINT [FK_BusinessEntities_BusinessEntitiesPrevious]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessEntities]') AND type in (N'U'))
	DROP TABLE [dbo].[BusinessEntities]
GO