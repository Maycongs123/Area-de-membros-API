using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Login([FromBody] Login login)
        {
            LoginService _loginService;
            TokenService _tokenService;

            try
            {
                _loginService = new LoginService();

                var _users = _loginService.Select(login);

                if (_users == null)
                {
                    return NotFound("Email ou senha invalida");
                }

                _tokenService = new TokenService();

                string _token = _tokenService.GenerateToken(_users);

                if (_token == null)
                {
                    return Unauthorized();
                }

                var token = new { token = _token, idUser = _users.Id, nameUser = _users.Name, ProfileImageBase64 = _users.ProfileImageBase64 };

                return Ok(token);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
    }
}
