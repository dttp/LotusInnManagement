CREATE PROCEDURE [dbo].[RoleObjectPermissionUpdate]
	@id nvarchar(15),
	@roleId nvarchar(15),
	@object nvarchar(50),
	@permission int
AS
	UPDATE RoleObjectPermission
	SET RoleId = @roleId, 
		Object = @object,
		Permission = @permission
	WHERE Id = @id