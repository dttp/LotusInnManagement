CREATE PROCEDURE [dbo].[HouseDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE House 
	WHERE Id = @id
END