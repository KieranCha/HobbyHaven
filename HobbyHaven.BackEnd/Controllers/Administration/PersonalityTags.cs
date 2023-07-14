using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using HobbyHaven.Shared.DTOs.Administration.PersonalityTags;
using HobbyHaven.BackEnd.Database.Models;
using HobbyHaven.BackEnd.Decorators.Authentication;
using Microsoft.Extensions.Options;

namespace HobbyHaven.BackEnd.Controllers.Administration.PersonalityTags
{

    [ApiController]
	public class AdministrationPersonalityTags : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }
		public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
		public AdministrationPersonalityTags(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
		{
			_context = context;
			_authenticationLinkSettings = authSettings.Value;
		}


		// Endpoint for getting all personality tags as an administrator.

		[Route("api/administration/personality-tags/all")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<List<DTOAdminPersonalityTagViewBasic>>> Get()
		{
			List<DTOAdminPersonalityTagViewBasic> revisedList = new() { };

			(await _context.PersonalityTags.Include(t => t.Users).Include(t => t.Hobbies).ToListAsync()).ForEach(x =>
			{
				revisedList.Add(x.ToAdminDTOBasic());
			});

			return Ok(revisedList);
		}



		// Endpoint for creating a personality tag by an administrator

		[Route("api/administration/personality-tags/create")]
		[AuthenticationLink]
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] DTOAdminCreatePersonalityTag personalityTagDTO)
		{
			PersonalityTag tag = new(personalityTagDTO);

            await _context.PersonalityTags.AddAsync(tag);
			await _context.SaveChangesAsync();

			return Ok(tag.ToAdminDTO());

		}

		// Endpoint for viewing a specific personality tag as an administrator

		[Route("api/administration/personality-tags/{PersonalityTagID}/view")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<DTOAdminPersonalityTagView>> Get(Guid PersonalityTagID)
		{

			PersonalityTag? tag = await _context.PersonalityTags.Include(t => t.Users).Include(t => t.Hobbies).FirstAsync(t => t.PersonalityTagID == PersonalityTagID);


			if (tag == null) return NotFound(); 
			else
			{
				return Ok(tag.ToAdminDTO());
			}

		}

		// Endpoint for deleting a personality tag from the database

		[Route("api/administration/personality-tags/{PersonalityTagID}/delete")]
		[AuthenticationLink]
		[HttpDelete]
		public async Task<IActionResult> Delete(Guid PersonalityTagID)
		{

			PersonalityTag? tag = await _context.PersonalityTags.Include(t => t.Users).Include(t => t.Hobbies).FirstAsync(t => t.PersonalityTagID == PersonalityTagID);

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
		public async Task<ActionResult<DTOAdminPersonalityTagView>> Post(Guid PersonalityTagID, [FromBody] Dictionary<string, string> changes)
		{

			// Define permitted changes allowed
			List<string> permitted_changes = new List<string>() { "Description", "Name" };

			// Get the tag from the database
			PersonalityTag? tag = await _context.PersonalityTags.Include(t => t.Users).Include(t => t.Hobbies).FirstAsync(t => t.PersonalityTagID == PersonalityTagID);

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
			return Ok(tag.ToAdminDTO());

		}

	}

}
