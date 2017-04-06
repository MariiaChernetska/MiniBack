CREATE TABLE [dbo].[Ratings] (
    [ID]        UNIQUEIDENTIFIER NOT NULL,
    [UserID]    UNIQUEIDENTIFIER NOT NULL,
    [VideoID]   UNIQUEIDENTIFIER NOT NULL,
    [Comment]   NVARCHAR (MAX)   NULL,
    [DateAdded] DATETIME         NOT NULL,
    [Rating]    INT              NOT NULL,
    CONSTRAINT [FK_Ratings_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Ratings_Videos] FOREIGN KEY ([VideoID]) REFERENCES [dbo].[Videos] ([ID])
);

