using HobbyHaven.BackEnd.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace HobbyHaven.BackEnd.Decorators.Authentication
{

    public class AuthenticationLink : ActionFilterAttribute
	{
        private bool _IsAdminEndpoint;

        public AuthenticationLink(bool IsAdminEndpoint = false)
        {
            _IsAdminEndpoint = IsAdminEndpoint;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
			
            IDataController controller = (IDataController)context.Controller;

            if (!controller._authenticationLinkSettings.EnforceAuthorization)
            {
                base.OnActionExecuting(context);
                return;
            };

            // Get the database context object from the controller, by casting it to an interface so the compiler knows the controller has that funky attribute.

            DataContext databaseContext = controller._context;

            // Predefine a succesfullAuthentication as false, if false do not complete the request. Only flip to true when all authentication and authorization has been complete without throwing an error.

            bool succesfullAuthentication = false;

            try
            {

                // Extract the Auth0ID from the request headers.

                string? Auth0ID = GetCurrentUserID(context, databaseContext);

                // Get the user from the Auth0ID, if it exists in the database.

				User? currentUser = GetCurrentUser(databaseContext, Auth0ID);

                // If current user doesnt exist in the database, create it and return the user object.

                if (currentUser == null) currentUser = CreateUserRecord(databaseContext, Auth0ID);

                // Check if the user is authorized for this endpoint. If not throws error.

				CheckIfAuthorized(currentUser);

                // Flip to true so the request completes!!!

				succesfullAuthentication = true;
            }
            catch (MissingAuthorizationHeader exc)
            {
				context.ModelState.AddModelError("Missing header", "You are missing the 'authorization' header.");
				context.Result = new BadRequestObjectResult(context.ModelState);
			}
            catch (MissingAuthorizationPermissions exc)
            {
				context.ModelState.AddModelError("Unauthorized", "You are not authorized for this endpoint.");
				context.Result = new UnauthorizedObjectResult(context.ModelState);
			}
            catch (InvalidAuthorizationHeader exc) {
                context.ModelState.AddModelError("Invalid authorization header", "You have an invalid authorization header.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }


			if (succesfullAuthentication) base.OnActionExecuting(context);

        }

        private User CreateUserRecord(DataContext databaseContext, string Auth0UserID)
        {

			User currentUser = new()
			{
				UserID = Auth0UserID,
				Admin = false
			};

			databaseContext.Users.Add(currentUser);
			databaseContext.SaveChanges();

            return currentUser;
		}

        private void CheckIfAuthorized(User currentUser)
        {
            if (_IsAdminEndpoint && !currentUser.Admin) { throw new MissingAuthorizationPermissions(); }
        }

        private User? GetCurrentUser(DataContext databaseContext, string Auth0UserID)
        {
            return databaseContext.Users.Find(Auth0UserID);
		}

        private string? GetCurrentUserID(ActionExecutingContext context, DataContext databaseContext)
        {
			string? token = context.HttpContext.Request.Headers["authorization"];

			if (token == null) throw new MissingAuthorizationHeader();

			JwtSecurityTokenHandler tokenHandler = new();

            string? Auth0UserID;

            try
            {
				Auth0UserID = tokenHandler.ReadJwtToken(token.Split(" ")[1]).Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
			} catch (IndexOutOfRangeException)
            {
                throw new InvalidAuthorizationHeader();
			}

            if (Auth0UserID == null) throw new MissingAuthorizationHeader();

            return Auth0UserID;
		}

    }
}
