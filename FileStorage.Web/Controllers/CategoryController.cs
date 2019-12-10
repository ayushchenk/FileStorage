using System;
using System.Threading.Tasks;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IEntityService<CategoryDTO> categoryService;

        public CategoryController(IEntityService<CategoryDTO> categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> Get()
        {
            var items = await categoryService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await categoryService.FindAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CategoryDTO value)
        {
            if (ModelState.IsValid)
            {
                await categoryService.AddAsync(value);
                await categoryService.SaveAsync();
                return Ok();
            }
            return BadRequest("Invalid request model");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] CategoryDTO value)
        {
            if (ModelState.IsValid)
            {
                await categoryService.UpdateAsync(value);
                await categoryService.SaveAsync();
                return Ok();
            }
            return BadRequest("Invalid request model");
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await categoryService.RemoveAsync(id))
            {
                await categoryService.SaveAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
