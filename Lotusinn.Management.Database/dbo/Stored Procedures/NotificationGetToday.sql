
CREATE PROCEDURE [dbo].[NotificationGetToday]
AS
BEGIN
	SELECT * FROM Notification
	WHERE DATEDIFF(day, Date, GETDATE()) = 0
END