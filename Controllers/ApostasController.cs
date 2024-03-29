using System.Linq;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Shared;
using ProjetoGabrielAPI.Interfaces;


namespace ApostasBackend.Controllers
{

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

        [HttpGet("{search}/{page}")]
        [EnableCors("AllowDev")]
        public IActionResult GetApostasPaginacao(string search,string page, [FromServices]IApostasRepository repository){
            if(search != "null")
                return null;

            string pageSize = "10";
            var apostas = repository.Read().OrderByDescending(x=>x.DataAposta).ToList();
            apostas = repository.FiltroAposta(apostas,page, pageSize);
            return Ok(apostas);
        }

        // [HttpGet("{idFiltro}/{idMercados}/{tipoFiltro}")]
        // [EnableCors("AllowDev")]
        // public IActionResult GetApostasPorData(int idFiltro, string idMercados, string tipoFiltro, [FromServices]IApostasRepository repository, [FromServices]IFiltrosRepository repositoryFiltro)
        // {
        //     List<int> listidsMercados = new List<int>();
        //     if(!string.IsNullOrEmpty(idMercados) && idMercados != "null")
        //         listidsMercados = idMercados.Split(',').Select(Int32.Parse).ToList();


        //     Filtro filtro = repositoryFiltro.Read(idFiltro);
        //     List<Apostas> apostas = new List<Apostas>();

        //     if(tipoFiltro == "Somente essa aposta")
        //        apostas =  repository.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim &&
        //                     listidsMercados.Contains(x.Mercados_id)).ToList();

        //     else if(tipoFiltro == "Exclui Aposta(s)")
        //         apostas =  repository.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim &&
        //                     !listidsMercados.Contains(x.Mercados_id)).ToList();

        //     else{
        //         apostas =  repository.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim).ToList();
        //     }

        //     return Ok(apostas);
        // }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Apostas aposta, [FromServices]IApostasRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();


            aposta.DataAposta = Convert.ToDateTime(aposta.DataApostaString);
            aposta.RoiStake = UtilsProject.retornaRoiStake(aposta.Stake, aposta.PL);

            repository.Create(aposta);
            return Ok();
        }

        [HttpGet("{idAposta}")]
        [EnableCors("AllowDev")]
        public IActionResult Get(int idAposta, [FromServices]IApostasRepository repository)
        {
            var aposta = repository.Read().Where(x=>x.Id == idAposta).First();
            aposta.DataApostaString = aposta.DataAposta.ToString();
            return Ok(aposta);
        }

        [HttpPost("{id}")]
        [EnableCors("AllowDev")]
        public IActionResult Update(int id,[FromForm]Apostas aposta,[FromServices]IApostasRepository repository)
        {
            repository.Update(id,aposta);
            return Ok();
        }
    }

}