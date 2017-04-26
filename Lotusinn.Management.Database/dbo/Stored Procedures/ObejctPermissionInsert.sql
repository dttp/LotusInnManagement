CREATE PROCEDURE [dbo].[ObejctPermissionInsert]
	@permissionSetsId nvarchar(15),
	@objectName nvarchar(50),
	@permission nvarchar(max)
AS
	INSERT INTO ObjectPermission(PermissionSetsId, ObjectName, Permissions)
	VALUES (@permissionSetsId, @objectName, @permission)
