CREATE PROCEDURE [dbo].[EquipmentGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT * FROM Equipment WHERE Id = @id

END