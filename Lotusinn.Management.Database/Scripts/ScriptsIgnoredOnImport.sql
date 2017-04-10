
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
	--The following statement was imported into the database project as a schema object and named dbo.House.
--CREATE TABLE [dbo].[House](
--		[Id] [nvarchar](15) NOT NULL,
--		[Name] [nvarchar](50) NOT NULL,
--		[Address] [nvarchar](200) NULL,
--	 CONSTRAINT [PK_House] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

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
	--The following statement was imported into the database project as a schema object and named dbo.Role.
--CREATE TABLE [dbo].[Role](
--		[Id] [nvarchar](15) NOT NULL,
--		[Name] [nvarchar](20) NOT NULL,
--	 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

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
	--The following statement was imported into the database project as a schema object and named dbo.User.
--CREATE TABLE [dbo].[User](
--		[Id] [nvarchar](15) NOT NULL,
--		[Username] [nvarchar](50) NOT NULL,
--		[Phone] [nvarchar](20) NULL,
--		[Password] [nvarchar](50) NOT NULL,
--		[Email] [nvarchar](50) NOT NULL,
--		[HouseId] [nvarchar](15) NULL,
--		[RoleId] [nvarchar](15) NOT NULL,
--		[Status] [nvarchar](20) NULL
--	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

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
	--The following statement was imported into the database project as a schema object and named dbo.RolePermission.
--CREATE TABLE [dbo].[RolePermission](
--		[RoleId] [nvarchar](15) NOT NULL,
--		[Object] [nvarchar](10) NOT NULL,
--		[Permission] [nvarchar](20) NOT NULL
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'RoomType'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.RoomType.
--CREATE TABLE [dbo].[RoomType](
--		[Id] [nvarchar](15) NOT NULL,
--		[HouseId] [nvarchar](15) NOT NULL,
--		[Name] [nvarchar](50) NOT NULL,
--		[Price] [nvarchar](50) NOT NULL,
--		[Unit] [nvarchar](3) NOT NULL,
--	 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Customer'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.Customer.
--CREATE TABLE [dbo].[Customer](
--		[Id] [nvarchar](15) NOT NULL,
--		[Name] [nvarchar](50) NOT NULL,
--		[Phone] [nvarchar](20) NULL,		
--		[Email] [nvarchar](50) NULL,
--		[Address] [nvarchar](200) NULL,
--		[Country] [nvarchar](20) NULL,
--		[PassportOrId] [nvarchar](50) NOT NULL
--	 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'BookingOrder'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.BookingOrder.
--CREATE TABLE [dbo].[BookingOrder](
--		[Id] [nvarchar](15) NOT NULL,
--		[Status] [nvarchar](50) NOT NULL,
--		[Note] [nvarchar](500) NULL,
--		[CheckinDate] DateTime NULL,
--		[CheckoutDate] DateTime NULL,
--		[TotalGuest] int,
--		[StayLength] nvarchar(100) NULL,
--		[HouseId] nvarchar(15) NOT NULL
--	 CONSTRAINT [PK_BookingOrder] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OrderCustomer'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.OrderCustomer.
--CREATE TABLE [dbo].[OrderCustomer](
--		[OrderId] nvarchar(15) NOT NULL,
--		[CustomerId] nvarchar(15) NOT NULL
--	 CONSTRAINT [PK_OrderCustomer] PRIMARY KEY CLUSTERED 
--	(
--		[OrderId] ASC,
--		[CustomerId] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OrderRoom'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.OrderRoom.
--CREATE TABLE [dbo].[OrderRoom](
--		[OrderId] nvarchar(15) NOT NULL,
--		[RoomId] nvarchar(15) NOT NULL
--	 CONSTRAINT [PK_OrderRoom] PRIMARY KEY CLUSTERED 
--	(
--		[OrderId] ASC,
--		[RoomId] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Deposit'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.Deposit.
--CREATE TABLE [dbo].[Deposit](
--		[Id] nvarchar(15) NOT NULL,
--		[OrderId] nvarchar(15) NOT NULL,
--		[Date] DateTime NOT NULL,
--		[Amount] nvarchar(10) NOT NULL,
--		[Unit] nvarchar(3) NOT NULL
--	 CONSTRAINT [PK_Deposit] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Discount'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.Discount.
--CREATE TABLE [dbo].[Discount](
--		[Id] nvarchar(15) NOT NULL,
--		[OrderId] nvarchar(15) NOT NULL,
--		[Reason] nvarchar(500) NOT NULL,
--		[Amount] nvarchar(10) NOT NULL,
--		[Unit] nvarchar(3) NOT NULL
--	 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Payment'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.Payment.
--CREATE TABLE [dbo].[Payment](
--		[Id] nvarchar(15) NOT NULL,		
--		[Name] nvarchar(500) NOT NULL,
--		[Date] DateTime NOT NULL,
--		[Amount] nvarchar(10) NOT NULL,
--		[Unit] nvarchar(3) NOT NULL,
--		[Method] nvarchar(100) NULL,
--		[Note] nvarchar(255) NULL
--	 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OrderPayment'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.OrderPayment.
--CREATE TABLE [dbo].[OrderPayment](
--		[PaymentId] nvarchar(15) NOT NULL,		
--		[OrderId] nvarchar(15) NOT NULL		
--	 CONSTRAINT [PK_OrderPayment] PRIMARY KEY CLUSTERED 
--	(
--		[PaymentId] ASC,
--		[OrderId] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Room'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.Room.
--CREATE TABLE [dbo].[Room](
--		[Id] nvarchar(15) NOT NULL,
--		[RoomNumber] nvarchar(5) NOT NULL,
--		[HouseId] nvarchar(15) NOT NULL,
--		[RoomTypeId] nvarchar(15) NOT NULL		
--	 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC		
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Equipment'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.Equipment.
--CREATE TABLE [dbo].[Equipment](
--		[Id] nvarchar(15) NOT NULL,
--		[RoomId] nvarchar(15) NULL,
--		[HouseId] nvarchar(15) NOT NULL,
--		[Name] nvarchar(100) NOT NULL,
--		[Category] nvarchar(100) NULL,
--		[Quantity] int,
--		[Status] nvarchar(50) NOT NULL,
--		[Price] nvarchar(20) NOT NULL,
--		[Unit] nvarchar(3) NOT NULL,
--		[CreatedDate] DateTime
--	 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC		
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

END
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Budget'))
BEGIN
	--The following statement was imported into the database project as a schema object and named dbo.Budget.
--CREATE TABLE [dbo].[Budget](
--		[Id] nvarchar(15) NOT NULL,		
--		[Name] nvarchar(500) NOT NULL,
--		[Date] DateTime NOT NULL,
--		[Amount] nvarchar(10) NOT NULL,
--		[Unit] nvarchar(3) NOT NULL,
--		[Method] nvarchar(100) NULL,
--		[Note] nvarchar(255) NULL,
--		[HouseId] nvarchar(15) NOT NULL,
--		[Type] nvarchar(20) NOT NULL
--	 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
--	(
--		[Id] ASC
--	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--	) ON [PRIMARY]

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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserGetById]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseGetAll]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseGetById]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[HouseDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserGetAll]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserUpdateStatus]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserUpdateStatus]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserChangePassword]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserChangePassword]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoleGetAll]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeGetAll]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeGetById]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypeDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomTypeDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelect]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelect]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerGetById]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerIsUsingInOrder]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerIsUsingInOrder]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelectCheckoutByDate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelectCheckoutByDate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelectActive]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelectActive]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSelectReserved]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[CustomerSelectReserved]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomGetAll]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomGetById]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomGetAvailable]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomGetAvailable]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomSelectWithStatus]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[RoomSelectWithStatus]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepositInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DepositInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscountInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DiscountInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PaymentGetById]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderCustomerInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderCustomerInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderPaymentInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderPaymentInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderRoomInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderRoomInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderGetAll]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[OrderGetById]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentSelectAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentSelectAll]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentGetById]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[EquipmentDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetInsert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetInsert]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetUpdate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetDelete]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetSelectByMonth]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetSelectByMonth]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BudgetGetById]
GO

/*============================= DATA =======================================================*/
