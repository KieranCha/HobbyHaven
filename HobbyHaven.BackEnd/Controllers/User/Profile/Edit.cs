using HobbyHaven.BackEnd.Database.Models.Users;
using HobbyHaven.Shared.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace HobbyHaven.BackEnd.Controllers.Profile
{
    [Route("api/user/profile")]
    [ApiController]
    public class EditProfile : ControllerBase
    {
        [HttpPut]
        public IActionResult EditUserProfile([FromBody] Dictionary<string, string> changes)
        {
            List<string> permitted_changes = new List<string>() { "Bio", "Username", "ProfilePicture" };
            string? token = Request.Headers["Authorization"];

            User user = new(token, true);

            if (!user.AuthenticateByToken(token))
            {
                return StatusCode(401, "You are not authorized!");
            }

            Type type = typeof(User);

            foreach (KeyValuePair<string, string> pair in changes)
            {
                if (permitted_changes.Contains(pair.Key))
                {
                    PropertyInfo property = type.GetProperty(pair.Key);
                    if (property != null)
                    {
                        property.SetValue(user, pair.Value, null);
                    }
                }
            }


            user.Update();

            return Ok("Changes have been made.");
        }
    }
}