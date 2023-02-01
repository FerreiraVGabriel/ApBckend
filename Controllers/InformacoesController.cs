using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace InformacoesController.Controllers{

    [Route("informacoes")]
    [ApiController]
    public class InformacoesController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]IInformacoesRepository repository)
        {
            var informacoes = repository.Read();
            return Ok(informacoes);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Informacoes informacoes,[FromServices]IInformacoesRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            repository.Create(informacoes);
            return Ok();
        }
    }

}