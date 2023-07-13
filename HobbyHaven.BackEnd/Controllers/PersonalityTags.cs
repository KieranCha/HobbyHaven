using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HobbyHaven.Shared.DTOs.PersonalityTag;
using HobbyHaven.BackEnd.Decorators.Authentication;
using Microsoft.Extensions.Options;

namespace HobbyHaven.BackEnd.Controllers.PersonalityTags
{

    // Endpoints for viewing personality tags

    [ApiController]
	public class PersonalityTags : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }
		public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
		public PersonalityTags(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
		{
			_context = context;
			_authenticationLinkSettings = authSettings.Value;
		}


		// Endpoint for getting all personality tags.

		[Route("api/personality-tags/all")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<List<DTOPersonalityTag>>> Get()
		{
			List<DTOPersonalityTag> revisedList = new() { };

			(await _context.PersonalityTags.ToListAsync()).ForEach(x =>
			{
				revisedList.Add(new DTOPersonalityTag
				{
					Id = x.PersonalityTagID,
					Name = x.Name,
					Description = x.Description,
				});
			});

			return Ok(revisedList);
		}


	}

}
