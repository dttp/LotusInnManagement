CREATE PROCEDURE [dbo].[MoneySourceDelete]
	@id nvarchar(15)
AS
	DELETE MoneySource WHERE Id = @id
