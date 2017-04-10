CREATE PROCEDURE [dbo].[UserInsert]	
	@id nvarchar(15),
	@username nvarchar(50),
	@password nvarchar(50),
	@email nvarchar(50),
	@phone nvarchar(20),
	@houseId nvarchar(15),
	@roleId NVARCHAR(15)
AS
BEGIN
	INSERT INTO [dbo].[User](Id, Username, Password, Email, Phone, HouseId, RoleId, Status)
	VALUES (@id, @username, @password, @email, @phone, @houseId, @roleId, 'NotVerified')
END