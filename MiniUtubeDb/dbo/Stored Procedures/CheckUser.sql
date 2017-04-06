-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckUser]                                                
(                         
     @Login            NVARCHAR(50),
	 @Password         NVARCHAR(50), 
	 @Exists           BIT = 1 OUTPUT,
	 @Error				NVARCHAR (40) = '' OUTPUT
)
AS
BEGIN

IF EXISTS (SELECT * FROM Users WHERE Login = @Login and Password = @Password)
BEGIN
	SELECT * FROM Users WHERE Login = @Login and Password = @Password	
END
ELSE 
BEGIN 
	SET @Error = 'Wrong email or password'
	SET @Exists = 0
END

END  
