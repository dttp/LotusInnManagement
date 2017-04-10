CREATE PROCEDURE [dbo].[PaymentRecordGetByOrderId]
	@orderId nvarchar(15),
	@type nvarchar(20)
AS
	SELECT * FROM PaymentRecord
	WHERE OrderId = @orderId AND Type = @type
