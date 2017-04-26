CREATE PROCEDURE [dbo].[ObjectPermissionUpdate]
	@permissionSetsId nvarchar(15),
	@objectName nvarchar(50),
	@permission nvarchar(max)
AS
	UPDATE ObjectPermission 
	SET Permissions = @permission
	WHERE PermissionSetsId = @permissionSetsId AND ObjectName = @objectName
