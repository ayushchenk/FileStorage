using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.Model;
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

        [AllowAnonymous]
        [HttpGet("public")]
        public async Task<IActionResult> GetPublic()
        {
            return Ok(await fileService.FindByAsync(file => file.FileAccessibility == FileAccessibility.Public));
        }

        [AllowAnonymous]
        [HttpGet("search/{category}/{key}")]
        public async Task<IActionResult> Search(string category, string key)
        {
            key = key.ToLower();
            var query = await fileService.FindByAsync(file => file.FileAccessibility == FileAccessibility.Public
                && file.CategoryId.ToString() == category);
            if (key != "undefined")
                query = query.Where(file => file.FileName.ToLower().Contains(key));
            return Ok(query);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await fileService.FindAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [AllowAnonymous]
        [HttpGet("{id}/url")]
        public async Task<IActionResult> GetUrl(Guid id)
        {
            string url = "https://tinyurl.com/api-create.php?url=https://localhost:44304/d/" + id;
            HttpClient client = new HttpClient();
            return Ok(await client.GetStringAsync(url));
        }

        [AllowAnonymous]
        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var item = await fileService.FindAsync(id);
            if (item == null)
                return NotFound();
            if (item.FileAccessibility == FileAccessibility.Private)
                return BadRequest("Private file");
            Stream stream = new FileStream(path: item.Path, mode: FileMode.Open);
            HttpContext.Response.Headers.Add("filename", item.FileName);
            return File(stream, "application/octet-stream");
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

        [HttpPost("stream")]
        public IActionResult GetStream([FromBody] Guid[] ids)
        {
            string path = @$"Files\tempo\{Guid.NewGuid().ToString()}.zip";
            using (var zip = ZipFile.Open(path, ZipArchiveMode.Create))
            {
                foreach (var id in ids)
                {
                    var item = fileService.Find(id);
                    if (item != null)
                        zip.CreateEntryFromFile(item.Path, item.FileName, CompressionLevel.Optimal);
                }
            }
            return File(new FileStream(path: path, mode: FileMode.Open), "application/octet-stream");
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
                if (move)
                {
                    var parent = await folderService.FindAsync(value.FolderId ?? Guid.Empty);
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

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var file = await fileService.FindAsync(id);
                if (file != null)
                {
                    ioService.DeleteFile(file);
                    await fileService.RemoveAsync(id);
                }
            }
            await fileService.SaveAsync();
            return Ok();
        }
    }
}
