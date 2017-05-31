CREATE PROCEDURE [dbo].[UserObjectPermissionDelete]
	@id nvarchar(15)
AS
	DELETE FROM UserObjectPermission
	WHERE Id = @id
