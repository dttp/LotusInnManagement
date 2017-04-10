CREATE PROCEDURE [dbo].[RoomGetAll]	
	@houseId nvarchar(15)
AS
BEGIN
	SELECT r.*, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit
	FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
	WHERE r.HouseId = @houseId
	ORDER BY r.RoomNumber ASC
END