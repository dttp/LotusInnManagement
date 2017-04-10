CREATE PROCEDURE [dbo].[DepositInsert]
	@id nvarchar(15),
	@orderId nvarchar(15),
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3)
AS
BEGIN
	INSERT INTO Deposit(Id, OrderId, Date, Amount, Unit) VALUES(@id, @orderId, @date, @amount, @unit)
END