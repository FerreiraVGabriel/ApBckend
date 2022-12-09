using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;
using ProjetoGabrielAPI.Shared;

namespace ApostasLiveBackend.Controllers{

    [Route("apostasLive")]
    [ApiController]
    public class ApostasLiveController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]IApostasLiveRepository repository)
        {
            var apostasLive = repository.Read();
            return Ok(apostasLive);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]ApostasLive apostasLive, [FromServices]IApostasLiveRepository repository,[FromServices] UtilsProject utilsProject)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            apostasLive.RoiStake = utilsProject.percentageReturns(apostasLive.Stake, apostasLive.PL);
            
            repository.Create(apostasLive);
            return Ok();
        }
    }

}