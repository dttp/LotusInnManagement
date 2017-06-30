CREATE PROCEDURE [dbo].[UserObjectPermissionGetByObjectId]
	@objectId nvarchar(15)
AS
	SELECT * FROM UserObjectPermission 
	WHERE ObjectId = @objectId
