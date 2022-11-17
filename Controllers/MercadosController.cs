using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace MercadosBackend.Controllers{

    [Route("mercados")]
    [ApiController]
    public class MercadosController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]IMercadosRepository repository)
        {
            var mercados = repository.Read();
            return Ok(mercados);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Mercados mercados, [FromServices]IMercadosRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(mercados);
            return Ok();
        }
    }

}