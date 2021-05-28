using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC_API.Infrastructure.Repository;
using TCC_API.Services;

namespace TCC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : Controller
    {
        [HttpPost("Noticias")]
        [Authorize]
        public IActionResult Index([FromBody] ComentariosNoticias comentarios, [FromServices] ComentariosService service )
        {
            if(ModelState.IsValid)
            {
                return Ok( new { postado = service.ComentarNoticias(comentarios) });
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return View();
        }

        [HttpGet("Noticias/{idPublicacao}")]
        [Authorize]
        public IActionResult Get(Guid idPublicacao, [FromServices] ComentariosService service)
        {

            return Ok(service.GetComentarios(idPublicacao));
           
            //return View();
        }

        [HttpPost("Receitas")]
        [Authorize]
        public IActionResult Index([FromBody] ComentariosReceitas comentarios, [FromServices] ComentariosService service)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { postado = service.ComentarReceitas(comentarios) });
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return View();
        }

        [HttpGet("Receitas/{idPublicacao}")]
        [Authorize]
        public IActionResult GetReceita(Guid idPublicacao, [FromServices] ComentariosService service)
        {

            return Ok(service.GetComentariosReceitas(idPublicacao));

            //return View();
        }
    }
}
