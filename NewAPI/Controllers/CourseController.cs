using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        [HttpGet]
        public List<Course> Get()
        {
            CourseService _cursoService;

            try
            {
                _cursoService = new CourseService();

                return _cursoService.Select();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("{id}")]
        public Course GetById(Guid id)
        {
            CourseService _cursoService;
            Course Course;

            try
            {
                _cursoService = new CourseService();

                Course = _cursoService.SelectByID(id);

                return Course;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("GetByuserId/{userId}")]
        public List<Course> GetByUserId(Guid userId)
        {
            CourseService _cursoService;
            List<Course> course;

            try
            {
                _cursoService = new CourseService();

                course = _cursoService.SelectByUserId(userId);

                return course;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost("{userId}")]
        public Course Post(Guid userId, Course curso)
        {
            CourseService _cursoService;
            Course _curso;

            try
            {
                _cursoService = new CourseService();

                _curso = _cursoService.Insert(userId, curso);

                return _curso;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        [HttpPut("{id}")]
        public Course Put(Guid id, [FromBody] Course curso)
        {
            CourseService _cursoService;
            Course _curso;

            try
            {
                _cursoService = new CourseService();

                _curso = _cursoService.Update(id, curso);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _curso;
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            CourseService _cursoService;
            bool isDeleted;

            try
            {
                _cursoService = new CourseService();

                isDeleted = _cursoService.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
