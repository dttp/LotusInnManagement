
CREATE PROCEDURE [dbo].[OrderDelete]
	@id nvarchar(15)
AS
BEGIN			

	DELETE OrderCustomer
	WHERE OrderId = @id

	DELETE FROM Deposit 
	WHERE OrderId = @id

	DELETE OrderRoom 
	WHERE OrderId = @id

	DELETE Notification
	WHERE OrderId = @id

	DELETE BookingOrder
	WHERE Id = @id
END