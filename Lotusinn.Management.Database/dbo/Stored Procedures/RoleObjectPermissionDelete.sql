CREATE PROCEDURE [dbo].[RoleObjectPermissionDelete]
	@id nvarchar(15)
AS
	DELETE FROM RoleObjectPermission WHERE Id = @id
