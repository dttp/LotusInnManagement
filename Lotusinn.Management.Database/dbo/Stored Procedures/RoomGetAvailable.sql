CREATE PROCEDURE [dbo].[RoomGetAvailable]
	@houseId nvarchar(15),
	@startDate DateTime,
	@endDate DateTime
AS
BEGIN
	SELECT r.*	
	FROM Room r 
	WHERE r.HouseId = @houseId AND 
		  r.Id NOT IN 
			(SELECT odr.RoomId 
			 FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id 
			 WHERE (o.Status = 'Active' OR o.Status = 'Reserved') AND 
					((DATEDIFF(day, o.CheckinDate, @startDate) >= 0) AND (DATEDIFF(day, @startDate, o.CheckoutDate) >= 0)) OR
					((DATEDIFF(day, o.CheckinDate, @endDate) >= 0) AND (DATEDIFF(day, @endDate, o.CheckOutDate) >= 0)))
	ORDER BY r.RoomNumber
END