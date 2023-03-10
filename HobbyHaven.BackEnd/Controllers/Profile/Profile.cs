using HobbyHaven.BackEnd.Database.Models.Users;
using HobbyHaven.Shared.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HobbyHaven.BackEnd.Controllers.Profile
{
    [Route("api/user/profile")]
    [ApiController]
    public class ViewProfile : ControllerBase
    {
        [HttpGet]
        public ActionResult<DTOUser> GetUserProfile([FromQuery] string profile) // Create the user account
        {
            User user = new(int.Parse(profile), true);

            return user.DTOUser;
        }
    }
}