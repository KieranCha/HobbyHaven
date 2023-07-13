using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HobbyHaven.Shared.DTOs.Havens;
using Microsoft.Extensions.Options;

namespace HobbyHaven.BackEnd.Controllers.Hobbies
{

    // Endpoints for viewing personality tags

    [ApiController]
	public class Havens : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }
		public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
		public Havens(DataContext context, IOptions<AuthenticationLinkSettings> authSettings) { 
			_context = context; 
			_authenticationLinkSettings = authSettings.Value;
		}
		

		// Endpoint for getting all havens.

		[Route("api/havens/all")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<List<DTOHaven>>> Get()
		{
			List<DTOHaven> revisedList = new() { };

			(await _context.Havens.ToListAsync()).ForEach(x =>
			{
				revisedList.Add(x.ToDTO());
			});

			return Ok(revisedList);
		}


	}

}
