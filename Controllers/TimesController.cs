using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace TimesBackend.Controllers{

    [Route("times")]
    [ApiController]
    public class TimesController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Get([FromServices]ITimesRepository repository)
        {
            var times = repository.Read();
            return Ok(times);
        }

        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Post([FromBody]Times times, [FromServices]ITimesRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(times);
            return Ok();
        }
    }

}