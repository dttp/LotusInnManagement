CREATE PROCEDURE [dbo].[CustomerIsUsingInOrder]
	@id nvarchar(15)	
AS
BEGIN
	SELECT CustomerId FROM OrderCustomer
	WHERE CustomerId = @id
END