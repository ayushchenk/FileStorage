using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.UnitOfWorks;
using FileStorage.Web.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class FolderController : ControllerBase
    {
        private readonly IEntityService<FolderDTO> folderService;
        private readonly IOService ioService;

        public FolderController(IEntityService<FolderDTO> folderService)
        {
            this.folderService = folderService;
            this.ioService = new IOService();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await folderService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("stream/{id}")]
        public async Task<IActionResult> GetStream(Guid id)
        {
            var item = await folderService.FindAsync(id);
            if (item == null)
                return NotFound();
            return File(ioService.DownloadFolder(item), "application/zip");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await folderService.FindAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FolderDTO value)
        {
            if (ModelState.IsValid)
            {
                var parent = await folderService.FindAsync(value.ParentFolderId ?? Guid.Empty);
                ioService.CreateFolder(value, parent);
                var id = await folderService.AddAsync(value);
                await folderService.SaveAsync();
                return Ok(id);
            }
            return BadRequest("Invalid request model");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FolderDTO value)
        {
            if (ModelState.IsValid)
            {
                if(!Request.Headers.TryGetValue("move", out var moveHeader))
                    return BadRequest();
                var folder = await folderService.FindAsync(value.Id);
                bool move = bool.Parse(moveHeader.First());
                if (move)
                {
                    var parent = await folderService.FindAsync(value.ParentFolderId ?? Guid.Empty);
                    value.ParentPath = folder.ParentPath;
                    ioService.MoveFolder(value, parent);
                }
                else
                {
                    ioService.RenameFolder(value, folder);
                }
                await folderService.UpdateAsync(value);
                await folderService.SaveAsync();
                return Ok(value);
            }
            return BadRequest("Invalid request model");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var folder = await folderService.FindAsync(id);
            if (folder != null && await folderService.RemoveAsync(id))
            {
                Directory.Delete(folder.FullPath, true);
                await folderService.SaveAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
