using Microsoft.AspNetCore.Mvc;
using System.Reflection;

using HobbyHaven.Shared.DTOs.Havens;
using HobbyHaven.BackEnd.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HobbyHaven.Shared.DTOs.Users;
using HobbyHaven.BackEnd.Decorators.Authentication;
using Microsoft.Extensions.Options;
using HobbyHaven.Shared.DTOs.Administration.Users;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using HobbyHaven.Shared.DTOs.Hobbies;

// totally not copy paste of adminstrator ver by harry
namespace HobbyHaven.BackEnd.Controllers.Users
{
    [ApiController]
    public class UserHobbies : ControllerBase, IDataController
    {

        // Set the datacontext object
        JwtSecurityTokenHandler tokenHandler = new();
        public DataContext _context { get; set; }
        public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
        public UserHobbies(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
        {
            _context = context;
            _authenticationLinkSettings = authSettings.Value;
        }

        [Route("api/users/profile/hobbies/{hobbyID}/add")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> AddHobby( Guid hobbyID)
        {
            var auth0ID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User? user = await _context.Users.Include(u => u.Hobbies).FirstAsync(u => u.UserID == auth0ID);
            if (user == null) return NoContent();

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);
            if (hobby == null) return NoContent();
            if (user.Hobbies.Contains(hobby)) return BadRequest();

            user.Hobbies.Add(hobby);

            _context.Entry(user).Collection(u => u.Hobbies).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok();
        }


        [Route("api/users/profile/hobbies/{hobbyID}/remove")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> RemoveHobby(Guid hobbyID)
        {
            var auth0ID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User? user = await _context.Users.Include(u => u.Hobbies).FirstAsync(u => u.UserID == auth0ID);
            if (user == null) return NoContent();

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);
            if (hobby == null) return NoContent();
            if (!user.Hobbies.Contains(hobby)) return BadRequest();

            user.Hobbies.Remove(hobby);

            _context.Entry(user).Collection(u => u.Hobbies).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Route("api/users/profile/hobbies/all")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DTOHobby>>> allHobbies()
        {
            var auth0ID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User? user = await _context.Users.Include(u => u.Hobbies).FirstAsync(u => u.UserID == auth0ID);
            if (user == null) return NoContent();
            if (user.Hobbies == null) return BadRequest();

            List<DTOHobby> revisedList = new();
            user.Hobbies.ForEach(x =>
            {
                revisedList.Add(x.ToDTO());
            });

            return Ok(revisedList);
        }

        [Route("api/users/profile/hobbies/subscribeCheck/{hobbyID}")]
        [AuthenticationLink]
        [HttpGet]
        public async Task<ActionResult<bool>> subscribeCheck(Guid hobbyID)
        {
            var auth0ID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User? user = await _context.Users.Include(u => u.Hobbies).FirstAsync(u => u.UserID == auth0ID);
            if (user == null) return NoContent();

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);
            if (hobby == null) return NoContent();

            if (user.Hobbies.Contains(hobby)) return Ok(true);
            else return Ok(false);
        }
    }
}
