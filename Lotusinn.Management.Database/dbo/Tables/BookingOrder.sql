CREATE TABLE [dbo].[BookingOrder] (
    [Id]           NVARCHAR (15)  NOT NULL,
    [Note]         NVARCHAR (500) NULL,
    [CheckinDate]  DATETIME       NULL,
    [CheckoutDate] DATETIME       NULL,
    [TotalGuest]   INT            NULL,
    [StayLength]   NVARCHAR (100) NULL,
    [Status]       NVARCHAR (50)  NULL,
    [HouseId]      NVARCHAR (15)  NULL,
    [PaymentCycle] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_BookingOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
);

