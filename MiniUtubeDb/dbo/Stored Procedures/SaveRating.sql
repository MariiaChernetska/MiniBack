-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveRating]
(
	@UserId uniqueidentifier,
	@VideoId uniqueidentifier, 
	@Comment NVARCHAR(MAX), 
	@Rating INT = 0
)
AS
BEGIN

Declare  @RatingId uniqueidentifier
Set @RatingId = NEWID()
Insert into Ratings (ID, UserID, VideoID, Comment, DateAdded, Rating)
values (@RatingId, @UserId, @VideoId, @Comment, GETUTCDATE(), @Rating)

select Ratings.Comment
	 , Ratings.DateAdded
	 , Ratings.Rating
	 , Users.FullName
from Ratings
	 inner join Users on Ratings.UserID = Users.ID
where Ratings.ID = @RatingId

END