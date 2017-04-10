CREATE TABLE [dbo].[RoomType] (
    [Id]            NVARCHAR (15) NOT NULL,
    [HouseId]       NVARCHAR (15) NOT NULL,
    [Price]         NVARCHAR (50) NOT NULL,
    [Unit]          NVARCHAR (3)  NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [PricePerWeek]  NVARCHAR (50) CONSTRAINT [DF_RoomType_PricePerWeek] DEFAULT ((0)) NOT NULL,
    [PricePerNight] NVARCHAR (50) CONSTRAINT [DF_RoomType_PricePerNight] DEFAULT ((0)) NOT NULL,
    [UnitPerWeek]   NVARCHAR (3)  CONSTRAINT [DF_RoomType_UnitPerWeek] DEFAULT (N'USD') NOT NULL,
    [UnitPerNight]  NVARCHAR (3)  CONSTRAINT [DF_RoomType_UnitPerNight] DEFAULT (N'USD') NOT NULL,
    [Square]        NVARCHAR (5)  NULL,
    CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

