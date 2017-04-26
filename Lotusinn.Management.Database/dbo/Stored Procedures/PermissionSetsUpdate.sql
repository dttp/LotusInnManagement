CREATE PROCEDURE [dbo].[PermissionSetsUpdate]
	@id nvarchar(15),
	@name nvarchar(50)
AS
	UPDATE PermissionSets 
	SET Name = @name
	WHERE Id = @id;