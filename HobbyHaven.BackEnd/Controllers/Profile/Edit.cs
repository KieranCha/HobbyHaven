using HobbyHaven.BackEnd.Database.Models.Users;
using HobbyHaven.Shared.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HobbyHaven.BackEnd.Controllers.Profile
{
    [Route("api/user/profile")]
    [ApiController]
    public class EditProfile : ControllerBase
    {
        [HttpPut]
        public IActionResult EditUserProfile([FromBody] Dictionary<string, string> changes)
        {
            string? token = Request.Headers["Authorization"];

            User user = new(token, true);


            return Ok();
        }
    }
}