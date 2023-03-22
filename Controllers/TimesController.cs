using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;

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

        [HttpGet("{idFiltro}")]
        [EnableCors("AllowDev")]
        public IActionResult GetApostasInfo(int idFiltro,[FromServices]ITimesRepository repository,[FromServices]IApostasRepository repositoryApostas, [FromServices]IFiltrosRepository repositoryFiltro)
        {
            Filtro filtro = repositoryFiltro.Read(idFiltro);

            string ano ="";
            List<Times> times = new List<Times>();
            if(filtro.Mes == 0){
                times = repository.Read();
                ano = filtro.Ano.ToString();
            }
            else
                times = repository.Read();

            List<Apostas> apostas = repositoryApostas.ReadApostasPorData(filtro.DataInicio, filtro.DataFim);
            List<ApostasInfo> listTimesInfo = repository.ReadTimesInfo(apostas, times, ano);
            
            return Ok(listTimesInfo);
        }


        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Times times, [FromServices]ITimesRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(times);
            return Ok();
        }
    }

}