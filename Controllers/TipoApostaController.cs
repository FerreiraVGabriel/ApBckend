using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace TipoApostaController.Controllers{

    [Route("tipoAposta")]
    [ApiController]
    public class TipoApostaController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Get([FromServices]ITipoApostaRepository repository)
        {
            var tipoAposta = repository.Read();
            return Ok(tipoAposta);
        }

        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Post([FromBody]TipoAposta tipoAposta, [FromServices]ITipoApostaRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(tipoAposta);
            return Ok();
        }
    }

}