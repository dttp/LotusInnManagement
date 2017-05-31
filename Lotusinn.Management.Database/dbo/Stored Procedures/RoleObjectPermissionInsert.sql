CREATE PROCEDURE [dbo].[RoleObjectPermissionInsert]
	@id nvarchar(15),
	@roleId nvarchar(15),
	@object nvarchar(50),
	@permission int
AS
	INSERT INTO RoleObjectPermission(Id, RoleId, Object, Permission)
	VALUES (@id, @roleId, @object, @permission)
