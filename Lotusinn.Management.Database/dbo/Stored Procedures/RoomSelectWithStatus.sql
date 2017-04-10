CREATE PROCEDURE [dbo].[RoomSelectWithStatus]
	@houseId nvarchar(15)
AS
BEGIN
	DECLARE @roomStatusTable TABLE(
		Id nvarchar(15), 
		RoomNumber nvarchar(50), 
		HouseId nvarchar(15), 
		RoomTypeId nvarchar(15),
		Status nvarchar(50)
	)

	INSERT INTO @roomStatusTable 
		SELECT r.*, 'Available'
		FROM Room r 
		WHERE r.HouseId = @houseId AND r.Id NOT IN (SELECT odr.RoomId FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id WHERE o.Status = 'Active' OR o.Status = 'Reserved')
		ORDER BY r.RoomNumber

	INSERT INTO @roomStatusTable 
		SELECT r.*, 'Reserved'
		FROM Room r
		WHERE r.HouseId = @houseId AND r.Id IN (SELECT odr.RoomId FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id WHERE o.Status = 'Reserved')
		ORDER BY r.RoomNumber

	INSERT INTO @roomStatusTable 
		SELECT r.*, 'Busy'
		FROM Room r 
		WHERE r.HouseId = @houseId AND r.Id IN (SELECT odr.RoomId FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id WHERE o.Status = 'Active')
		ORDER BY r.RoomNumber

	SELECT * FROM @roomStatusTable 
	ORDER BY RoomNumber
END