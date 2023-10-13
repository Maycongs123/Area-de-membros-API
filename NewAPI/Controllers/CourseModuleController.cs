using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseModuleController : Controller
    {
        [HttpGet]
        public List<CourseModule> Get()
        {
            CourseModuleService _courseModuleService;

            try
            {
                _courseModuleService = new CourseModuleService();

                return _courseModuleService.Select();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("{id}")]
        public CourseModule GetById(Guid id)
        {
            CourseModuleService _courseModuleService;
            CourseModule courseModule;

            try
            {
                _courseModuleService = new CourseModuleService();

                courseModule = _courseModuleService.SelectByID(id);

                return courseModule;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        [HttpGet("GetByCourseId/{courseId}")]
        public List<CourseModule> GetByCourseId(Guid courseId)
        {
            CourseModuleService _courseModuleService;
            List<CourseModule> courseModules;

            try
            {
                _courseModuleService = new CourseModuleService();

                courseModules = _courseModuleService.SelectByCourseId(courseId);

                return courseModules;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost]
        public CourseModule Post(CourseModule courseModule)
        {
            CourseModuleService _courseModuleService;
            CourseModule _courseModule;

            try
            {
                _courseModuleService = new CourseModuleService();

                _courseModule = _courseModuleService.Insert(courseModule);

                return _courseModule;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPut("{id}")]
        public CourseModule Put(Guid id, [FromBody] CourseModule courseModule)
        {
            CourseModuleService _courseModuleService;
            CourseModule _courseModule;

            try
            {
                _courseModuleService = new CourseModuleService();

                _courseModule = _courseModuleService.Update(id, courseModule);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _courseModule;
        }


        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            CourseModuleService _courseModuleService;
            bool isDeleted = false;

            try
            {
                _courseModuleService = new CourseModuleService();

                isDeleted = _courseModuleService.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
