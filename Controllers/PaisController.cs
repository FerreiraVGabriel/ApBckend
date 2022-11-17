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
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]IPaisRepository repository)
        {
            var pais = repository.Read();
            return Ok(pais);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Pais pais,[FromServices]IPaisRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            repository.Create(pais);
            return Ok();
        }
    }

}