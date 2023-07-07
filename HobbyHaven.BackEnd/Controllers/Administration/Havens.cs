using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using HobbyHaven.Shared.DTOs.Administration.Havens;
using HobbyHaven.BackEnd.Database.Models;

namespace HobbyHaven.BackEnd.Controllers.Administration.Havens
{

	[ApiController]
	public class AdministrationHavens : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }

		public AdministrationHavens(DataContext context) { _context = context; }


		// Endpoint for viewing all havens as a administrator.

		[Route("api/administration/havens/all")]
        [AuthenticationLink]
        [HttpGet]
        public async Task<ActionResult<List<DTOAdminHavenView>>> Get()
        {

            List<DTOAdminHavenView> revisedList = new() { };

            (await _context.Havens.ToListAsync()).ForEach(x =>
            {
                revisedList.Add(x.ToAdminDTO());
            });

            return Ok(revisedList);
        }

		// Endpoint for creating a haven as an admin.

		[Route("api/administration/havens/create")]
		[AuthenticationLink]
		[HttpPost]
        public async Task<ActionResult> Post([FromBody] DTOAdminCreateHaven havenDTO)
        {

            if (havenDTO.OwnerID == null) { return StatusCode(403, "Please specify an OwnerID."); }

            Haven haven = new(havenDTO);

            await _context.Havens.AddAsync(haven);
            await _context.SaveChangesAsync();

            return Ok(haven.ToAdminDTO());
        }

		// Endpoint for viewing a specific haven as an administrator

		[Route("api/administration/havens/{havenID}/view")]
		[AuthenticationLink]
		[HttpGet]
        public async Task<ActionResult<DTOAdminHavenView>> Get(Guid havenID)
        {

            Haven? haven = await _context.Havens.FindAsync(havenID);

            if (haven == null) return NotFound();
            else
            {
                return Ok(haven.ToAdminDTO());
            }

        }

		// Endpoint for deleting a haven from the database

		[Route("api/administration/havens/{havenID}/delete")]
		[AuthenticationLink]
		[HttpDelete]
        public async Task<IActionResult> Delete(Guid havenID)
        {

            Haven? haven = await _context.Havens.FindAsync(havenID);

            if (haven == null) return NotFound();
            else
            {
                _context.Havens.Remove(haven);
                await _context.SaveChangesAsync();

                return Ok();
            }

        }

        // Endpoint for editing a haven

        [Route("api/administration/havens/{havenID}/edit")]
		[AuthenticationLink]
		[HttpPost]
        public async Task<ActionResult<DTOAdminHavenView>> Post(Guid havenID, [FromBody] Dictionary<string, string> changes)
        {

            // Define permitted changes allowed
            List<string> permitted_changes = new List<string>() { "Description", "Name", "Location", "Address", "OwnerID" };

            // Get the tag from the database
            Haven? haven = await _context.Havens.FindAsync(havenID);

            if (haven == null) return NotFound();

            // Get the type of the DatabasePersonalityTag so that it can overwrite the attributes
            Type type = typeof(Haven);

            // Loop through each change.
            foreach (KeyValuePair<string, string> pair in changes)
            {
                // Continue if change is permitted by the list.
                if (permitted_changes.Contains(pair.Key))
                {
                    // Get the property from the type by the name.
                    PropertyInfo property = type.GetProperty(pair.Key);

                    // If property exists set the value of the property.
                    if (property != null) property.SetValue(haven, pair.Value, null);

                }

            }

            await _context.SaveChangesAsync();

            // Return the updated personality tag as a DTOAdminPersonalityTag
            return Ok(haven.ToAdminDTO());

        }

    }

}
