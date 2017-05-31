CREATE PROCEDURE [dbo].[RoleObjectPermissionGetById]
	@id nvarchar(15)
AS
	SELECT * FROM RoleObjectPermission
	WHERE Id = @id
