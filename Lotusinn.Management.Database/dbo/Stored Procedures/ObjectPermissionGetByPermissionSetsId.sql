CREATE PROCEDURE [dbo].[ObjectPermissionGetByPermissionSetsId]
	@permissionSetsId nvarchar(15)
AS
	SELECT * FROM ObjectPermission 
	WHERE PermissionSetsId = @permissionSetsId
