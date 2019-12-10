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
    [Route("api/[controller]")]
    public class PrivateFileController : ControllerBase
    {
        private readonly IEntityService<FileDTO> fileService;

        public PrivateFileController(IEntityService<FileDTO> fileService)
        {
            this.fileService = fileService;
        }

        public async Task<IActionResult> Get(Guid id)
        {
            var items = await fileService.FindByAsync(file => file.FileAccessibility == DAL.Model.FileAccessibility.Private && file.UserId == id);
            return Ok(items);
        }
    }
}
