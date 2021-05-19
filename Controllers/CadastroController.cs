using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCC_API.DTO;
using TCC_API.Infrastructure.Repository;
using TCC_API.Services;

namespace TCC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : Controller
    {
        // GET: CadastroController
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return Ok(new { ok = true});
        }


        [HttpGet("isLogged")]
        [Authorize]
        public async Task<IActionResult> autenticado(
          )
        {            
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { autenticado = true });
            }
            else
            {
                return Forbid();
            }

        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromServices] UsuarioService service, [FromBody] Usuarios usuarios)
        {
            if(ModelState.IsValid)
            {
                return Ok(service.AddUsuario(usuarios));
            }
            else
            {
                return BadRequest();
            }
                      
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromServices] UsuarioService service, [FromBody] RequestLoginDTO request)
        {
            if (ModelState.IsValid)
            {
                Usuarios log = service.Login(request);
                if (log == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(log);
                log.senha = "";
                return Ok(new
                {
                    usuario = log,
                    token_ = token
                });
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
