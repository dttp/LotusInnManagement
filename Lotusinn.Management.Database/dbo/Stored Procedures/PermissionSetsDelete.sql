CREATE PROCEDURE [dbo].[PermissionSetsDelete]
	@id nvarchar(15)
AS
	DELETE FROM PermissionSets WHERE Id = @id
