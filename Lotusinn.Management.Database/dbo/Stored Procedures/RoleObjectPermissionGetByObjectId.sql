CREATE PROCEDURE [dbo].[RoleObjectPermissionGetByObjectId]
	@objectId nvarchar(15)
AS
	SELECT * FROM RoleObjectPermission 
	WHERE ObjectId = @objectId
