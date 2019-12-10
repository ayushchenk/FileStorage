using System;
using System.Threading.Tasks;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class UserStorageController : Controller
    {
        private readonly IEntityService<FolderDTO> folderService;
        private readonly IEntityService<FileDTO> fileService;

        public UserStorageController(IEntityService<FolderDTO> folderService, IEntityService<FileDTO> fileService)
        {
            this.folderService = folderService;
            this.fileService = fileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Folders(Guid id)
        {
            var folders = await folderService.FindByAsync(f => f.UserId == id);
            return Ok(folders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Files(Guid id)
        {
            var files = await fileService.FindByAsync(f => f.UserId == id);
            return Ok(files);
        }
    }
}
