using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

using HobbyHaven.Shared.DTOs.Administration.Hobbies;
using HobbyHaven.BackEnd.Database.Models;

namespace HobbyHaven.BackEnd.Controllers.Administration.Hobbies
{

	// Endpoints for viewing personality tags

	[ApiController]
	public class AdministrationHobbies : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }

		public AdministrationHobbies(DataContext context) { _context = context; }


        // Endpoint for viewing all hobbies as a administrator.

        [Route("api/administration/hobbies/all")]
		[AuthenticationLink]
		[HttpGet]
        public async Task<ActionResult<List<DTOAdminHobbyView>>> Get()
        {

            List<DTOAdminHobbyView> revisedList = new() { };

            (await _context.Hobbies.ToListAsync()).ForEach(x =>
            {
                revisedList.Add(new DTOAdminHobbyView
                {
                    Id = x.HobbyID,
                    Name = x.Name,
                    Description = x.Description,

                });
            });

            return Ok(revisedList);
        }

        // Endpoint for creating a hobby as an admin.

        [Route("api/administration/hobbies/create")]
		[AuthenticationLink]
		[HttpPost]
        public async Task<ActionResult> Post([FromBody] DTOAdminCreateHobby hobbyDTO)
        {

            await _context.Hobbies.AddAsync(new()
            {
                Name = hobbyDTO.Name,
                Description = hobbyDTO.Description
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        // Endpoint for viewing a specific hobby as an administrator

        [Route("api/administration/hobbies/{hobbyID}/view")]
		[AuthenticationLink]
		[HttpGet]
        public async Task<ActionResult<DTOAdminHobbyView>> Get(long hobbyID)
        {

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();
            else
            {
                return Ok(new DTOAdminHobbyView()
                {
                    Id = hobby.HobbyID,
                    Name = hobby.Name,
                    Description = hobby.Description
                });
            }

        }

        // Endpoint for deleting a hobby from the database

        [Route("api/administration/hobbies/{hobbyID}/delete")]
		[AuthenticationLink]
		[HttpDelete]
        public async Task<IActionResult> Delete(long hobbyID)
        {

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();
            else
            {
                _context.Hobbies.Remove(hobby);
                await _context.SaveChangesAsync();

                return Ok();
            }

        }

        // Endpoint for editing a hobby

        [Route("api/administration/hobbies/{hobbyID}/edit")]
		[AuthenticationLink]
		[HttpPost]
        public async Task<ActionResult<DTOAdminHobbyView>> Post(long hobbyID, [FromBody] Dictionary<string, string> changes)
        {

            // Define permitted changes allowed
            List<string> permitted_changes = new List<string>() { "Description", "Name" };

            // Get the tag from the database
            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();

            // Get the type of the DatabasePersonalityTag so that it can overwrite the attributes
            Type type = typeof(Hobby);

            // Loop through each change.
            foreach (KeyValuePair<string, string> pair in changes)
            {
                // Continue if change is permitted by the list.
                if (permitted_changes.Contains(pair.Key))
                {
                    // Get the property from the type by the name.
                    PropertyInfo property = type.GetProperty(pair.Key);

                    // If property exists set the value of the property.
                    if (property != null) property.SetValue(hobby, pair.Value, null);

                }

            }

            await _context.SaveChangesAsync();

            // Return the updated personality tag as a DTOAdminPersonalityTag
            return Ok(new DTOAdminHobbyView()
            {
                Id = hobby.HobbyID,
                Name = hobby.Name,
                Description = hobby.Description
            });

        }

    }

}
