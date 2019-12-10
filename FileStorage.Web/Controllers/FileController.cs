using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class FileController : ControllerBase
    {
        private readonly IEntityService<FileDTO> fileService;
        private readonly IEntityService<FolderDTO> folderService;
        private readonly IOService ioService;

        public FileController(IEntityService<FileDTO> fileService, IEntityService<FolderDTO> folderService)
        {
            this.fileService = fileService;
            this.folderService = folderService;
            this.ioService = new IOService();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await fileService.FindAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpGet("stream/{id}")]
        public async Task<IActionResult> GetStream(Guid id)
        {
            var item = await fileService.FindAsync(id);
            if (item == null)
                return NotFound();
            Stream stream = new FileStream(path: item.Path, mode: FileMode.Open);
            return File(stream, "application/octet-stream");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileDTO value)
        {
            if (ModelState.IsValid)
            {
                if (value.File.Length > 0)
                {
                    var folder = await folderService.FindAsync(value.FolderId ?? Guid.Empty);
                    ioService.SaveFile(value, folder);
                    await fileService.AddAsync(value);
                    await fileService.SaveAsync();
                    return Ok(value.Id);
                }
                else
                {
                    return BadRequest("No file");
                }
            }
            return BadRequest("Invalid request model");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FileDTO value)
        {
            if (ModelState.IsValid)
            {
                if (!Request.Headers.TryGetValue("move", out var moveHeader))
                    return BadRequest();
                bool move = bool.Parse(moveHeader.First());
                var file = await fileService.FindAsync(value.Id);
                var parent = await folderService.FindAsync(value.FolderId ?? Guid.Empty);
                if (move)
                {
                    file.FolderId = value.FolderId;
                    ioService.MoveFile(file, parent);
                }
                else
                {
                    ioService.RenameFile(value, file);
                }
                await fileService.UpdateAsync(value);
                await fileService.SaveAsync();
                return Ok(value);
            }
            return BadRequest("Ivalid request modell");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var file = await fileService.FindAsync(id);
            if (file != null)
            {
                ioService.DeleteFile(file);
                await fileService.RemoveAsync(id);
                await fileService.SaveAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
