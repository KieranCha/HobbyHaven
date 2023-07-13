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
	public class AdministrationUserHobbies : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }
		public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
		public AdministrationUserHobbies(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
		{
			_context = context;
			_authenticationLinkSettings = authSettings.Value;
		}

		[Route("api/administration/users/{userID}/profile/hobbies/{hobbyID}/add")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTOAdminUserView>> AddHobby(string userID, Guid hobbyID)
        {
			User? user = await _context.Users.Include(u => u.PersonalityTags).Include(u => u.Hobbies).FirstAsync(u => u.UserID == userID);

			if (user == null) return NotFound();

			Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

			if (hobby == null) return NotFound();

			if (user.Hobbies.Contains(hobby)) return BadRequest();

			user.Hobbies.Add(hobby);

			_context.Entry(user).Collection(u => u.Hobbies).IsModified = true;
			await _context.SaveChangesAsync();

			return Ok(user.ToAdminDTO());
		}


		[Route("api/administration/users/{userID}/profile/hobbies/{hobbyID}/remove")]
		[Authorize]
		[HttpPost]
		public async Task<ActionResult<DTOAdminUserView>> RemoveHobby(string userID, Guid hobbyID)
		{
            User? user = await _context.Users.Include(u => u.PersonalityTags).Include(u => u.Hobbies).FirstAsync(u => u.UserID == userID);

            if (user == null) return NotFound();

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();

            if (!user.Hobbies.Contains(hobby)) return BadRequest();

            user.Hobbies.Remove(hobby);

            _context.Entry(user).Collection(u => u.Hobbies).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok(user.ToAdminDTO());
        }

	}

}
