CREATE PROCEDURE [dbo].[PaymentRecordUpdate]
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
	UPDATE PaymentRecord
	SET Name = @name,
		Date = @date,
		Amount = @amount,
		Unit = @unit,
		Method = @method,
		Note = @note,
		Type = @type,
		MoneySourceId = @moneySourceId,
		OrderId = @orderId
	WHERE Id = @id

