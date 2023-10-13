using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class UserService
    {
        public List<User> Select()
        {
            UserRepository _userRepository;
            List<User> _users;

            try
            {
                _users = new List<User>();

                _userRepository = new UserRepository();

                _users = _userRepository.Select(null);

                return _users;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public User SelectByID(Guid id)
        {
            UserRepository _userRepository; ;
            List<User> _users;
            User _user;

            try
            {
                _userRepository = new UserRepository();

                _users = _userRepository.Select(id);
                _user = _users.FirstOrDefault();

                return _user;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public User Insert(User user)
        {
            UserRepository _userRepository; ;
            User _user;

            try
            {
                _userRepository = new UserRepository();

                _user = _userRepository.Insert(user);

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _user;
        }

        public User Update(Guid id, User user)
        {
            UserRepository _userRepository;
            User _user;

            try
            {
                _userRepository = new UserRepository();

                _user = _userRepository.Update(id, user);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return user;
        }

        public bool Delete(Guid id)
        {
            UserRepository _userRepository;
            bool isDeleted = false;

            try
            {
                _userRepository = new UserRepository();

                isDeleted = _userRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
