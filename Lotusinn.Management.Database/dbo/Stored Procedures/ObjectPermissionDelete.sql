CREATE PROCEDURE [dbo].[ObjectPermissionDelete]
	@permissionSetsId nvarchar(15)
AS
	DELETE FROM ObjectPermission WHERE PermissionSetsId = @permissionSetsId 
