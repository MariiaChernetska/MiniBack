-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOfficeVideos]
(
	@UserId uniqueidentifier,
	@PageNum  INT = 1,
	@OrderBy BIT = 1,
	@Order BIT = 1
)
AS
BEGIN
	
select Videos.Id
	 , Users.FullName as UserName
	 , Videos.Title as Title
	 , Videos.Path
	 , Videos.ScreenShot
	 , Videos.DateAdded
	 , count(Ratings.Rating) as RatingsAmount
	 , avg(Ratings.Rating) as Rating
from Videos
     inner join Users on Videos.UserId = Users.Id
	 left join Ratings on Videos.Id = Ratings.VideoId
where Videos.UserID = @UserId
group by Videos.Id
	   , Users.FullName
	   , Videos.Title
	   , Videos.Path
	   , Videos.ScreenShot
	   , Videos.DateAdded
	   , Rating
order by 
	case when @OrderBy = 1 and @Order=1 then  Videos.DateAdded END ASC,
	case when @OrderBy = 0 and @Order = 1 then Rating END ASC,
	case when @OrderBy = 1 and @Order = 0 then Videos.DateAdded END DESC,
	case when @OrderBy = 0 and @Order = 0 then Rating END DESC
OFFSET (@PageNum - 1)*4 ROWS
FETCH NEXT 4 ROWS ONLY

END