using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VideoController : Controller
    {
        [HttpGet]
        public List<Video> Get()
        {
            VideoService _videoService;

            try
            {
                _videoService = new VideoService();

                return _videoService.Select();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("{id}")]
        public Video GetById(Guid id)
        {
            VideoService _videoService;
            Video video;

            try
            {
                _videoService = new VideoService();

                video = _videoService.SelectByID(id);

                return video;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("GetByCourseModuleId/{courseModuleId}")]
        public List<Video> GetByCourseModuleId(Guid courseModuleId)
        {
            VideoService _videoService;
            List<Video> video;

            try
            {
                _videoService = new VideoService();

                video = _videoService.SelectByCourseModuleId(courseModuleId);

                return video;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost("{userId}")]
        public Video Post(Guid userId, Video video)
        {
            VideoService _videoService;
            Video _video;

            try
            {
                _videoService = new VideoService();

                _video = _videoService.Insert(userId, video);

                return _video;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPut("{id}")]
        public Video Put(Guid id, [FromBody] Video video)
        {
            VideoService _videoService;
            Video _video;

            try
            {
                _videoService = new VideoService();

                _video = _videoService.Update(id, video);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _video;
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            VideoService _videoService;
            bool isDeleted = false;

            try
            {
                _videoService = new VideoService();

                isDeleted = _videoService.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
