-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllVideos]
(
	@PageSize INT = 8,
	@PageNum  INT = 1,		
	@OrderBy BIT = 1,
	@Order BIT = 1
)
AS
BEGIN

select COUNT(*) OVER() AS Count
	 , Videos.Id
	 , Users.FullName as UserName
	 , Videos.Title as Title
	 , Videos.Path
	 , Videos.ScreenShot
	 , Videos.DateAdded
	 , count (Ratings.Rating)  as RatingsAmount
	 , avg(Ratings.Rating)  as Rating
from Videos
     inner join Users on Videos.UserId = Users.Id
	 left join Ratings on Videos.Id = Ratings.VideoId	
group by  Videos.Id
		, Users.FullName
		, Videos.Title
		, Videos.Path
		, Videos.ScreenShot
		, Videos.DateAdded
order by 
	case when @OrderBy = 1 and @Order=1 then  Videos.DateAdded END ASC,
	case when @OrderBy = 0 and @Order = 1 then avg(Ratings.Rating) END ASC,
	case when @OrderBy = 1 and @Order = 0 then Videos.DateAdded END DESC,
	case when @OrderBy = 0 and @Order = 0 then avg(Ratings.Rating) END DESC
OFFSET (@PageNum - 1)*@PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY OPTION (RECOMPILE)

END