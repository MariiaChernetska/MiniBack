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
     inner join Users on Users.ID = Ratings.UserID
		where Ratings.VideoID = @VideoId
order by Ratings.DateAdded Desc

END