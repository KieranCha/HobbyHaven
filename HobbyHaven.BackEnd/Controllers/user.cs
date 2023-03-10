using HobbyHaven.BackEnd.Database.Models.Users;
using HobbyHaven.Shared.DTOs.Authentication;
using HobbyHaven.Shared.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHaven.BackEnd.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class users : ControllerBase
    {
        [HttpPost]
        public ActionResult<Authentication> Post([FromForm] DTOCreateUser credentials) // Create the user account
        {

            User user = new(credentials.Email, true);

            if (user.exists) {
                return StatusCode(400, "Account already exists");
            } else
            {
                user.Username = credentials.Username;
                user.Password = credentials.Password;
                user.Create();
            }

            return StatusCode(200, "Account created");
        }

        [HttpGet]
        public ActionResult<Authentication> Get([FromForm] LoginInformation credentials) // Return the authentication token from the user account
        {
            User user = new(credentials.Email, true);

            Authentication response = new();

            if (user.exists && user.AuthenticateByPassword(credentials.Password))
            {
                response.Admin = user.Admin;
                response.Exists = true;
                response.Token = user.AuthorizationToken;
                response.id = user.Id;
                return response;
            }

            return StatusCode(401, response);
        }

    }
}