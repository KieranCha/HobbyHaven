﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.BackEnd.Database.Models;
using HobbyHaven.Shared.DTOs.Administration.Hobbies;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace HobbyHaven.BackEnd.Controllers.Hobbies
{

	// Endpoints for viewing personality tags

	[ApiController]
	public class Hobbies : ControllerBase, IDataController
	{

		// Set the datacontext object

		public DataContext _context { get; set; }
		public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
		public Hobbies(DataContext context, IOptions<AuthenticationLinkSettings> authSettings)
		{
			_context = context;
			_authenticationLinkSettings = authSettings.Value;
		}


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
        public async Task<ActionResult<DTOHobby>> Get(Guid hobbyID)
        {

            Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);

            if (hobby == null) return NotFound();
            else
            {
                return Ok(hobby.ToDTO());
            }

        }

		[Route("api/hobbies/subscribeCheck/{hobbyID}/{userID}")]
		[AuthenticationLink]
		[HttpGet]
		public async Task<ActionResult<bool>> subscribeCheck(Guid hobbyID, string userID)
		{
            User? user = await _context.Users.Include(u => u.PersonalityTags).Include(u => u.Hobbies).FirstAsync(u => u.UserID == userID);
			if (user == null) return NoContent();

			Hobby? hobby = await _context.Hobbies.FindAsync(hobbyID);
			if (hobby == null) return NoContent();

			if (user.Hobbies.Any<Hobby>(x => x == hobby)) return Ok(true);
			else return Ok(false);
		}

    }

}
