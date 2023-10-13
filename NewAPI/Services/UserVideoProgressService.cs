using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class UserVideoProgressService
    {
        public List<UserVideoProgress> Select()
        {
            UserVideoProgressRepository _userVideoProgressRepository;
            List<UserVideoProgress> _userVideoProgress;

            try
            {
                _userVideoProgressRepository = new UserVideoProgressRepository();
                _userVideoProgress = new List<UserVideoProgress>();


                _userVideoProgress = _userVideoProgressRepository.Select(null, null, null);

                return _userVideoProgress;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public UserVideoProgress SelectByID(Guid id)
        {
            UserVideoProgressRepository _userVideoProgressRepository;
            List<UserVideoProgress> _usersVideosProgress;
            UserVideoProgress _userVideoProgress;

            try
            {
                _userVideoProgressRepository = new UserVideoProgressRepository();

                _usersVideosProgress = _userVideoProgressRepository.Select(id, null, null);
                _userVideoProgress = _usersVideosProgress.FirstOrDefault();

                return _userVideoProgress;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public List<UserVideoProgress> SelectByCourseModuleIdAndUserId(Guid courseModuleId, Guid userId)
        {
            UserVideoProgressRepository _userVideoProgressRepository;
            List<UserVideoProgress> _userVideoProgress;

            try
            {
                _userVideoProgressRepository = new UserVideoProgressRepository();

                _userVideoProgress = _userVideoProgressRepository.Select(null, courseModuleId, userId);

                return _userVideoProgress;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public UserVideoProgress Insert(UserVideoProgress userVideoProgress)
        {
            UserVideoProgressRepository _userVideoProgressRepository; ;
            UserVideoProgress _userVideoProgress;

            try
            {
                _userVideoProgressRepository = new UserVideoProgressRepository();

                _userVideoProgress = _userVideoProgressRepository.Insert(userVideoProgress);

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _userVideoProgress;
        }

        public UserVideoProgress UpdateIsWatched(Guid id, UserVideoProgress userVideoProgress)
        {
            UserVideoProgressRepository _userVideoProgressRepository;
            UserVideoProgress _userVideoProgress;

            try
            {
                _userVideoProgressRepository = new UserVideoProgressRepository();

                _userVideoProgress = _userVideoProgressRepository.Update(id, userVideoProgress);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userVideoProgress;
        }

        public bool Delete(Guid id)
        {
            UserVideoProgressRepository _userVideoProgressRepository;
            bool isDeleted = false;

            try
            {
                _userVideoProgressRepository = new UserVideoProgressRepository();

                isDeleted = _userVideoProgressRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
