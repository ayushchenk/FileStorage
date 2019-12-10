﻿using System;
using System.IO;
using System.Threading.Tasks;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userManager.Users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await userManager.FindByIdAsync(id.ToString());
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await userManager.DeleteAsync(user);
                Directory.Delete(Path.Combine("Files", user.Id.ToString()), true);
                return Ok();
            }
            return NotFound();
        }
    }
}