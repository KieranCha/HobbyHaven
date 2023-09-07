using Microsoft.AspNetCore.Mvc;

using HobbyHaven.BackEnd.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using HobbyHaven.Shared.DTOs.Administration.Hobbies;

namespace HobbyHaven.BackEnd.Controllers.Administration.Users
{

    [ApiController]
    public class AdministrationHobbyPersonalityTags : ControllerBase, IDataController
    {

        // Set the datacontext object

        public DataContext _context { get; set; }
        public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
        public AdministrationHobbyPersonalityTags(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
        {
            _context = context;
            _authenticationLinkSettings = authSettings.Value;
        }

        [Route("api/administration/hobbies/{hobbyID}/personality-tags/{tagID}/add")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTOAdminHobbyView>> AddTag(Guid hobbyID, Guid tagID)
        {
            Hobby? hobby = await _context.Hobbies.Include(h => h.PersonalityTags).Include(h => h.Users).Include(h => h.Havens).FirstAsync(h => h.HobbyID == hobbyID);

            if (hobby == null) return NotFound();

            PersonalityTag? tag = await _context.PersonalityTags.FindAsync(tagID);

            if (tag == null) return NotFound();

            if (hobby.PersonalityTags.Contains(tag)) return BadRequest();

            hobby.PersonalityTags.Add(tag);

            _context.Entry(hobby).Collection(h => h.PersonalityTags).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok(hobby.ToAdminDTO());
        }


        [Route("api/administration/hobbies/{hobbyID}/personality-tags/{tagID}/remove")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTOAdminHobbyView>> RemoveTag(Guid hobbyID, Guid tagID)
        {
            Hobby? hobby = await _context.Hobbies.Include(h => h.PersonalityTags).Include(h => h.Users).Include(h => h.Havens).FirstAsync(h => h.HobbyID == hobbyID);

            if (hobby == null) return NotFound();

            PersonalityTag? tag = await _context.PersonalityTags.FindAsync(tagID);

            if (tag == null) return NotFound();

            if (!hobby.PersonalityTags.Contains(tag)) return BadRequest();

            hobby.PersonalityTags.Remove(tag);

            _context.Entry(hobby).Collection(h => h.PersonalityTags).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok(hobby.ToAdminDTO());
        }

    }

}
