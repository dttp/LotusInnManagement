CREATE PROCEDURE [dbo].[EquipmentDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE Equipment WHERE Id = @id
END