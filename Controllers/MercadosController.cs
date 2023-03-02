using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace MercadosBackend.Controllers{

    [Route("mercados")]
    [ApiController]
    public class MercadosController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]IMercadosRepository repository)
        {
            var mercados = repository.Read();
            return Ok(mercados);
        }

        [HttpGet("{idFiltro}")]
        [EnableCors("AllowDev")]
        public IActionResult GetMercadosInfo(int idFiltro,[FromServices]IMercadosRepository repository,[FromServices]IApostasRepository repositoryApostas, [FromServices]IFiltrosRepository repositoryFiltro)
        {
            Filtro filtro = repositoryFiltro.Read(idFiltro);
            List<Apostas> apostas = repositoryApostas.ReadApostasPorData(filtro.DataInicio, filtro.DataFim);
            List<Mercados> mercados = repository.Read();
            List<ApostasInfo> listMercadosInfo = repository.ReadMercadoInfo(apostas, mercados);
            
            return Ok(listMercadosInfo);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Mercados mercados, [FromServices]IMercadosRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            repository.Create(mercados);
            return Ok();
        }
    }

}