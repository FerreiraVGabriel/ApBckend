using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;
using ProjetoGabrielAPI.Shared;

namespace TelaPrincipalBackend.Controllers{

    [Route("telaPrincipal")]
    [ApiController]
    public class TelaPrincipalController : ControllerBase
    {
        [HttpGet("{idFiltro}")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Get(int idFiltro,[FromServices]ITelaPrincipalRepository repository,[FromServices]IApostasRepository repositoryApostas,
         [FromServices]IFiltrosRepository repositoryFiltro, [FromServices]IInformacoesRepository repositoryInformacoes)
        {
            Filtro filtro = repositoryFiltro.Read(idFiltro);

            InfoTelaPrincipal infoTelaPrincipal = new InfoTelaPrincipal();
            infoTelaPrincipal.ListApostas = repositoryApostas.Read().Where(x=>x.DataAposta>= filtro.DataInicio && x.DataAposta<= filtro.DataFim).ToList();
            infoTelaPrincipal.Informacoes = repositoryInformacoes.GetInformacoesCompletaMesAno(filtro, infoTelaPrincipal.ListApostas);
            infoTelaPrincipal.ListRoiByDays = repository.GetROIByDay(infoTelaPrincipal.ListApostas, filtro.Mes, filtro.Ano, infoTelaPrincipal.Informacoes.inicio);

            return Ok(infoTelaPrincipal);
        }
    }

}