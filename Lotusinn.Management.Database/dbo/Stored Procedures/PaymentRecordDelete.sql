CREATE PROCEDURE [dbo].[PaymentRecordDelete]
	@id nvarchar(15)
AS
	DELETE PaymentRecord
	WHERE Id = @id
