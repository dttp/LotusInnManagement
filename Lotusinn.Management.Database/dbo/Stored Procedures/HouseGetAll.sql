CREATE PROCEDURE [dbo].[HouseGetAll]	
	@houseId nvarchar(15) = NULL
AS
BEGIN
	IF (@houseId IS NULL OR @houseId = '')
	BEGIN
		SELECT Id, Name, Address 
		FROM dbo.[House]	
		ORDER BY Name ASC	
	END
	ELSE
	BEGIN
		SELECT Id, Name, Address
		FROM House
		WHERE Id = @houseId
	END
END