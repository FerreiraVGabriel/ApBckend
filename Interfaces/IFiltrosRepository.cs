
using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Interfaces
{
 public interface IFiltrosRepository
    {
        List<Filtro> Read();

        void Create (Filtro filtro);
    }
}
   