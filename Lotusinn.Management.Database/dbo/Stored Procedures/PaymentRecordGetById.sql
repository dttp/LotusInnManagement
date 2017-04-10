CREATE PROCEDURE [dbo].[PaymentRecordGetById]
	@id nvarchar(15)
AS
	SELECT * FROM PaymentRecord WHERE Id = @id
