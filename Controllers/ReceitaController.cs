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
    public class ReceitaController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] Receitas receitas, [FromServices] ReceitasService service)
        {
            if(ModelState.IsValid)
            {
                return Ok(service.AddReceita(receitas));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Receitas receitas, [FromServices] ReceitasService service)
        {
            if (ModelState.IsValid)
            {
                return Ok(service.UpdateReceita(receitas));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        public IActionResult GetAll([FromServices] ReceitasService service)
        {

                return Ok(service.GetAllReceitas());
            
        }

        [HttpGet("Ler/{idReceita}")]
        public IActionResult GetById(Guid idReceita, [FromServices] ReceitasService service)
        {

            return Ok(service.GetReceitaById(idReceita));

        }

        [HttpGet("Editar/{idReceita}")]
        public IActionResult GeReceitatById(Guid idReceita, [FromServices] ReceitasService service)
        {
                
            return Ok(service.GetReceitasByIdClass(idReceita));

        }

        [HttpGet("{idUsuario}")]
        public IActionResult GetAll(Guid idUsuario,[FromServices] ReceitasService service)
        {

            return Ok(service.GetMinhasReceitas(idUsuario));

        }

        [HttpDelete("{idReceita}")]
        public IActionResult Delete(Guid idReceita, [FromServices] ReceitasService service)
        {

            return Ok(service.DeleteReceitaById(idReceita));

        }
    }
}
