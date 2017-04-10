CREATE PROCEDURE [dbo].[UserUpdate]	
	@id nvarchar(15),
	@username nvarchar(50),	
	@email nvarchar(50),
	@phone nvarchar(20),
	@houseId nvarchar(15),
	@roleId NVARCHAR(15)
AS
BEGIN
	UPDATE [dbo].[User]
	SET Username=@username,
		Email=@email,
		Phone=@phone,
		HouseId=@houseId,
		RoleId=@roleId
	WHERE Id=@id
END