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

IF ((SELECT COUNT(1) FROM [PermissionSets] WHERE Id='super-admin') = 0)
BEGIN
	INSERT INTO [dbo].[PermissionSets] (Id, Name) VALUES ('super-admin', 'Super Adminsitrator')
END

IF ((SELECT COUNT(1) FROM [Role] WHERE Id='super-admin') = 0)
BEGIN
	INSERT INTO [dbo].[Role] (Id, Name, PermissionSetsId) VALUES ('super-admin','Super Administrator', 'super-admin')
END

IF ((SELECT COUNT(1) FROM [User] WHERE Id='admin') = 0)
BEGIN
	INSERT INTO [dbo].[User](Id, Email, HouseId, Password, RoleId, Status, Username)
			VALUES ('admin', 'admin@lotusinn.vn', '', 'd9VudzcDjFxnIo3s82tdLF7MTwgU4XfmDUVGuQw', 'super-admin', 'Verified', 'admin')
END

--IF ((SELECT Count(1) FROM [Role] WHERE Id='r-admin') = 0)
--BEGIN
--	INSERT INTO [dbo].[Role] (Id, Name) VALUES ('r-admin', 'Administrator')
--END
--GO

--IF ((SELECT Count(1) FROM [Role] WHERE Id='r-manager') = 0)
--BEGIN
--	INSERT INTO [dbo].[Role] (Id, Name) VALUES ('r-manager', 'Manager')
--END
--GO

--IF ((SELECT Count(1) FROM [Role] WHERE Id='r-user') = 0)
--BEGIN
--	INSERT INTO [dbo].[Role] (Id, Name) VALUES ('r-user', 'Standard User')
--END
--GO

--IF ((SELECT Count(1) FROM [User] WHERE Id='u-admin') = 0)
--BEGIN
--	INSERT INTO [dbo].[User] (Id, Username, Phone, Email, HouseId, RoleId, Status, Password) VALUES ('u-admin', 'admin', '', 'admin@lotusinn.vn', '', 'r-admin','Verified', 'd9VudzcDjFxnIo3s82tdLF7MTwgU4XfmDUVGuQw')
--END
--GO

--IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-admin' AND [Object]='House') = 0)
--BEGIN
--	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-admin', 'House', 'RW')
--END
--GO

--IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-admin' AND [Object]='Role') = 0)
--BEGIN
--	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-admin', 'Role', 'RW')
--END
--GO

--IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-admin' AND [Object]='User') = 0)
--BEGIN
--	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-admin', 'User', 'RW')
--END
--GO

--IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-manager' AND [Object]='House') = 0)
--BEGIN
--	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-manager', 'House', 'R')
--END
--GO
