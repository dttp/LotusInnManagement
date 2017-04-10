CREATE PROCEDURE [dbo].[OrderRoomInsert]
	@orderId nvarchar(15),
	@roomId nvarchar(15)
AS
BEGIN
	INSERT INTO OrderRoom(OrderId, RoomId) VALUES(@orderId, @roomId)
END