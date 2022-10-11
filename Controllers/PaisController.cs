using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace PaisController.Controllers{

    [Route("pais")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Get([FromServices]IPaisRepository repository)
        {
            var pais = repository.Read();
            return Ok(pais);
        }

        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Post([FromBody]Pais pais, [FromServices]IPaisRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(pais);
            return Ok();
        }
    }

}