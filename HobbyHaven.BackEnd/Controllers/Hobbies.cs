using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.BackEnd.Database.Models;
using HobbyHaven.Shared.DTOs.Administration.Hobbies;

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
				revisedList.Add(x.ToDTO());
			});

			return Ok(revisedList);
		}

        [Route("api/hobbies/{hobbyID}/view")]
        [AuthenticationLink]
        [HttpGet]
        public async Task<ActionResult<DTOHobby>> Get(long hobbyID)
        {

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();
            else
            {
                return Ok(hobby.ToDTO());
            }

        }

    }

}
