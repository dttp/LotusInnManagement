CREATE PROCEDURE [dbo].[EquipmentSelectAll]
	@name nvarchar(100) = NULL,
	@category nvarchar(100) = NULL,
	@status nvarchar(50) = NULL,
	@houseId nvarchar(15) = NULL,
	@roomId nvarchar(15) = NULl
AS
BEGIN
	DECLARE @query nvarchar(1000)	
	SET @query = 'SELECT * FROM Equipment'

	IF @houseId IS NOT NULL AND @houseId != ''
	BEGIN
		SET @query = @query + ' WHERE HouseId Like ''%' + @houseId + '%'''
	END
	
	IF @name IS NOT NULL AND @name != ''
	BEGIN
		SET @query = @query + ' AND Name Like ''%' + @name + '%'''
	END
	
	IF @category IS NOT NULL AND @category != ''
	BEGIN
		SET @query = @query + ' AND Category Like ''%' + @category + '%'''
	END

	IF @status IS NOT NULL AND @status != ''
	BEGIN
		SET @query = @query + ' AND Status Like ''%' + @status + '%'''
	END

	IF @roomId IS NOT NULL AND @roomId != ''
	BEGIN
		SET @query = @query + ' AND RoomId Like ''%' + @roomId + '%'''
	END
	ELSE 
	BEGIN
		SET @query = @query + ' AND (RoomId IS NULL OR RoomId = '''')'
	END
	
	EXEC (@query)	

END