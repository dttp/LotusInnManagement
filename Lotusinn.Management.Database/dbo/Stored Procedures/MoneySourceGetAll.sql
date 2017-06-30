CREATE PROCEDURE [dbo].[MoneySourceGetAll]	
	@userId nvarchar(15)
AS	
	DECLARE @roleId nvarchar(15);
	SET @roleId = (SELECT RoleId FROM [User] WHERE Id = @userId)

	SELECT ms.* 
	FROM MoneySource ms	LEFT JOIN UserObjectPermission uop ON ms.Id = uop.ObjectId LEFT JOIN RoleObjectPermission rop ON ms.Id = rop.ObjectId
	WHERE (uop.UserId = @userId AND (uop.Permission & 1) = 1) OR (rop.RoleId = @roleId AND (rop.Permission & 1) = 1)