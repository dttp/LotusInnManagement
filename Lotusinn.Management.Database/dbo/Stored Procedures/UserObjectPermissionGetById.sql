CREATE PROCEDURE [dbo].[UserObjectPermissionGetById]
	@id nvarchar(15)
AS
	SELECT * FROM UserObjectPermission 
	WHERE Id = @id
