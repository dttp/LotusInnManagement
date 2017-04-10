CREATE PROCEDURE [dbo].[UserGetAll]	
AS
BEGIN
	SELECT u.*, h.Name as HouseName, h.Address as HouseAddress, r.Name as RoleName
	FROM dbo.[User] u LEFT JOIN [dbo].[House] h ON u.HouseId = h.Id
		INNER JOIN [dbo].[Role] r ON u.RoleId = r.Id
END