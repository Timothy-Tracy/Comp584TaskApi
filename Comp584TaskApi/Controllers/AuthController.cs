﻿using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using DataModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Comp584TaskApi.DTO;

namespace Comp584TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<AppUser> userManager, JwtHandler handler) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(DTO.LoginRequest request)
        {
            AppUser? user = await userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return Unauthorized(new { message = "Bad Username" });
            }
            bool success = await userManager.CheckPasswordAsync(user, request.Password);
            if (!success)
            {
                return Unauthorized(new { message = "Bad Password" });
            }

            JwtSecurityToken token = await handler.GetSecurityTokenAsync(user);
            string jwtstring = new JwtSecurityTokenHandler().WriteToken(token);

            LoginResponse response = new()
            {
                success = true,
                message = "good",
                token = jwtstring
            };
            return Ok(response);


        }
    }
}
