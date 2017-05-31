CREATE PROCEDURE [dbo].[RoleObjectPermissionGetByRoleId]
	@roleId nvarchar(15)
AS
	SELECT * FROM RoleObjectPermission WHERE RoleId = @roleId
