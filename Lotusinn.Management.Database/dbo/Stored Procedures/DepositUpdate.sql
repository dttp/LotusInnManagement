
CREATE PROCEDURE [dbo].[DepositUpdate]
	@id nvarchar(15),
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3)
AS
BEGIN
	UPDATE Deposit
	SET 		
		Date = @date,
		Amount = @amount,
		Unit = @unit
	WHERE Id = @id	
END