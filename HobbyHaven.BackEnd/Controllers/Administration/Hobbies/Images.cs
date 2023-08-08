using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HobbyHaven.BackEnd.Database.Models;
using HobbyHaven.BackEnd.Images;

using Microsoft.Extensions.Options;

namespace HobbyHaven.BackEnd.Controllers.Administration.Hobbies
{

    [ApiController]
    public class AdministratorHobbyImages : ControllerBase, IDataController
    {

        public DataContext _context { get; set; }
        public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }
        public ImageSettings _imageSettings { get; set; }
        public AdministratorHobbyImages(DataContext context, IOptions<AuthenticationLinkSettings> authSettings, IOptions<ImageSettings> imageSettings)
        {
            _context = context;
            _authenticationLinkSettings = authSettings.Value;
            _imageSettings = imageSettings.Value;
        }



        [Route("api/administration/hobbies/{hobbyID}/image/set")]
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file, Guid hobbyID)
        {

            Hobby? hobby = await _context.Hobbies.Include(h => h.PersonalityTags).Include(h => h.Users).FirstAsync(h => h.HobbyID == hobbyID);

            if (hobby == null) return NotFound();

            string filePath = $"{_imageSettings.hobbyImagePath}{hobbyID}.png";

            using (Stream stream = new FileStream(filePath, FileMode.Create)) file.CopyTo(stream);

            hobby.HasImage = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Route("api/administration/hobbies/{hobbyID}/image/remove")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid hobbyID)
        {

            Hobby? hobby = await _context.Hobbies.Include(h => h.PersonalityTags).Include(h => h.Users).FirstAsync(h => h.HobbyID == hobbyID);

            if (hobby == null) return NotFound();

            string filePath = $"{_imageSettings.hobbyImagePath}{hobbyID}.png";

            if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);

            hobby.HasImage = false;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }

}
