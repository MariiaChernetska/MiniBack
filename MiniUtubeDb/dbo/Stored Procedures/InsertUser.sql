-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertUser]                                                
(                         
     @Login            NVARCHAR(50),  
     @FullName         NVARCHAR(50),  
     @Password         NVARCHAR(50),
	 @Error			   NVARCHAR (40) = '' OUTPUT,
     @ErrorCode		   INT = 0 OUTPUT 
)
AS
BEGIN
    
DECLARE @ID uniqueidentifier = NEWID()
IF NOT EXISTS  (select * from Users where Login=@Login)
BEGIN
	INSERT INTO [Users] ([ID],[Login],[FullName],[Password],[RegistrationDate])
	VALUES (@ID,@Login,@FullName,@Password,GETUTCDATE())  
		
	SELECT * FROM Users WHERE ID = @ID
END 
ELSE
BEGIN 
	SET @Error = 'User already exists'
	SET @ErrorCode = 1
END          

END