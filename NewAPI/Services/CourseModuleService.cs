using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class CourseModuleService
    {
        public List<CourseModule> Select()
        {
            CourseModuleRepository _moduleRepository;
            List<CourseModule> _modules;

            try
            {
                _modules = new List<CourseModule>();

                _moduleRepository = new CourseModuleRepository();

                _modules = _moduleRepository.Select(null);

                return _modules;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public CourseModule SelectByID(Guid id)
        {
            CourseModuleRepository _moduleRepository;
            List<CourseModule> _modules;
            CourseModule _module;

            try
            {
                _moduleRepository = new CourseModuleRepository();

                _modules = _moduleRepository.Select(id);
                _module = _modules.FirstOrDefault();

                return _module;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public List<CourseModule> SelectByCourseId(Guid courseId)
        {
            CourseModuleRepository _moduleRepository;
            List<CourseModule> _modules;

            try
            {
                _modules = new List<CourseModule>();

                _moduleRepository = new CourseModuleRepository();

                _modules = _moduleRepository.SelectByCourseId(courseId);

                return _modules;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public CourseModule Insert(CourseModule module)
        {
            CourseModuleRepository _moduleRepository; ;
            CourseModule _module;

            try
            {
                _moduleRepository = new CourseModuleRepository();

                _module = _moduleRepository.Insert(module);

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _module;
        }

        public CourseModule Update(Guid id, CourseModule module)
        {
            CourseModuleRepository _moduleRepository;
            CourseModule _module;

            try
            {
                _moduleRepository = new CourseModuleRepository();

                _module = _moduleRepository.Update(id, module);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return module;
        }

        public bool Delete(Guid id)
        {
            CourseModuleRepository _moduleRepository;
            bool isDeleted = false;

            try
            {
                _moduleRepository = new CourseModuleRepository();

                isDeleted = _moduleRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
