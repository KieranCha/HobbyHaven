using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HobbyHaven.Shared.DTOs.Havens;
using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.BackEnd.Controllers.Hobbies
{

	// Endpoints for viewing personality tags

	[ApiController]
	public class Havens : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }

		public Havens(DataContext context) { _context = context; }
		

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
