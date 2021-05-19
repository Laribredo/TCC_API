using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TCC_API.Infrastructure.Repository;
using TCC_API.Services;

namespace TCC_API.Controllers
{
    [Route("api/[controller]")]
    public class PublicacaoController : Controller
    {
        [HttpPost]
        [Authorize]
        public ActionResult post([FromBody] Noticias noticia, [FromServices] PublicacaoService service)
        {
            if (ModelState.IsValid)
            {
                return Ok(service.AddPublicacao(noticia));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize]
        public ActionResult Update([FromBody] Noticias noticia, [FromServices] PublicacaoService service)
        {
            if (ModelState.IsValid)
            {
                return Ok(service.UpdatePublicacao(noticia));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult Get(string id, [FromServices] PublicacaoService service)
        {

           return Ok(service.GetPublicaoById(id));
          
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAll( [FromServices] PublicacaoService service)
        {

            return Ok(service.GetAllNoticias());

        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeletePublicacao(string id, [FromServices] PublicacaoService service)
        {
            return Ok(service.DeletePublicacaoById(id));
        }

        [HttpGet("Recentes/{usuario}")]
        [Authorize]
        public ActionResult GetRecentes(string usuario, [FromServices] PublicacaoService service)
        {
           return Ok(service.GetPublicacoesRecentesByUsuario(usuario));            
        }

        [HttpGet("Cadastro/{usuario}")]
        [Authorize]
        public ActionResult GetPublicacoesByUser(string usuario, [FromServices] PublicacaoService service)
        {
            return Ok(service.GetAllPublicacoesByUser(usuario));
        }

    }
}
