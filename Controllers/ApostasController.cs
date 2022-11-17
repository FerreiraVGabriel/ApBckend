using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace ApostasBackend.Controllers{

    [Route("apostas")]
    [ApiController]
    public class ApostasController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]IApostasRepository repository)
        {
            var apostas = repository.Read();
            return Ok(apostas);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Apostas apostas, [FromServices]IApostasRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(apostas);
            return Ok();
        }
    }

}