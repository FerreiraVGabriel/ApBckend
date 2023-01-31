
using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Interfaces
{
 public interface IFiltrosRepository
    {
        List<Filtro> Read();

        Filtro Read(int idFiltro);

        void Create (Filtro filtro);
    }
}
   