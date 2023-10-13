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
    public class UserVideoProgressController : Controller
    {
        [HttpGet]
        public List<UserVideoProgress> Get()
        {
            UserVideoProgressService _videoService;

            try
            {
                _videoService = new UserVideoProgressService();

                return _videoService.Select();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("{id}")]
        public UserVideoProgress GetById(Guid id)
        {
            UserVideoProgressService _userVideoProgressService;
            UserVideoProgress userVideoProgress;

            try
            {
                _userVideoProgressService = new UserVideoProgressService();

                userVideoProgress = _userVideoProgressService.SelectByID(id);

                return userVideoProgress;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("GetByCourseModuleIdAndUserId/{courseModuleId}/{userId}")]
        public List<UserVideoProgress> GetByCourseModuleIdAndUserId(Guid courseModuleId, Guid userId)
        {
            UserVideoProgressService _userVideoProgressService;
            List<UserVideoProgress> userVideoProgress;

            try
            {
                _userVideoProgressService = new UserVideoProgressService();

                userVideoProgress = _userVideoProgressService.SelectByCourseModuleIdAndUserId(courseModuleId, userId);

                return userVideoProgress;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost]
        public UserVideoProgress Post(UserVideoProgress userVideoProgress)
        {
            UserVideoProgressService _userVideoProgressService;
            UserVideoProgress _userVideoProgress;

            try
            {
                _userVideoProgressService = new UserVideoProgressService();

                _userVideoProgress = _userVideoProgressService.Insert(userVideoProgress);

                return _userVideoProgress;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPut("{id}")]
        public UserVideoProgress Put(Guid id, [FromBody] UserVideoProgress userVideoProgress)
        {
            UserVideoProgressService _userVideoProgressService;
            UserVideoProgress _userVideoProgress;

            try
            {
                _userVideoProgressService = new UserVideoProgressService();

                _userVideoProgress = _userVideoProgressService.UpdateIsWatched(id, userVideoProgress);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _userVideoProgress;
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            UserVideoProgressService _userVideoProgressService;
            bool isDeleted = false;

            try
            {
                _userVideoProgressService = new UserVideoProgressService();

                isDeleted = _userVideoProgressService.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
