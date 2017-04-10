CREATE TABLE [dbo].[Notification] (
    [Id]                NVARCHAR (15) NOT NULL,
    [Date]              DATETIME      NOT NULL,
    [OrderId]           NVARCHAR (15) NOT NULL,
    [DaysToPaymentDate] INT           NOT NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED ([Id] ASC)
);

