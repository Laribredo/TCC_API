using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCC_API.Infrastructure.Repository;
using TCC_API.Services;

namespace TCC_API.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantesController : Controller
    {
        [HttpGet("Estados")]    
        public IActionResult GetEstados([FromServices] RestaurantesService service)
        {
            return Ok(service.GetUfs());
        }

        [HttpGet("Municipios")]
        public IActionResult GetMunicipios(string estado,[FromServices] RestaurantesService service)
        {
            return Ok(service.GetMunicipios(estado));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Restaurantes rs, [FromServices] RestaurantesService service)
        {
            if(ModelState.IsValid)
            {
                return Ok(service.cadastrarRestaurante(rs));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("Avaliar")]
        public IActionResult Post([FromBody] AvaliacoesRestaurantes rs, [FromServices] AvaliacaoService service)
        {
            if (ModelState.IsValid)
            {
                return Ok(service.AvaliarRestaurante(rs));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{cidade}/{idUsuario}")]
        public IActionResult GetRestaurantesByCidade(string cidade, Guid idUsuario ,[FromServices] RestaurantesService service)
        {
            
            return Ok(service.GetRestaurantesByCidade(cidade,idUsuario));
            
        }
    }
}
