using Microsoft.VisualBasic.CompilerServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;
using ProjetoGabrielAPI.Shared;

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
        public IActionResult Post([FromForm]Apostas aposta, [FromServices]IApostasRepository repository,[FromServices] UtilsProject utilsProject)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            aposta.RoiStake = utilsProject.retornaPorcentagem(aposta.Stake, aposta.PL);

            repository.Create(aposta);
            return Ok();
        }
    }

}