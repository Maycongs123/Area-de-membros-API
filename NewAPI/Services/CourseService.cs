using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class CourseService
    {
        public List<Course> Select()
        {
            CourseRepository _CourseRepository;
            List<Course> _Courses;

            try
            {
                _Courses = new List<Course>();

                _CourseRepository = new CourseRepository();

                _Courses = _CourseRepository.Select(null);

                return _Courses;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Course SelectByID(Guid id)
        {
            CourseRepository _courseRepository;
            List<Course> _courses;
            Course _course;

            try
            {
                _courseRepository = new CourseRepository();

                _courses = _courseRepository.Select(id);

                _course = _courses.FirstOrDefault();

                return _course;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public List<Course> SelectByUserId(Guid userId)
        {
            CourseRepository _courseRepository;
            CourseModuleService _courseModuleService;
            List<Course> _courses;
            List<CourseModule> _coursesModules;

            try
            {
                _courseRepository = new CourseRepository();
                _courseModuleService = new CourseModuleService();
                _courses = new List<Course>();

                _courses = _courseRepository.SelectByUserId(userId);

                foreach (Course course in _courses)
                {
                    course.CourseModule = _courseModuleService.SelectByCourseId(course.Id);
                }

                return _courses;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Course Insert(Guid userId, Course newCourse)
        {
            CourseRepository _courseRepository;
            UserCourseCourseRepository _userCursoRepository;
            Course _curso;
            UserCourse userCourse;

            try
            {
                _courseRepository = new CourseRepository();
                _userCursoRepository = new UserCourseCourseRepository();
                _curso = new Course();

                _curso = _courseRepository.Insert(newCourse);

                userCourse = new UserCourse
                {
                    CourseId = _curso.Id,
                    UserId = userId
                };

                _userCursoRepository.Insert(userCourse);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _curso;
        }

        public Course Update(Guid id, Course curso)
        {
            CourseRepository _cursoRepository;
            Course _curso;

            try
            {
                _cursoRepository = new CourseRepository();

                _curso = _cursoRepository.Update(id, curso);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _curso;
        }

        public bool Delete(Guid id)
        {
            CourseRepository _cursoRepository;
            bool isDeleted = false;

            try
            {
                _cursoRepository = new CourseRepository();

                isDeleted = _cursoRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
