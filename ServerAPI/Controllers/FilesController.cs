using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        string rootDir = "";

        public FilesController(IWebHostEnvironment _appEnvironment)
        { 
            rootDir = Path.Combine(_appEnvironment.ContentRootPath, "wwwroot/Files");
        }

        [HttpGet]
        public async Task<ActionResult> DownloadFile()
        {
            var filePath = $"/file.txt";
            rootDir += filePath;
            var bytes = await System.IO.File.ReadAllBytesAsync(rootDir);
            return File(bytes, "text/plain", Path.GetFileName(rootDir));
        }
    }
}
