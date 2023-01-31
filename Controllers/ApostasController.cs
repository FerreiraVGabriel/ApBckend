using System.Linq;
using System;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;
using ProjetoGabrielAPI.Shared;
using ProjetoGabrielAPI.Interfaces;

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

        [HttpGet("{idFiltro}")]
        [EnableCors("AllowDev")]
        public IActionResult GetApostasPorData(int idFiltro, [FromServices]IApostasRepository repository, [FromServices]IFiltrosRepository repositoryFiltro)
        {
            Filtro filtro = repositoryFiltro.Read(idFiltro);
            var apostas = repository.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim);
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