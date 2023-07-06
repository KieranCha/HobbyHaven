using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.BackEnd.Controllers.Hobbies
{

	// Endpoints for viewing personality tags

	[ApiController]
	public class Hobbies : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }

		public Hobbies(DataContext context) { _context = context; }
		

		// Endpoint for getting all hobbies.

		[Route("api/hobbies/all")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<List<DTOHobby>>> Get()
		{
			List<DTOHobby> revisedList = new() { };

			(await _context.Hobbies.ToListAsync()).ForEach(x =>
			{
				revisedList.Add(new DTOHobby
				{
					Id = x.HobbyID,
					Name = x.Name,
					Description = x.Description,
				});
			});

			return Ok(revisedList);
		}


	}

}
