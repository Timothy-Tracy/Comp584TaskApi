using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Comp584TaskApi.DTO;



namespace Comp584TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(TaskdbContext db, IHostEnvironment environment, UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost("Users")]
        public async Task<IActionResult> ImportUsersAsync()
        {
            (string name, string email) = ("tim", "timothydtracy@gmail.com");
            AppUser user = new AppUser()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            if (await userManager.FindByEmailAsync(email) is not null) return Ok(user);
            IdentityResult dbUser = await userManager.CreateAsync(user, "Aa#12345678");
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok(user);

        }

        [HttpPost("Users/Create")]
        public async Task<IActionResult> CreateUserAsync(CreateUserDTO req)
        {
            AppUser user = new AppUser()
            {
                UserName = req.UserName,
                Email = req.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            if (await userManager.FindByEmailAsync(req.Email) is not null) return Ok(user);
            IdentityResult result = await userManager.CreateAsync(user, req.Password);
             // Check if the user creation was successful
            if (!result.Succeeded) {
                // Return error messages
                return BadRequest(result.Errors);
            }
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok(user);

        }
    }
}
