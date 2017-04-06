using Dapper;
using ProcTest.Models;
using ProcTest.Models.Database;
using ProcTest.Models.Enums;
using ProcTest.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProcTest.Services
{
    public class VideoService : BaseService
    {
        public QueryResult<MainPageVideoViewModel> GetVideosForMain(int pageNum, OrderType orderBy, OrderDirection order, int pageSize)
        {
            var p = new DynamicParameters();
            p.Add("@PageNum", pageNum, DbType.Int32);
            p.Add("@OrderBy", orderBy, DbType.Int32);
            p.Add("@Order", order, DbType.Int32);
            var res = Connection.Query<CountViewModel>("GetAllVideos", p, commandType: CommandType.StoredProcedure);
            MainPageVideoViewModel videoRes = new MainPageVideoViewModel();
            int count = 0;
            
            return new QueryResult<MainPageVideoViewModel>(new MainPageVideoViewModel {
                PageNum = pageNum,
                VideosArray = res.Select(e => { count = e.Count; return e as VideoViewModel; }).ToList(),
                PagesAmount = (int)Math.Ceiling(count / (double)pageSize)
        });
        }

        public QueryResult<MainPageVideoViewModel> GetVideosForMainBlock(int pageNum, OrderType orderBy, OrderDirection order, int pageSize)
        {
            var p = new DynamicParameters();
            p.Add("@BlockNum", pageNum, DbType.Int32);
            p.Add("@OrderBy", orderBy, DbType.Int32);
            p.Add("@Order", order, DbType.Int32);
            var res = Connection.Query<CountViewModel>("GetVideosBlock", p, commandType: CommandType.StoredProcedure);
            MainPageVideoViewModel videoRes = new MainPageVideoViewModel();
            int count = 0;

            return new QueryResult<MainPageVideoViewModel>(new MainPageVideoViewModel
            {
                PageNum = pageNum,
                VideosArray = res.Select(e => { count = e.Count; return e as VideoViewModel; }).ToList(),
                PagesAmount = (int)Math.Ceiling(count / (double)pageSize)
            });
        }


        public QueryResult<List<VideoViewModel>> GetVideosForUser(int pageNum, OrderType orderBy, OrderDirection order, Guid userId)
        {
            var p = new DynamicParameters();
            p.Add("@PageNum", pageNum);
            p.Add("@OrderBy", orderBy);
            p.Add("@UserId", userId, DbType.Guid);
            p.Add("@Order", order);
            var res = Connection.Query<VideoViewModel>("GetOfficeVideos", p, commandType: CommandType.StoredProcedure);
            
            return new QueryResult<List<VideoViewModel>>(res.ToList());
        }

        public QueryResult<VideoRatingsViewModel> GetVideo(Guid videoId)
        {
            var p = new DynamicParameters();

            p.Add("@VideoId", videoId, DbType.Guid);
            var res = new VideoRatingsViewModel();
            var ratings = new List<RatingViewModel>();
            try {
                res = Connection.Query<VideoRatingsViewModel>("GetOneVideo", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ratings = Connection.Query<RatingViewModel>("GetVideoRatings", p, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex) {

            }
            res.Ratings = ratings;
            return new QueryResult<VideoRatingsViewModel>(res);
        }

        public QueryResult<VideoViewModel> SaveVideo(Video inputVideo) {
            var p = new DynamicParameters();
            p.Add("@UserId", inputVideo.UserId, DbType.Guid);
            p.Add("@Title", inputVideo.VideoName, DbType.String, size: 50);
            p.Add("@Path", inputVideo.Path, DbType.String);
            p.Add("@ScreenShot", inputVideo.ScreenshotPath, DbType.String);
            p.Add("@Description", inputVideo.Description, DbType.String);
            var res = new VideoViewModel();
            try
            {
                 res = Connection.Query<VideoViewModel>("SaveVideo", p, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            catch {}
            return new QueryResult<VideoViewModel>(res);
        }
        public QueryResult<RatingViewModel> SaveRating(Rating rating)
        {
            var p = new DynamicParameters();
            p.Add("@UserId", rating.UserId, DbType.Guid);
            p.Add("@VideoId", rating.VideoId, DbType.Guid);
            p.Add("@Rating", rating.RatingNum, DbType.Int32);
            p.Add("@Comment", rating.Comment, DbType.String);
           
            var res = Connection.Query<RatingViewModel>("SaveRating", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return new QueryResult<RatingViewModel>(res);
        }
    }
}