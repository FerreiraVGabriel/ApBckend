using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace ApostasLiveBackend.Controllers{

    [Route("apostasLive")]
    [ApiController]
    public class ApostasLiveController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Get([FromServices]IApostasLiveRepository repository)
        {
            var apostasLive = repository.Read();
            return Ok(apostasLive);
        }

        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Post([FromBody]ApostasLive apostasLive, [FromServices]IApostasLiveRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(apostasLive);
            return Ok();
        }
    }

}