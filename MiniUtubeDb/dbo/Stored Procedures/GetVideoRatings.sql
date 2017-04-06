-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetVideoRatings]
(
	@VideoId uniqueidentifier
)
AS
BEGIN

select Ratings.Comment
	 , Ratings.Rating
	 , Users.FullName as UserName
	 , Ratings.DateAdded 
from Ratings 
	 inner join Videos on Videos.ID = @VideoId
	 inner join Users on Users.ID = Ratings.UserID
order by Ratings.DateAdded Desc

END