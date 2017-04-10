
CREATE PROCEDURE [dbo].[NotificationInsert]
	@id nvarchar(15),
	@date datetime,
	@orderId nvarchar(15),
	@daysToPaymentDate int
AS
BEGIN
	INSERT INTO Notification(Id, Date, OrderId, DaysToPaymentDate)
	VALUES (@id, @date, @orderId, @daysToPaymentDate)
END