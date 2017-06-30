/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF ((SELECT COUNT(1) FROM [Role] WHERE Id='super-admin') = 0)
BEGIN
	INSERT INTO [dbo].[Role] (Id, Name, Type) VALUES ('super-admin','Super Administrator', 'System')
END

IF ((SELECT COUNT(1) FROM [User] WHERE Id='admin') = 0)
BEGIN
	INSERT INTO [dbo].[User](Id, Email, HouseId, Password, RoleId, Status, Username)
			VALUES ('admin', 'admin@lotusinn.vn', '', 'd9VudzcDjFxnIo3s82tdLF7MTwgU4XfmDUVGuQw', 'super-admin', 'Verified', 'admin')
END

IF ((SELECT COUNT(1) FROM RoleObjectPermission WHERE RoleId='super-admin' AND [ObjectType] = 'House') = 0)
BEGIN
	INSERT INTO RoleObjectPermission(Id, RoleId, ObjectType, ObjectId, Permission)
	VALUES ('1', 'super-admin', 'House', NULL, 15)
END

IF ((SELECT COUNT(1) FROM RoleObjectPermission WHERE RoleId='super-admin' AND [ObjectType] = 'User') = 0)
BEGIN
	INSERT INTO RoleObjectPermission(Id, RoleId, ObjectType, ObjectId, Permission)
	VALUES ('2', 'super-admin', 'User', NULL, 15)
END

IF ((SELECT COUNT(1) FROM RoleObjectPermission WHERE RoleId='super-admin' AND [ObjectType] = 'Role') = 0)
BEGIN
	INSERT INTO RoleObjectPermission(Id, RoleId, ObjectType, ObjectId, Permission)
	VALUES ('3', 'super-admin', 'Role', NULL, 15)
END

IF ((SELECT COUNT(1) FROM RoleObjectPermission WHERE RoleId='super-admin' AND [ObjectType] = 'Warehouse') = 0)
BEGIN
	INSERT INTO RoleObjectPermission(Id, RoleId, ObjectType, ObjectId, Permission)
	VALUES ('4', 'super-admin', 'Warehouse', NULL, 15)
END


IF ((SELECT COUNT(1) FROM RoleObjectPermission WHERE RoleId='super-admin' AND [ObjectType] = 'MoneySource') = 0)
BEGIN
	INSERT INTO RoleObjectPermission(Id, RoleId, ObjectType, ObjectId, Permission)
	VALUES ('5', 'super-admin', 'MoneySource', NULL, 15)
END

IF ((SELECT COUNT(1) FROM RoleObjectPermission WHERE RoleId='super-admin' AND [ObjectType] = 'Order') = 0)
BEGIN
	INSERT INTO RoleObjectPermission(Id, RoleId, ObjectType, ObjectId, Permission)
	VALUES ('6', 'super-admin', 'Order', NULL, 15)
END