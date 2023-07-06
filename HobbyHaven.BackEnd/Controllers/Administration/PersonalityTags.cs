using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using HobbyHaven.Shared.DTOs.Administration.PersonalityTags;
using HobbyHaven.BackEnd.Database.Models;

namespace HobbyHaven.BackEnd.Controllers.Administration.PersonalityTags
{

	// Endpoints for viewing personality tags

	[ApiController]
	public class AdministrationPersonalityTags : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }

		public AdministrationPersonalityTags(DataContext context) { _context = context; }
		

		// Endpoint for getting all personality tags as an administrator.

		[Route("api/administration/personality-tags/all")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<List<DTOAdminPersonalityTagView>>> Get()
		{
			List<DTOAdminPersonalityTagView> revisedList = new() { };

			(await _context.PersonalityTags.ToListAsync()).ForEach(x =>
			{
				revisedList.Add(new DTOAdminPersonalityTagView
				{
					Id = x.PersonalityTagID,
					Name = x.Name,
					Description = x.Description,
				});
			});

			return Ok(revisedList);
		}



		// Endpoint for creating a personality tag by an administrator

		[Route("api/administration/personality-tags/create")]
		[AuthenticationLink]
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] DTOAdminCreatePersonalityTag personalityTagDTO)
		{
			await _context.PersonalityTags.AddAsync(new()
			{
				Name = personalityTagDTO.Name,
				Description = personalityTagDTO.Description
			});

			await _context.SaveChangesAsync();

			return Ok();

		}

		// Endpoint for viewing a specific personality tag as an administrator

		[Route("api/administration/personality-tags/{PersonalityTagID}/view")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<DTOAdminPersonalityTagView>> Get(long PersonalityTagID)
		{

			PersonalityTag? tag = await _context.PersonalityTags.FindAsync(PersonalityTagID);

			if (tag == null) return NotFound(); 
			else
			{
				return Ok(new DTOAdminPersonalityTagView()
				{
					Id = tag.PersonalityTagID,
					Name = tag.Name,
					Description = tag.Description
				});
			}

		}

		// Endpoint for deleting a personality tag from the database

		[Route("api/administration/personality-tags/{PersonalityTagID}/delete")]
		[AuthenticationLink]
		[HttpDelete]
		public async Task<IActionResult> Delete(long PersonalityTagID)
		{

			PersonalityTag? tag = await _context.PersonalityTags.FindAsync(PersonalityTagID);

			if (tag == null) return NotFound();
			else
			{
				_context.PersonalityTags.Remove(tag);
				await _context.SaveChangesAsync();

				return Ok();
			}

		}

		// Endpoint for editing a personality tag

		[Route("api/administration/personality-tags/{PersonalityTagID}/edit")]
		[AuthenticationLink]
		[HttpPost]
		public async Task<ActionResult<DTOAdminPersonalityTagView>> Post(long PersonalityTagID, [FromBody] Dictionary<string, string> changes)
		{

			// Define permitted changes allowed
			List<string> permitted_changes = new List<string>() { "Description", "Name" };

			// Get the tag from the database
			PersonalityTag? tag = await _context.PersonalityTags.FindAsync(PersonalityTagID);

			if (tag == null) return NotFound();

			// Get the type of the DatabasePersonalityTag so that it can overwrite the attributes
			Type type = typeof(PersonalityTag);

			// Loop through each change.
			foreach (KeyValuePair<string, string> pair in changes)
			{
				// Continue if change is permitted by the list.
				if (permitted_changes.Contains(pair.Key))
				{
					// Get the property from the type by the name.
					PropertyInfo property = type.GetProperty(pair.Key);

					// If property exists set the value of the property.
					if (property != null) property.SetValue(tag, pair.Value, null);

				}

			}

			await _context.SaveChangesAsync();

			// Return the updated personality tag as a DTOAdminPersonalityTag
			return Ok(new DTOAdminPersonalityTagView()
			{
				Id=tag.PersonalityTagID,
				Name=tag.Name,
				Description=tag.Description
			});

		}

	}

}
