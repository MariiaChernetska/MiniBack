CREATE TABLE [dbo].[Users] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Login]            NVARCHAR (50)    NOT NULL,
    [FullName]         NVARCHAR (50)    NOT NULL,
    [Password]         NVARCHAR (50)    NOT NULL,
    [RegistrationDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Login_unique]
    ON [dbo].[Users]([Login] ASC);

