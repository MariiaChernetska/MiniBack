-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOneVideo] 
(
	@VideoId uniqueidentifier
)
AS
BEGIN
select Videos.Id
	 , Users.FullName as UserName
	 , Videos.Title as Title
	 , Videos.Path
	 , Videos.ScreenShot
	 , Videos.DateAdded
	 , count (Ratings.Rating) as RatingsAmount
	 , avg(Ratings.Rating)  as Rating
from Videos
     inner join Users on Videos.UserId = Users.Id
	 left join Ratings on Videos.Id = Ratings.VideoId
where Videos.ID = @VideoId
group by Videos.Id
       , Users.FullName
	   , Videos.Title
	   , Videos.Path
	   , Videos.ScreenShot
	   , Videos.DateAdded
	
END