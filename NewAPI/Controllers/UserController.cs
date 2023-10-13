using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet]
        public List<User> Get()
        {
            UserService _userService;

            try
            {
                _userService = new UserService();

                return _userService.Select();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("{id}")]
        public User GetById(Guid id)
        {
            UserService _userService;
            User user;

            try
            {
                _userService = new UserService();

                user = _userService.SelectByID(id);

                return user;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost]
        public User Post(User user)
        {
            UserService _userService;
            User _user;

            try
            {
                _userService = new UserService();

                _user = _userService.Insert(user);

                return _user;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPut("{id}")]
        public User Put(Guid id, [FromBody] User user)
        {
            UserService _userService;
            User _user;

            try
            {
                _userService = new UserService();

                _user = _userService.Update(id, user);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _user;
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            UserService _userService;
            bool isDeleted = false;

            try
            {
                _userService = new UserService();

                isDeleted = _userService.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
