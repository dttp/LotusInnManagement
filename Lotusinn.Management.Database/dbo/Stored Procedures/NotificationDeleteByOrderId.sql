
CREATE PROCEDURE [dbo].[NotificationDeleteByOrderId]	
	@orderId nvarchar(15)
AS
BEGIN
	DELETE Notification
	WHERE OrderId = @orderId
END