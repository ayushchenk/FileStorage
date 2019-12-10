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
    public class PublicFileController : ControllerBase
    {
        private readonly IEntityService<FileDTO> fileService;

        public PublicFileController(IEntityService<FileDTO> fileService)
        {
            this.fileService = fileService;
        }

        public async Task<IActionResult> Get()
        {
            var items = await fileService.FindByAsync(file => file.FileAccessibility == DAL.Model.FileAccessibility.Public);
            return Ok(items);
        }
    }
}
