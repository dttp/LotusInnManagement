CREATE TABLE [dbo].[Equipment] (
    [Id]          NVARCHAR (15)  NOT NULL,
    [RoomId]      NVARCHAR (15)  NULL,
    [HouseId]     NVARCHAR (15)  NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Quantity]    INT            NULL,
    [Status]      NVARCHAR (50)  NOT NULL,
    [Price]       NVARCHAR (20)  NOT NULL,
    [Unit]        NVARCHAR (3)   NOT NULL,
    [Category]    NVARCHAR (100) NULL,
    [CreatedDate] DATETIME       NULL,
    CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

