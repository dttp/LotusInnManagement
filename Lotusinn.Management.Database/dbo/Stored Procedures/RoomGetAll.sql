CREATE PROCEDURE [dbo].[RoomGetAll]	
	@houseId nvarchar(15)
AS
BEGIN
	SELECT r.*
	FROM Room r
	WHERE r.HouseId = @houseId
	ORDER BY r.RoomNumber ASC
END