using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using System.Collections.Generic;
using System;

namespace TelaPrincipalBackend.Controllers{

    [Route("telaPrincipal")]
    [ApiController]
    public class TelaPrincipalController : ControllerBase
    {
        [HttpGet("{idFiltro}/{idMercados}/{tipoFiltro}")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Get(int idFiltro, string idMercados, string tipoFiltro,[FromServices]ITelaPrincipalRepository repository,[FromServices]IApostasRepository repositoryApostas,
         [FromServices]IFiltrosRepository repositoryFiltro, [FromServices]IInformacoesRepository repositoryInformacoes)
        {
            List<int> listidsMercados = new List<int>();
            if(!string.IsNullOrEmpty(idMercados) && idMercados != "null")
                listidsMercados = idMercados.Split(',').Select(Int32.Parse).ToList();

            Filtro filtro = repositoryFiltro.Read(idFiltro);
            InfoTelaPrincipal infoTelaPrincipal = new InfoTelaPrincipal();

            if(tipoFiltro == "Somente essa aposta"){
                infoTelaPrincipal.ListApostas = repositoryApostas.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim &&
                            listidsMercados.Contains(x.Mercados_id)).ToList();
            }
            else if(tipoFiltro == "Exclui Aposta(s)"){
                infoTelaPrincipal.ListApostas = repositoryApostas.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim &&
                            !listidsMercados.Contains(x.Mercados_id)).ToList();
            }
            else{
                infoTelaPrincipal.ListApostas = repositoryApostas.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim).ToList();
            }

            infoTelaPrincipal.Informacoes = repositoryInformacoes.GetInformacoesCompletaMesAno(filtro, infoTelaPrincipal.ListApostas);
            infoTelaPrincipal.ListRoiByDays = repository.GetROIByDay(infoTelaPrincipal.ListApostas, filtro.Mes, filtro.Ano, infoTelaPrincipal.Informacoes.inicio);

            return Ok(infoTelaPrincipal);
        }
    }

}