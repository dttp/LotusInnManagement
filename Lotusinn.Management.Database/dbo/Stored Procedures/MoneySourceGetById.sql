CREATE PROCEDURE [dbo].[MoneySourceGetById]
	@id nvarchar(15)
AS
	SELECT * FROM MoneySource WHERE Id = @id
