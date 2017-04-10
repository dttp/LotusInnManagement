CREATE PROCEDURE [dbo].[HouseGetById]	
	@id nvarchar(15)
AS
BEGIN
	SELECT Id, Name, Address 
	FROM dbo.[House]
	WHERE Id = @id
END