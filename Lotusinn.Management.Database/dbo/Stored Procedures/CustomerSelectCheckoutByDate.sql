CREATE PROCEDURE [dbo].[CustomerSelectCheckoutByDate]
	@houseId nvarchar(15),
	@date datetime	
AS
BEGIN
	SELECT c.*, r.Id as RoomId, o.Id as OrderId, o.CheckinDate, o.CheckoutDate
	FROM Customer c INNER JOIN OrderCustomer oc ON c.Id = oc.CustomerId 
					INNER JOIN BookingOrder o ON oc.OrderId = o.Id 
					INNER JOIN OrderRoom odr ON o.Id = odr.OrderId 
					INNER JOIN Room r ON odr.RoomId = r.Id
	WHERE DATEDIFF(day, o.CheckoutDate, @date) >= 0 AND o.HouseId = @houseId AND o.Status = 'Active'
	ORDER BY o.CheckoutDate 
END