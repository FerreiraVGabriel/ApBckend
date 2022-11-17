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
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]ITipoApostaRepository repository)
        {
            var tipoAposta = repository.Read();
            return Ok(tipoAposta);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]TipoAposta tipoAposta, [FromServices]ITipoApostaRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(tipoAposta);
            return Ok();
        }
    }

}