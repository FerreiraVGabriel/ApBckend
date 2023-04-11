using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Repositories;

namespace PaisController.Controllers{

    [Route("filtros")]
    [ApiController]
    public class FiltrosController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowDev")]
        public IActionResult Get([FromServices]IFiltrosRepository repository)
        {
            var filtros = repository.Read();
            return Ok(filtros);
        }

        [HttpPost]
        [EnableCors("AllowDev")]
        public IActionResult Post([FromForm]Filtro filtro,[FromServices]IFiltrosRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            repository.Create(filtro);
            return Ok();
        }
    }

}