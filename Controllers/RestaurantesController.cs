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
    public class RestaurantesController : Controller
    {
        [HttpGet("Estados")]    
        [Authorize]
        public IActionResult GetEstados([FromServices] RestaurantesService service)
        {
            return Ok(service.GetUfs());
        }

        [HttpGet("Municipios")]
        [Authorize]
        public IActionResult GetMunicipios(string estado,[FromServices] RestaurantesService service)
        {
            return Ok(service.GetMunicipios(estado));
        }

        [HttpPost]
        [Authorize]
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

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] Restaurantes rs, [FromServices] RestaurantesService service)
        {
            if (ModelState.IsValid)
            {
                return Ok(service.updateRestaurante(rs));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Guid id, [FromServices] RestaurantesService service)
        {

              return Ok(service.DeleteRestauranteById(id));
           
        }

        [HttpPost("Avaliar")]
        [Authorize]
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

        [HttpGet("Meus/{idUsuario}")]
        [Authorize]
        public IActionResult GetRestaurantesByIdUsuario(Guid idUsuario, [FromServices] RestaurantesService service)
        {

            return Ok(service.GetRestaurantesByIdUsuario(idUsuario));

        }

        [HttpGet("{idUsuario}")]
        [Authorize]
        public IActionResult GetRestaurantesById(Guid idUsuario, [FromServices] RestaurantesService service)
        {

            return Ok(service.GetRestaurantesById(idUsuario));

        }

        [HttpGet("{cidade}/{idUsuario}")]
        [Authorize]
        public IActionResult GetRestaurantesByCidade(string cidade, Guid idUsuario ,[FromServices] RestaurantesService service)
        {
            
            return Ok(service.GetRestaurantesByCidade(cidade,idUsuario));
            
        }
    }
}
