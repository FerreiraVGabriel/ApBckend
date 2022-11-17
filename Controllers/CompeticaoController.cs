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
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]ICompeticaoRepository repository)
        {
            var competicao = repository.Read();
            return Ok(competicao);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Competicao competicao, [FromServices]ICompeticaoRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(competicao);
            return Ok();
        }
    }

}