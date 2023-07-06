using Microsoft.AspNetCore.Mvc;
using System.Reflection;

using HobbyHaven.Shared.DTOs.Administration.Havens;
using HobbyHaven.BackEnd.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HobbyHaven.Shared.DTOs.Administration.Users;

namespace HobbyHaven.BackEnd.Controllers.Administration.Users
{

	// Endpoints for viewing personality tags

	[ApiController]
	public class AdministrationUsers : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }

		public AdministrationUsers(DataContext context) { _context = context; }


		// Endpoint for viewing all user as a administrator.

		[Route("api/administration/users/all")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DTOAdminUserView>>> Get()
        {

            List<DTOAdminUserView> revisedList = new() { };

            (await _context.Users.ToListAsync()).ForEach(x =>
            {
                revisedList.Add(new DTOAdminUserView
				{
                    Id = x.UserID,
                    Admin = x.Admin
                });
            });

            return Ok(revisedList);
        }

		// Endpoint for viewing a specific user as an administrator

		[Route("api/administration/users/{userID}/view")]
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<DTOAdminUserView>> Get(string userID)
        {

            User? user = await _context.Users.FindAsync(userID);

            if (user == null) return NotFound();
            else
            {
                return Ok(new DTOAdminUserView()
                {
					Id = user.UserID,
					Admin = user.Admin
				});
            }

        }

		// Endpoint for deleting a user from the database

		[Route("api/administration/users/{userID}/delete")]
		[Authorize]
		[HttpDelete]
        public async Task<IActionResult> Delete(string userID)
        {

            User? user = await _context.Users.FindAsync(userID);

            if (user == null) return NotFound();
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok();
            }

        }

        // Endpoint for editing a user

        [Route("api/administration/users/{userID}/edit")]
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<DTOAdminHavenView>> Post(string userID, [FromBody] Dictionary<string, string> changes)
        {

            // Define permitted changes allowed
            List<string> permitted_changes = new List<string>() { "Admin" };

            // Get the tag from the database
            User? user = await _context.Users.FindAsync(userID);

            if (user == null) return NotFound();

            // Get the type of the DatabasePersonalityTag so that it can overwrite the attributes
            Type type = typeof(User);

            // Loop through each change.
            foreach (KeyValuePair<string, string> pair in changes)
            {
                // Continue if change is permitted by the list.
                if (permitted_changes.Contains(pair.Key))
                {
                    // Get the property from the type by the name.
                    PropertyInfo property = type.GetProperty(pair.Key);

                    // If property exists set the value of the property.
                    if (property != null) property.SetValue(user, pair.Value, null);

                }

            }

            await _context.SaveChangesAsync();

            // Return the updated personality tag as a DTOAdminPersonalityTag
            return Ok(new DTOAdminUserView()
            {
				Id = user.UserID,
                Admin = user.Admin
			});

        }

    }

}
