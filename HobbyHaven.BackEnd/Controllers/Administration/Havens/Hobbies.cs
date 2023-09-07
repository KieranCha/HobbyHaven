using Microsoft.AspNetCore.Mvc;

using HobbyHaven.BackEnd.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using HobbyHaven.Shared.DTOs.Administration.Hobbies;
using HobbyHaven.BackEnd.Controllers.Hobbies;
using HobbyHaven.Shared.DTOs.Administration.Havens;

namespace HobbyHaven.BackEnd.Controllers.Administration.Havens
{

    [ApiController]
    public class AdministrationHavenHobbies : ControllerBase, IDataController
    {

        // Set the datacontext object

        public DataContext _context { get; set; }
        public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
        public AdministrationHavenHobbies(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
        {
            _context = context;
            _authenticationLinkSettings = authSettings.Value;
        }

        [Route("api/administration/havens/{havenID}/hobbies/{hobbyID}/add")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTOAdminHavenView>> AddTag(Guid havenID, Guid hobbyID)
        {
            Haven? haven = await _context.Havens.Include(h => h.Hobbies).FirstAsync(h => h.HavenID == havenID);

            if (haven == null) return NotFound();

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();

            if (haven.Hobbies.Contains(hobby)) return BadRequest();

            haven.Hobbies.Add(hobby);

            _context.Entry(haven).Collection(h => h.Hobbies).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok(haven.ToAdminDTO());
        }


        [Route("api/administration/havens/{havenID}/hobbies/{hobbyID}/remove")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTOAdminHavenView>> RemoveTag(Guid havenID, Guid hobbyID)
        {
            Haven? haven = await _context.Havens.Include(h => h.Hobbies).FirstAsync(h => h.HavenID == havenID);

            if (haven == null) return NotFound();

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();

            if (!haven.Hobbies.Contains(hobby)) return BadRequest();

            haven.Hobbies.Remove(hobby);

            _context.Entry(haven).Collection(h => h.Hobbies).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok(haven.ToAdminDTO());
        }

    }

}
