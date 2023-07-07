using HobbyHaven.BackEnd.Controllers.Administration.PersonalityTags;
using HobbyHaven.BackEnd.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace HobbyHaven.BackEnd.Decorators
{

	public class AuthenticationLink : ActionFilterAttribute
	{

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			
			DataContext databaseContext = ((IDataController)context.Controller)._context;
			JwtSecurityTokenHandler tokenHandler = new();

			string? token = context.HttpContext.Request.Headers["authorization"];

			if (token != null)
			{
				string? Auth0UserID = tokenHandler.ReadJwtToken(token.Split(" ")[1]).Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

				User? currentUser = databaseContext.Users.Find(Auth0UserID);

				if (currentUser == null)
				{
					databaseContext.Users.Add(new User()
					{
						UserID = Auth0UserID,
						Admin = false
					});

					databaseContext.SaveChanges();
				}
			}

			base.OnActionExecuting(context);
		}
	}
}
