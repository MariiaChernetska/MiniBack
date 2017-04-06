CREATE TABLE [dbo].[Videos] (
    [ID]          UNIQUEIDENTIFIER NOT NULL,
    [UserID]      UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (MAX)   NOT NULL,
    [Path]        NVARCHAR (MAX)   NOT NULL,
    [ScreenShot]  NVARCHAR (MAX)   NULL,
    [DateAdded]   DATETIME         NULL,
    [Description] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Videos_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);

