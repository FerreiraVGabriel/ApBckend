using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace CompeticaoBackend.Controllers{

    [Route("competicao")]
    [ApiController]
    public class CompeticaoController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Get([FromServices]ICompeticaoRepository repository)
        {
            var competicao = repository.Read();
            return Ok(competicao);
        }

        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Post([FromBody]Competicao competicao, [FromServices]ICompeticaoRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(competicao);
            return Ok();
        }
    }

}