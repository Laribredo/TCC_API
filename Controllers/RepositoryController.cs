using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCC_API.Services;

namespace TCC_API.Controllers
{
    [Route("api/[controller]")]
    public class RepositoryController : Controller
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UploadFileAsync(IFormFile file, [FromServices] RepositoryService service)
        {
            if(ModelState.IsValid)
            {
                var res = await service.UploadImagens(file);
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
            
        }
    }
}
