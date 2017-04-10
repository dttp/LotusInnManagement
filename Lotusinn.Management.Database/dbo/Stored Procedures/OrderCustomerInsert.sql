CREATE PROCEDURE [dbo].[OrderCustomerInsert]
	@orderId nvarchar(15),
	@customerId nvarchar(15)
AS
BEGIN
	INSERT INTO OrderCustomer(OrderId, CustomerId) VALUES(@orderId, @customerId)
END