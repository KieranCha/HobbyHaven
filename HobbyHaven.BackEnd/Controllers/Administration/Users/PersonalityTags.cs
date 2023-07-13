using Microsoft.AspNetCore.Mvc;
using System.Reflection;

using HobbyHaven.Shared.DTOs.Administration.Havens;
using HobbyHaven.BackEnd.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HobbyHaven.Shared.DTOs.Administration.Users;
using HobbyHaven.BackEnd.Decorators.Authentication;
using Microsoft.Extensions.Options;

namespace HobbyHaven.BackEnd.Controllers.Administration.Users
{

    [ApiController]
    public class AdministrationUserPersonalityTags : ControllerBase, IDataController
    {

        // Set the datacontext object

        public DataContext _context { get; set; }
        public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
        public AdministrationUserPersonalityTags(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
        {
            _context = context;
            _authenticationLinkSettings = authSettings.Value;
        }

        [Route("api/administration/users/{userID}/profile/personality-tags/{tagID}/add")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTOAdminUserView>> AddTag(string userID, Guid tagID)
        {
            User? user = await _context.Users.Include(u => u.PersonalityTags).Include(u => u.Hobbies).FirstAsync(u => u.UserID == userID);

            if (user == null) return NotFound();

            PersonalityTag? tag = await _context.PersonalityTags.FindAsync(tagID);

            if (tag == null) return NotFound();

            if (user.PersonalityTags.Contains(tag)) return BadRequest();

            user.PersonalityTags.Add(tag);

            _context.Entry(user).Collection(u => u.PersonalityTags).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok(user.ToAdminDTO());
        }


        [Route("api/administration/users/{userID}/profile/personality-tags/{tagID}/remove")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTOAdminUserView>> RemoveTag(string userID, Guid tagID)
        {
            User? user = await _context.Users.Include(u => u.PersonalityTags).Include(u => u.Hobbies).FirstAsync(u => u.UserID == userID);

            if (user == null) return NotFound();

            PersonalityTag? tag = await _context.PersonalityTags.FindAsync(tagID);

            if (tag == null) return NotFound();

            if (!user.PersonalityTags.Contains(tag)) return BadRequest();

            user.PersonalityTags.Remove(tag);

            _context.Entry(user).Collection(u => u.PersonalityTags).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok(user.ToAdminDTO());
        }

    }

}
