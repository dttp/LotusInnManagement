USE [LotusInn]
GO
/****** Object:  Table [dbo].[House]    Script Date: 6/20/2016 2:58:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'House'))
BEGIN
	CREATE TABLE [dbo].[House](
		[Id] [nvarchar](15) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Address] [nvarchar](200) NULL,
	 CONSTRAINT [PK_House] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/20/2016 2:58:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Role'))
BEGIN
	CREATE TABLE [dbo].[Role](
		[Id] [nvarchar](15) NOT NULL,
		[Name] [nvarchar](20) NOT NULL,
	 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/20/2016 2:58:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'User'))
BEGIN
	CREATE TABLE [dbo].[User](
		[Id] [nvarchar](15) NOT NULL,
		[Username] [nvarchar](50) NOT NULL,
		[Phone] [nvarchar](20) NULL,
		[Password] [nvarchar](50) NOT NULL,
		[Email] [nvarchar](50) NOT NULL,
		[HouseId] [nvarchar](15) NULL,
		[RoleId] [nvarchar](15) NOT NULL,
		[Status] [nvarchar](20) NULL
	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[RolePermission]    Script Date: 6/20/2016 4:47:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'RolePermission'))
BEGIN
	CREATE TABLE [dbo].[RolePermission](
		[RoleId] [nvarchar](15) NOT NULL,
		[Object] [nvarchar](10) NOT NULL,
		[Permission] [nvarchar](20) NOT NULL
	) ON [PRIMARY]
END
GO


IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'RoomType'))
BEGIN
	CREATE TABLE [dbo].[RoomType](
		[Id] [nvarchar](15) NOT NULL,
		[HouseId] [nvarchar](15) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Price] [nvarchar](50) NOT NULL,
		[Unit] [nvarchar](3) NOT NULL,
		[PricePerWeek] [nvarchar](50) NOT NULL,
		[UnitPerWeek] [nvarchar](3) NOT NULL,
		[PricePerNight] [nvarchar](50) NOT NULL,
		[UnitPerNight] [nvarchar](3) NOT NULL,
		[Square] nvarchar(5)
	 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Customer'))
BEGIN
	CREATE TABLE [dbo].[Customer](
		[Id] [nvarchar](15) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Phone] [nvarchar](20) NULL,		
		[Email] [nvarchar](50) NULL,
		[Address] [nvarchar](200) NULL,
		[Country] [nvarchar](20) NULL,
		[PassportOrId] [nvarchar](50) NOT NULL
	 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'BookingOrder'))
BEGIN
	CREATE TABLE [dbo].[BookingOrder](
		[Id] [nvarchar](15) NOT NULL,
		[Status] [nvarchar](50) NOT NULL,
		[Note] [nvarchar](500) NULL,
		[CheckinDate] DateTime NULL,
		[CheckoutDate] DateTime NULL,
		[TotalGuest] int,
		[StayLength] nvarchar(100) NULL,
		[HouseId] nvarchar(15) NOT NULL,
		[paymentCycle] nvarchar(50) NULL
	 CONSTRAINT [PK_BookingOrder] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OrderCustomer'))
BEGIN
	CREATE TABLE [dbo].[OrderCustomer](
		[OrderId] nvarchar(15) NOT NULL,
		[CustomerId] nvarchar(15) NOT NULL
	 CONSTRAINT [PK_OrderCustomer] PRIMARY KEY CLUSTERED 
	(
		[OrderId] ASC,
		[CustomerId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OrderRoom'))
BEGIN
	CREATE TABLE [dbo].[OrderRoom](
		[OrderId] nvarchar(15) NOT NULL,
		[RoomId] nvarchar(15) NOT NULL
	 CONSTRAINT [PK_OrderRoom] PRIMARY KEY CLUSTERED 
	(
		[OrderId] ASC,
		[RoomId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Deposit'))
BEGIN
	CREATE TABLE [dbo].[Deposit](
		[Id] nvarchar(15) NOT NULL,
		[OrderId] nvarchar(15) NOT NULL,
		[Date] DateTime NOT NULL,
		[Amount] nvarchar(10) NOT NULL,
		[Unit] nvarchar(3) NOT NULL
	 CONSTRAINT [PK_Deposit] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Discount'))
BEGIN
	CREATE TABLE [dbo].[Discount](
		[Id] nvarchar(15) NOT NULL,
		[OrderId] nvarchar(15) NOT NULL,
		[Reason] nvarchar(500) NOT NULL,
		[Amount] nvarchar(10) NOT NULL,
		[Unit] nvarchar(3) NOT NULL
	 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Payment'))
BEGIN
	CREATE TABLE [dbo].[Payment](
		[Id] nvarchar(15) NOT NULL,		
		[Name] nvarchar(500) NOT NULL,
		[Date] DateTime NOT NULL,
		[Amount] nvarchar(10) NOT NULL,
		[Unit] nvarchar(3) NOT NULL,
		[Method] nvarchar(100) NULL,
		[Note] nvarchar(255) NULL,	
		[Paid] bit
	 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OrderPayment'))
BEGIN
	CREATE TABLE [dbo].[OrderPayment](
		[PaymentId] nvarchar(15) NOT NULL,		
		[OrderId] nvarchar(15) NOT NULL		
	 CONSTRAINT [PK_OrderPayment] PRIMARY KEY CLUSTERED 
	(
		[PaymentId] ASC,
		[OrderId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Room'))
BEGIN
	CREATE TABLE [dbo].[Room](
		[Id] nvarchar(15) NOT NULL,
		[RoomNumber] nvarchar(5) NOT NULL,
		[HouseId] nvarchar(15) NOT NULL,
		[RoomTypeId] nvarchar(15) NOT NULL		
	 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC		
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Equipment'))
BEGIN
	CREATE TABLE [dbo].[Equipment](
		[Id] nvarchar(15) NOT NULL,
		[RoomId] nvarchar(15) NULL,
		[HouseId] nvarchar(15) NOT NULL,
		[Name] nvarchar(100) NOT NULL,
		[Category] nvarchar(100) NULL,
		[Quantity] int,
		[Status] nvarchar(50) NOT NULL,
		[Price] nvarchar(20) NOT NULL,
		[Unit] nvarchar(3) NOT NULL,
		[CreatedDate] DateTime
	 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC		
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Budget'))
BEGIN
	CREATE TABLE [dbo].[Budget](
		[Id] nvarchar(15) NOT NULL,		
		[Name] nvarchar(500) NOT NULL,
		[Date] DateTime NOT NULL,
		[Amount] nvarchar(10) NOT NULL,
		[Unit] nvarchar(3) NOT NULL,
		[Method] nvarchar(100) NULL,
		[Note] nvarchar(255) NULL,
		[HouseId] nvarchar(15) NOT NULL,
		[Type] nvarchar(20) NOT NULL
	 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Notification'))
BEGIN
	CREATE TABLE [dbo].[Notification](
		[Id] nvarchar(15) NOT NULL,		
		[Date] DateTime NOT NULL,
		[OrderId] nvarchar(15) NOT NULL,
		[DaysToPaymentDate] int NOT NULL
		
	 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

/*=============================================== Stored Proc ==================================================*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetByName]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserGetByName]
GO

CREATE PROCEDURE [dbo].[UserGetByName]
	@username nvarchar(50)
AS
BEGIN
	SELECT u.*, h.Name as HouseName, h.Address as HouseAddress, r.Name as RoleName
	FROM dbo.[User] u LEFT JOIN [dbo].[House] h ON u.HouseId = h.Id
		INNER JOIN [dbo].[Role] r ON u.RoleId = r.Id
	WHERE UserName = @username
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserGetById]
GO

CREATE PROCEDURE [dbo].[UserGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT u.*, h.Name as HouseName, h.Address as HouseAddress, r.Name as RoleName
	FROM dbo.[User] u LEFT JOIN [dbo].[House] h ON u.HouseId = h.Id
		INNER JOIN [dbo].[Role] r ON u.RoleId = r.Id
	WHERE u.Id = @id
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseGetAll]
GO

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

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseGetById]
GO

CREATE PROCEDURE [dbo].[HouseGetById]	
	@id nvarchar(15)
AS
BEGIN
	SELECT Id, Name, Address 
	FROM dbo.[House]
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseInsert]
GO

CREATE PROCEDURE [dbo].[HouseInsert]	
	@id nvarchar(15),
	@name nvarchar(50),
	@address nvarchar(200)
AS
BEGIN
	INSERT House(Id, Name, Address) VALUES(@id, @name, @address)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseUpdate]
GO

CREATE PROCEDURE [dbo].[HouseUpdate]	
	@id nvarchar(15),
	@name nvarchar(50),
	@address nvarchar(200)
AS
BEGIN
	UPDATE House
	SET Name = @name, Address=@address
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseDelete]
GO

CREATE PROCEDURE [dbo].[HouseDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE House 
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserGetAll]
GO

CREATE PROCEDURE [dbo].[UserGetAll]	
AS
BEGIN
	SELECT u.*, h.Name as HouseName, h.Address as HouseAddress, r.Name as RoleName
	FROM dbo.[User] u LEFT JOIN [dbo].[House] h ON u.HouseId = h.Id
		INNER JOIN [dbo].[Role] r ON u.RoleId = r.Id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserInsert]
GO

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

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserUpdate]
GO

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

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserUpdateStatus]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserUpdateStatus]
GO

CREATE PROCEDURE [dbo].[UserUpdateStatus]	
	@id nvarchar(15),
	@status nvarchar(20)
AS
BEGIN
	UPDATE [dbo].[User]
	SET Status = @status
	WHERE Id=@id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserDelete]
GO

CREATE PROCEDURE [dbo].[UserDelete]	
	@id nvarchar(15)	
AS
BEGIN
	DELETE [dbo].[User]
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserChangePassword]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserChangePassword]
GO

CREATE PROCEDURE [dbo].[UserChangePassword]
	@id nvarchar(15),
	@password nvarchar(50)	
AS
BEGIN
	UPDATE [dbo].[User]
	SET Password = @password
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoleGetAll]
GO

CREATE PROCEDURE [dbo].[RoleGetAll]	
AS
BEGIN
	SELECT * FROM Role
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeGetAll]
GO

CREATE PROCEDURE [dbo].[RoomTypeGetAll]	
	@houseId nvarchar(15)
AS
BEGIN
	SELECT *
	FROM dbo.[RoomType]	
	WHERE HouseId = @houseId
	ORDER BY Price ASC	
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeGetById]
GO

CREATE PROCEDURE [dbo].[RoomTypeGetById]	
	@id nvarchar(15)
AS
BEGIN
	SELECT *
	FROM dbo.[RoomType]	
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeInsert]
GO

CREATE PROCEDURE [dbo].[RoomTypeInsert]	
	@id nvarchar(15),
	@name nvarchar(50),
	@houseId nvarchar(15),
	@price nvarchar(50),
	@unit nvarchar(3),
	@pricePerWeek nvarchar(50),
	@unitPerWeek nvarchar(3),
	@pricePerNight nvarchar(50),
	@unitPerNight nvarchar(3),
	@square nvarchar(5)
AS
BEGIN
	INSERT RoomType(Id, Name, HouseId, Price, Unit, PricePerWeek, UnitPerWeek, PricePerNight, UnitPerNight, Square) VALUES(@id, @name, @houseId, @price, @unit, @pricePerWeek, @unitPerWeek, @pricePerNight, @unitPerNight, @square)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeUpdate]
GO

CREATE PROCEDURE [dbo].[RoomTypeUpdate]
	@id nvarchar(15),
	@name nvarchar(50),
	@houseId nvarchar(15),
	@price nvarchar(50),
	@unit nvarchar(3),
	@pricePerWeek nvarchar(50),
	@unitPerWeek nvarchar(3),
	@pricePerNight nvarchar(50),
	@unitPerNight nvarchar(3),
	@square nvarchar(5)
AS
BEGIN
	UPDATE RoomType
	SET Name = @name, HouseId = @houseId, Price = @price, Unit = @unit, PricePerWeek = @pricePerWeek, UnitPerWeek = @unitPerWeek, PricePerNight = @pricePerNight, UnitPerNight = @unitPerNight, Square = @square
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeDelete]
GO

CREATE PROCEDURE [dbo].[RoomTypeDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE RoomType
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelect]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelect]
GO

CREATE PROCEDURE [dbo].[CustomerSelect]	
	@name nvarchar(50) NULL,
	@email nvarchar(50) NULL,
	@passportOrId nvarchar(50) NULL,
	@pageSize int = 50,
	@pageIndex int = 1,
	@houseId nvarchar(15)
AS
BEGIN
	DECLARE @query nvarchar(2000), @condition nvarchar(2000), @start int, @end int
	SET @query = 'SELECT ROW_NUMBER() OVER (ORDER BY Name) AS RowNum, c.* FROM CUSTOMER c'
	SET @condition = ''

	SET @start = (@pageIndex - 1) * @pageSize + 1
	SET @end = @pageIndex * @pageSize

	IF @name IS NOT NULL AND @name != ''
		SET @condition = 'Name Like ''%' + @name + '%'''	
	IF @condition != '' 
		SET @condition = @condition + ' AND '
	IF @email IS NOT NULL AND @email != ''
		SET @condition = @condition + 'Email Like ''%' + @email + '%'''
	IF @condition != '' 
		SET @condition = @condition + ' AND '
	IF @passportOrId IS NOT NULL AND @passportOrId != ''
		SET @condition = @condition + 'PassportOrId LIKE ''%' + @passportOrId + '%'''
	
	IF @condition != '' SET @condition = @condition + ' AND '
	SET @condition = @condition + ' Id IN (SELECT CustomerId FROM OrderCustomer oc INNER JOIN BookingOrder o ON oc.OrderId = o.Id WHERE o.HouseId = ''' + @houseId	+ ''')'
	 
	SET @query = @query + ' WHERE ' + @condition

	SET @query = 'SELECT * FROM (' + @query + ') as customerwithrow WHERE RowNum >= ' + convert(nvarchar(10),@start) + ' AND RowNum <= ' + convert(nvarchar(10),@end)

	EXEC (@query)

	SELECT COUNT(CustomerId) as Total FROM OrderCustomer oc INNER JOIN BookingOrder o ON oc.OrderId = o.Id WHERE o.HouseId = @houseId
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerGetById]
GO

CREATE PROCEDURE [dbo].[CustomerGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT * FROM Customer 
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerGetCount]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerGetCount]
GO

CREATE PROCEDURE [dbo].[CustomerGetCount]
	@houseId nvarchar(15)
AS
BEGIN
	SELECT Count(CustomerId) FROM OrderCustomer oc INNER JOIN BookingOrder o ON oc.OrderId = o.Id WHERE o.HouseId = @houseId AND o.Status = 'Active'
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerInsert]
GO

CREATE PROCEDURE [dbo].[CustomerInsert]
	@id nvarchar(15),
	@name nvarchar(50),
	@phone nvarchar(20),
	@email nvarchar(50),
	@address nvarchar(200),
	@country nvarchar(20),
	@passportOrId nvarchar(50)	
AS
BEGIN
	INSERT Customer(Id,Name,Phone,Email,Address,Country,PassportOrId) VALUES(@id, @name,@phone,@email,@address,@country,@passportOrId)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerUpdate]
GO

CREATE PROCEDURE [dbo].[CustomerUpdate]
	@id nvarchar(15),
	@name nvarchar(50),
	@phone nvarchar(20),
	@email nvarchar(50),
	@address nvarchar(200),
	@country nvarchar(20),
	@passportOrId nvarchar(50)	
AS
BEGIN
	UPDATE Customer
	SET Name=@name, Phone=@phone,Address=@address, Email=@email,Country=@country, PassportOrId=@passportOrId
	WHERE Id=@id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerDelete]
GO

CREATE PROCEDURE [dbo].[CustomerDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE Customer
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerIsUsingInOrder]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerIsUsingInOrder]
GO

CREATE PROCEDURE [dbo].[CustomerIsUsingInOrder]
	@id nvarchar(15)	
AS
BEGIN
	SELECT CustomerId FROM OrderCustomer
	WHERE CustomerId = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelectCheckoutByDate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelectCheckoutByDate]
GO

CREATE PROCEDURE [dbo].[CustomerSelectCheckoutByDate]
	@houseId nvarchar(15),
	@date datetime	
AS
BEGIN
	SELECT c.*, r.Id as RoomId, o.Id as OrderId, o.CheckinDate, o.CheckoutDate
	FROM Customer c INNER JOIN OrderCustomer oc ON c.Id = oc.CustomerId 
					INNER JOIN BookingOrder o ON oc.OrderId = o.Id 
					INNER JOIN OrderRoom odr ON o.Id = odr.OrderId 
					INNER JOIN Room r ON odr.RoomId = r.Id
	WHERE DATEDIFF(day, o.CheckoutDate, @date) >= 0 AND o.HouseId = @houseId AND o.Status = 'Active'
	ORDER BY o.CheckoutDate 
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelectActive]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelectActive]
GO

CREATE PROCEDURE [dbo].[CustomerSelectActive]
	@name nvarchar(50) NULL,
	@email nvarchar(50) NULL,
	@passportOrId nvarchar(50) NULL,
	@pageSize int = 50,
	@pageIndex int = 1,
	@houseId nvarchar(15)	
AS
BEGIN

	DECLARE @activeCustomerTable TABLE (
		Id nvarchar(15),
		Name nvarchar(50),
		Phone nvarchar(20),
		Email nvarchar(50),
		Address nvarchar(200),
		Country nvarchar(20),
		PassportOrId nvarchar(50),
		OrderId nvarchar(15),
		CheckinDate DateTime,
		CheckoutDate DateTime
	)

	INSERT INTO @activeCustomerTable
		SELECT c.*, o.Id as OrderId, o.CheckinDate, o.CheckoutDate
		FROM Customer c INNER JOIN OrderCustomer oc ON c.Id = oc.CustomerId 
						INNER JOIN BookingOrder o ON oc.OrderId = o.Id 					
		WHERE o.HouseId = @houseId AND o.Status = 'Active'
		ORDER BY c.Name 

	DECLARE @start int, @end int
	
	SET @start = (@pageIndex - 1) * @pageSize + 1
	SET @end = @pageIndex * @pageSize

	IF @name IS NOT NULL AND @name != ''
		DELETE FROM @activeCustomerTable 
		WHERE Name Not like '%' + @name + '%'

	IF @email IS NOT NULL AND @email != ''
		DELETE FROM @activeCustomerTable 
		WHERE Email NOT LIKE '%' + @email + '%'
	
	IF @passportOrId IS NOT NULL AND @passportOrId != ''
		DELETE FROM @activeCustomerTable
		WHERE PassportOrId NOT LIKE '%' + @passportOrId + '%'
	
	SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Name) AS RowNum, * FROM @activeCustomerTable) as customerwithrow WHERE RowNum >= CONVERT(nvarchar(10), @start) AND RowNum <= CONVERT(nvarchar(10), @end)

	SELECT COUNT(Id) as Total FROM @activeCustomerTable

	SELECT odr.OrderId, odr.RoomId
	FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id
	WHERE o.Status = 'Active' AND o.HouseId = @houseId


END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelectReserved]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelectReserved]
GO

CREATE PROCEDURE [dbo].[CustomerSelectReserved]
	@name nvarchar(50) NULL,
	@email nvarchar(50) NULL,
	@passportOrId nvarchar(50) NULL,
	@pageSize int = 50,
	@pageIndex int = 1,
	@houseId nvarchar(15)	
AS
BEGIN

	DECLARE @reservedCustomerTable TABLE (
		Id nvarchar(15),
		Name nvarchar(50),
		Phone nvarchar(20),
		Email nvarchar(50),
		Address nvarchar(200),
		Country nvarchar(20),
		PassportOrId nvarchar(50),
		OrderId nvarchar(15),
		CheckinDate DateTime,
		CheckoutDate DateTime
	)

	INSERT INTO @reservedCustomerTable
		SELECT c.*, o.Id as OrderId, o.CheckinDate, o.CheckoutDate
		FROM Customer c INNER JOIN OrderCustomer oc ON c.Id = oc.CustomerId 
						INNER JOIN BookingOrder o ON oc.OrderId = o.Id 					
		WHERE o.HouseId = @houseId AND o.Status = 'Reserved'
		ORDER BY c.Name 

	DECLARE @start int, @end int	

	SET @start = (@pageIndex - 1) * @pageSize + 1
	SET @end = @pageIndex * @pageSize

	IF @name IS NOT NULL AND @name != ''
		DELETE FROM @reservedCustomerTable 
		WHERE Name Not like '%' + @name + '%'

	IF @email IS NOT NULL AND @email != ''
		DELETE FROM @reservedCustomerTable 
		WHERE Email NOT LIKE '%' + @email + '%'
	
	IF @passportOrId IS NOT NULL AND @passportOrId != ''
		DELETE FROM @reservedCustomerTable
		WHERE PassportOrId NOT LIKE '%' + @passportOrId + '%'
	
	SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Name) AS RowNum, * FROM @reservedCustomerTable) as customerwithrow WHERE RowNum >= CONVERT(nvarchar(10), @start) AND RowNum <= CONVERT(nvarchar(10), @end)

	SELECT COUNT(Id) as Total FROM @reservedCustomerTable

	SELECT odr.OrderId, odr.RoomId
	FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id
	WHERE o.Status = 'Reserved' AND o.HouseId = @houseId


END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomGetAll]
GO

CREATE PROCEDURE [dbo].[RoomGetAll]	
	@houseId nvarchar(15)
AS
BEGIN
	SELECT r.*, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit
	FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
	WHERE r.HouseId = @houseId
	ORDER BY r.RoomNumber ASC
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomGetById]
GO

CREATE PROCEDURE [dbo].[RoomGetById]	
	@id nvarchar(15)
AS
BEGIN
	SELECT r.*, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit
	FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
	WHERE r.Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomInsert]
GO

CREATE PROCEDURE [dbo].[RoomInsert]
	@id nvarchar(15),
	@houseId nvarchar(15),
	@roomNumber nvarchar(5),
	@roomTypeId nvarchar(15)		
AS
BEGIN
	INSERT Room(Id, HouseId, RoomNumber, RoomTypeId) 
	VALUES (@id, @houseId, @roomNumber, @roomTypeId)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomUpdate]
GO

CREATE PROCEDURE [dbo].[RoomUpdate]
	@id nvarchar(15),
	@roomNumber nvarchar(5),
	@roomTypeId nvarchar(15)		
AS
BEGIN
	UPDATE Room
	SET RoomNumber = @roomNumber,
		RoomTypeId = @roomTypeId
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomDelete]
GO

CREATE PROCEDURE [dbo].[RoomDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE Room
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomGetAvailable]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomGetAvailable]
GO

CREATE PROCEDURE [dbo].[RoomGetAvailable]
	@houseId nvarchar(15),
	@startDate DateTime,
	@endDate DateTime
AS
BEGIN
	SELECT r.*, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit
	FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
	WHERE r.HouseId = @houseId AND 
		  r.Id NOT IN 
			(SELECT odr.RoomId 
			 FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id 
			 WHERE (o.Status = 'Active' OR o.Status = 'Reserved') AND 
					((DATEDIFF(day, o.CheckinDate, @startDate) >= 0) AND (DATEDIFF(day, @startDate, o.CheckoutDate) >= 0)) OR
					((DATEDIFF(day, o.CheckinDate, @endDate) >= 0) AND (DATEDIFF(day, @endDate, o.CheckOutDate) >= 0)))
	ORDER BY r.RoomNumber
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomSelectWithStatus]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomSelectWithStatus]
GO

CREATE PROCEDURE [dbo].[RoomSelectWithStatus]
	@houseId nvarchar(15)
AS
BEGIN
	DECLARE @roomStatusTable TABLE(
		Id nvarchar(15), 
		RoomNumber nvarchar(50), 
		HouseId nvarchar(15), 
		RoomTypeId nvarchar(15),
		RoomTypeName nvarchar(100), 
		RoomTypePrice nvarchar(10), 
		RoomTypeUnit nvarchar(10), 
		Status nvarchar(50)
	)

	INSERT INTO @roomStatusTable 
		SELECT r.Id, r.RoomNumber, r.HouseId, r.RoomTypeId, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit, 'Available'
		FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
		WHERE r.HouseId = @houseId AND r.Id NOT IN (SELECT odr.RoomId FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id WHERE o.Status = 'Active' OR o.Status = 'Reserved')
		ORDER BY r.RoomNumber

	INSERT INTO @roomStatusTable 
		SELECT r.Id, r.RoomNumber, r.HouseId, r.RoomTypeId, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit, 'Reserved'
		FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
		WHERE r.HouseId = @houseId AND r.Id IN (SELECT odr.RoomId FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id WHERE o.Status = 'Reserved')
		ORDER BY r.RoomNumber

	INSERT INTO @roomStatusTable 
		SELECT r.Id, r.RoomNumber, r.HouseId, r.RoomTypeId, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit, 'Busy'
		FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
		WHERE r.HouseId = @houseId AND r.Id IN (SELECT odr.RoomId FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id WHERE o.Status = 'Active')
		ORDER BY r.RoomNumber

	SELECT * FROM @roomStatusTable 
	ORDER BY RoomNumber
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepositInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DepositInsert]
GO

CREATE PROCEDURE [dbo].[DepositInsert]
	@id nvarchar(15),
	@orderId nvarchar(15),
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3)
AS
BEGIN
	INSERT INTO Deposit(Id, OrderId, Date, Amount, Unit) VALUES(@id, @orderId, @date, @amount, @unit)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepositUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DepositUpdate]
GO

CREATE PROCEDURE [dbo].[DepositUpdate]
	@id nvarchar(15),
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3)
AS
BEGIN
	UPDATE Deposit
	SET 		
		Date = @date,
		Amount = @amount,
		Unit = @unit
	WHERE Id = @id	
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscountInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DiscountInsert]
GO

CREATE PROCEDURE [dbo].[DiscountInsert]
	@id nvarchar(15),
	@orderId nvarchar(15),
	@reason nvarchar(500),
	@amount nvarchar(10),
	@unit nvarchar(3)
AS
BEGIN
	INSERT INTO Discount(Id, OrderId, Reason, Amount, Unit) VALUES(@id, @orderId, @reason, @amount, @unit)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscountUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DiscountUpdate]
GO

CREATE PROCEDURE [dbo].[DiscountUpdate]
	@id nvarchar(15),
	@reason nvarchar(500),
	@amount nvarchar(10),
	@unit nvarchar(3)
AS
BEGIN
	UPDATE Discount
	SET 		
		Reason = @reason,
		Amount = @amount,
		Unit = @unit
	WHERE Id = @id	
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentInsert]
GO

CREATE PROCEDURE [dbo].[PaymentInsert]
	@id nvarchar(15),
	@name nvarchar(500),	
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3),
	@method nvarchar(100),
	@note nvarchar(255),
	@paid bit
AS
BEGIN
	INSERT INTO Payment(Id, Name, Date, Amount, Unit, Method, Note, Paid) VALUES(@id, @name, @date, @amount, @unit, @method, @note, @paid)
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentUpdate]
GO

CREATE PROCEDURE [dbo].[PaymentUpdate]
	@id nvarchar(15),
	@name nvarchar(500),	
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3),
	@method nvarchar(100),
	@note nvarchar(255),
	@paid bit
AS
BEGIN
	UPDATE Payment
	SET Name = @name,
		Date = @date,
		Amount = @amount,
		Unit = @unit,
		Method = @method,
		Note = @note,
		Paid = @paid
	WHERE Id = @id;
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentDelete]
GO

CREATE PROCEDURE [dbo].[PaymentDelete]
	@id nvarchar(15)
AS
BEGIN
	DELETE FROM OrderPayment WHERE PaymentId = @id

	DELETE FROM Payment WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentGetById]
GO

CREATE PROCEDURE [dbo].[PaymentGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT * FROM Payment
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderCustomerInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderCustomerInsert]
GO

CREATE PROCEDURE [dbo].[OrderCustomerInsert]
	@orderId nvarchar(15),
	@customerId nvarchar(15)
AS
BEGIN
	INSERT INTO OrderCustomer(OrderId, CustomerId) VALUES(@orderId, @customerId)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderPaymentInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderPaymentInsert]
GO

CREATE PROCEDURE [dbo].[OrderPaymentInsert]
	@orderId nvarchar(15),
	@paymentId nvarchar(15)
AS
BEGIN
	INSERT INTO OrderPayment(OrderId, PaymentId) VALUES(@orderId, @paymentId)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderRoomInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderRoomInsert]
GO

CREATE PROCEDURE [dbo].[OrderRoomInsert]
	@orderId nvarchar(15),
	@roomId nvarchar(15)
AS
BEGIN
	INSERT INTO OrderRoom(OrderId, RoomId) VALUES(@orderId, @roomId)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderInsert]
GO

CREATE PROCEDURE [dbo].[OrderInsert]
	@id nvarchar(15),
	@note nvarchar(500),
	@checkinDate Datetime,
	@checkoutDate DateTime,
	@totalGuest int,
	@stayLength nvarchar(100),
	@status nvarchar(50),
	@houseId nvarchar(15),
	@paymentCycle nvarchar(50)
AS
BEGIN
	INSERT INTO BookingOrder(Id, Note, CheckinDate, CheckoutDate, TotalGuest, StayLength, Status, HouseId, PaymentCycle) 
	VALUES (@id, @note, @checkinDate, @checkoutDate, @totalGuest, @stayLength, @status, @houseId, @paymentCycle)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderUpdate]
GO

CREATE PROCEDURE [dbo].[OrderUpdate]
	@id nvarchar(15),
	@note nvarchar(500),
	@checkinDate Datetime,
	@checkoutDate DateTime,
	@totalGuest int,
	@stayLength nvarchar(100),
	@status nvarchar(50),
	@houseId nvarchar(15),
	@paymentCycle nvarchar(50)
AS
BEGIN
	UPDATE BookingOrder
	SET Note = @note,
		CheckinDate = @checkinDate,
		CheckoutDate = @checkoutDate,
		TotalGuest = @totalGuest,
		StayLength = @stayLength,
		Status = @status,
		HouseId = @houseId,
		PaymentCycle = @paymentCycle
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderDelete]
GO

CREATE PROCEDURE [dbo].[OrderDelete]
	@id nvarchar(15)
AS
BEGIN			

	DELETE OrderCustomer
	WHERE OrderId = @id

	DELETE FROM Payment
	WHERE Id IN (SELECT PaymentId FROM OrderPayment WHERE OrderId = @id)

	DELETE FROM Discount
	WHERE OrderId = @id
	
	DELETE FROM Deposit 
	WHERE OrderId = @id

	DELETE OrderPayment
	WHERE OrderId = @id

	DELETE OrderRoom 
	WHERE OrderId = @id

	DELETE Notification
	WHERE OrderId = @id

	DELETE BookingOrder
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderGetAll]
GO

CREATE PROCEDURE [dbo].[OrderGetAll]
	
AS
BEGIN
	SELECT * FROM BookingOrder
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderGetById]
GO

CREATE PROCEDURE [dbo].[OrderGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT o.*, 
		   d.Id as DepositId, d.Amount as DepositAmount, d.Date as DepositDate, d.Unit as DepositUnit,
		   dc.Id as DiscountId, dc.Amount as DiscountAmount, dc.Reason as DiscountReason, dc.Unit as DiscountUnit
		
	FROM BookingOrder o INNER JOIN Deposit d ON o.Id = d.OrderId INNER JOIN Discount dc ON o.Id = dc.OrderId
	WHERE o.Id = @id
	ORDER BY o.CheckinDate DESC

	SELECT * FROM OrderCustomer WHERE OrderId = @id

	SELECT * FROM OrderRoom WHERE OrderId = @id;

	SELECT * FROM OrderPayment WHERE OrderId = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderSelectInRange]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderSelectInRange]
GO
CREATE PROCEDURE [dbo].[OrderSelectInRange]
	@houseId nvarchar(15),
	@startDate DateTime,
	@endDate DateTime
AS
BEGIN
	DECLARE @tmpTable TABLE ( 
		Id nvarchar(15)
	)
	INSERT INTO @tmpTable 
		SELECT o.Id
		FROM BookingOrder o 
		WHERE o.HouseId = @houseId 
			AND (o.Status = 'Active' OR o.Status = 'Reserved')
			AND ((o.CheckinDate BETWEEN @startDate AND @endDate) OR 
				(o.CheckoutDate BETWEEN @startDate AND @endDate) OR
				((@startDate BETWEEN o.CheckinDate AND o.CheckoutDate) AND (@endDate BETWEEN o.CheckinDate AND o.CheckoutDate)))
			--AND ((DATEDIFF(day, @startDate, o.CheckinDate) > 0 AND DATEDIFF(day, o.CheckinDate, @endDate) > 0) OR 
			--	 (DATEDIFF(day, @startDate, o.CheckoutDate) > 0 AND DATEDIFF(day, o.CheckoutDate, @endDate) > 0))

	SELECT o.*
	FROM BookingOrder o 
	WHERE o.Id in (SELECT Id FROM @tmpTable)

	SELECT * FROM OrderRoom 
	WHERE OrderId IN (SELECT Id FROM @tmpTable)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentSelectAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentSelectAll]
GO

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

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentGetById]
GO

CREATE PROCEDURE [dbo].[EquipmentGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT * FROM Equipment WHERE Id = @id

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentInsert]
GO

CREATE PROCEDURE [dbo].[EquipmentInsert]
	@id nvarchar(15),
	@houseId nvarchar(15),
	@roomId nvarchar(15),
	@name nvarchar(100),
	@category nvarchar(100),
	@quantity int,
	@status nvarchar(50),
	@price nvarchar(20),
	@unit nvarchar(3),
	@createdDate DateTime
AS
BEGIN
	
	INSERT INTO Equipment(Id, HouseId, RoomId, Name, Category, Quantity, Status, Price, Unit, CreatedDate)
	VALUES (@id, @houseId, @roomId, @name, @category, @quantity, @status, @price, @unit, @createdDate)

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentUpdate]
GO

CREATE PROCEDURE [dbo].[EquipmentUpdate]
	@id nvarchar(15),
	@houseId nvarchar(15),
	@roomId nvarchar(15),
	@name nvarchar(100),
	@category nvarchar(100),
	@quantity int,
	@status nvarchar(50),
	@price nvarchar(20),
	@unit nvarchar(3)
AS
BEGIN
	
	UPDATE Equipment
	SET HouseId = @houseId,
		RoomId = @roomId,
		Name = @name,
		Category = @category,
		Quantity = @quantity,
		Status = @status,
		Price = @price,
		Unit = @unit
	WHERE Id = @id

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentDelete]
GO

CREATE PROCEDURE [dbo].[EquipmentDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE Equipment WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetInsert]
GO

CREATE PROCEDURE [dbo].[BudgetInsert]
	@id nvarchar(15),
	@name nvarchar(500),
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3),
	@method nvarchar(100),
	@note nvarchar(255),
	@houseId nvarchar(15),
	@type nvarchar(20)	
AS
BEGIN
	INSERT INTO Budget(Id, Name, Date, Amount, Unit, Method, Note, HouseId, Type)
	VALUES (@id, @name, @date, @amount, @unit, @method, @note, @houseId, @type)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetUpdate]
GO

CREATE PROCEDURE [dbo].[BudgetUpdate]
	@id nvarchar(15),
	@name nvarchar(500),
	@date DateTime,
	@amount nvarchar(10),
	@unit nvarchar(3),
	@method nvarchar(100),
	@note nvarchar(255),
	@houseId nvarchar(15),
	@type nvarchar(20)	
AS
BEGIN
	UPDATE Budget
	SET Name = @name,
		Date = @date, 
		Amount = @amount,
		Unit = @unit,
		Method = @method,
		Note = @note,
		HouseId = HouseId,
		Type = type
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetDelete]
GO

CREATE PROCEDURE [dbo].[BudgetDelete]
	@id nvarchar(15)
AS
BEGIN
	DELETE Budget
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetSelectByMonth]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetSelectByMonth]
GO

CREATE PROCEDURE [dbo].[BudgetSelectByMonth]
	@houseId nvarchar(15),
	@date DateTime,
	@type nvarchar(20)
AS
BEGIN
	SELECT * FROM Budget b
	WHERE HouseId = @houseId 
	AND DATEPART(mm, Date) = DATEPART(Month, @date)
	AND DATEPART(YEAR, Date) = DATEPART(YEAR, @date)
	AND Type = @type
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetGetById]
GO

CREATE PROCEDURE [dbo].[BudgetGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT * FROM Budget
	WHERE Id = @id
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotificationGetToday]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[NotificationGetToday]
GO

CREATE PROCEDURE [dbo].[NotificationGetToday]
AS
BEGIN
	SELECT * FROM Notification
	WHERE DATEDIFF(day, Date, GETDATE()) = 0
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotificationInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[NotificationInsert]
GO

CREATE PROCEDURE [dbo].[NotificationInsert]
	@id nvarchar(15),
	@date datetime,
	@orderId nvarchar(15),
	@daysToPaymentDate int
AS
BEGIN
	INSERT INTO Notification(Id, Date, OrderId, DaysToPaymentDate)
	VALUES (@id, @date, @orderId, @daysToPaymentDate)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotificationDeleteByOrderId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[NotificationDeleteByOrderId]
GO

CREATE PROCEDURE [dbo].[NotificationDeleteByOrderId]	
	@orderId nvarchar(15)
AS
BEGIN
	DELETE Notification
	WHERE OrderId = @orderId
END

GO

/*============================= DATA =======================================================*/
IF ((SELECT Count(1) FROM [Role] WHERE Id='r-admin') = 0)
BEGIN
	INSERT INTO [dbo].[Role] (Id, Name) VALUES ('r-admin', 'Administrator')
