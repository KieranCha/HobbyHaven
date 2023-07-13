using HobbyHaven.BackEnd.Controllers.Administration.PersonalityTags;
using HobbyHaven.BackEnd.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace HobbyHaven.BackEnd.Decorators.Authentication
{

    public class MissingAuthorizationHeader : Exception
    {
        public MissingAuthorizationHeader() { }
    }

    public class MissingAuthorizationPermissions : Exception
    {
        public MissingAuthorizationPermissions() { }
    }

    public class InvalidAuthorizationHeader : Exception {
        public InvalidAuthorizationHeader() { }
    }


}
