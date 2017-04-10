
CREATE PROCEDURE [dbo].[CustomerGetCount]
	@houseId nvarchar(15)
AS
BEGIN
	SELECT Count(CustomerId) FROM OrderCustomer oc INNER JOIN BookingOrder o ON oc.OrderId = o.Id WHERE o.HouseId = @houseId AND o.Status = 'Active'
END