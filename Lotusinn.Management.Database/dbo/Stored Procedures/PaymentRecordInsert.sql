CREATE PROCEDURE [dbo].[PaymentRecordInsert]
	@id nvarchar(15),
	@name nvarchar(100),
	@date datetime,
	@amount nvarchar(20),
	@unit nvarchar(3),
	@method nvarchar(20),
	@note nvarchar(500),
	@type nvarchar(20),
	@moneySourceId nvarchar(15),
	@orderId nvarchar(15)
AS
	INSERT INTO PaymentRecord(Id, Name, Date, Amount, Unit, Method, Note, Type, MoneySourceId, OrderId)
	VALUES (@id, @name, @date, @amount, @unit, @method, @note, @type, @moneySourceId, @orderId)