END
GO

IF ((SELECT Count(1) FROM [Role] WHERE Id='r-manager') = 0)
BEGIN
	INSERT INTO [dbo].[Role] (Id, Name) VALUES ('r-manager', 'Manager')
END
GO

IF ((SELECT Count(1) FROM [Role] WHERE Id='r-user') = 0)
BEGIN
	INSERT INTO [dbo].[Role] (Id, Name) VALUES ('r-user', 'Standard User')
END
GO

IF ((SELECT Count(1) FROM [User] WHERE Id='u-admin') = 0)
BEGIN
	INSERT INTO [dbo].[User] (Id, Username, Phone, Email, HouseId, RoleId, Status, Password) VALUES ('u-admin', 'admin', '', 'admin@lotusinn.vn', '', 'r-admin','Verified', 'd9VudzcDjFxnIo3s82tdLF7MTwgU4XfmDUVGuQw')
END
GO

IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-admin' AND [Object]='House') = 0)
BEGIN
	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-admin', 'House', 'RW')
END

IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-admin' AND [Object]='Role') = 0)
BEGIN
	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-admin', 'Role', 'RW')
END

IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-admin' AND [Object]='User') = 0)
BEGIN
	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-admin', 'User', 'RW')
END

IF ((SELECT Count(1) FROM [RolePermission] WHERE RoleId='r-manager' AND [Object]='House') = 0)
BEGIN
	INSERT INTO [dbo].[RolePermission] (RoleId, Object, Permission) VALUES ('r-manager', 'House', 'R')
END
