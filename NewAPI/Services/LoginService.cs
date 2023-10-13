using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class LoginService
    {
        public User Select(Login login)
        {
            LoginRepository _loginRepository;
            List<User> _users;

            try
            {
                _users = new List<User>();

                _loginRepository = new LoginRepository();

                _users = _loginRepository.Select(login);

                var user = _users.FirstOrDefault();

                return user;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
    }
}
