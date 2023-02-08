using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Post([FromForm]ApostasLive apostasLive, [FromServices]IApostasLiveRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            apostasLive.RoiStake = UtilsProject.retornaRoiStake(apostasLive.Stake, apostasLive.PL);
            
            repository.Create(apostasLive);
            return Ok();
        }

        [HttpGet("{idMercado}")]
        [EnableCors("AllowDev")]
        public IActionResult GetApostasLive(int idMercado,[FromServices]IApostasLiveRepository repository)
        {
            List<ApostasLive> apostasLive = repository.Read(idMercado).ToList();

            List<ApostasLiveComTempo> returnApostasLiveComTempo = repository.GetEstatisticasApostasLive(apostasLive);

            return Ok(returnApostasLiveComTempo);
        }
    }

}