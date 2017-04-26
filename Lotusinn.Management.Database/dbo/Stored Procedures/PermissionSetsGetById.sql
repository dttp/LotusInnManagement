CREATE PROCEDURE [dbo].[PermissionSetsGetById]
	@id nvarchar(15)
AS
	SELECT * FROM PermissionSets WHERE Id = @id
