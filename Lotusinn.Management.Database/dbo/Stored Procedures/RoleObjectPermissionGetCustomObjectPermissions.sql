CREATE PROCEDURE [dbo].[RoleObjectPermissionGetCustomObjectPermissions]
	@roleId nvarchar(15),
	@objectType nvarchar(50)
AS
	SELECT * FROM RoleObjectPermission
	WHERE RoleId = @roleId AND ObjectType = @objectType AND ObjectId IS NOT NULL
