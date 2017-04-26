CREATE PROCEDURE [dbo].[PermissionSetsInsert]
	@id nvarchar(15),
	@name nvarchar(50)
AS
	INSERT INTO PermissionSets (Id, Name)
	VALUES(@id, @name)
