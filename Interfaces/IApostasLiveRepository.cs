
using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Interfaces
{
 public interface IApostasLiveRepository
    {
        List<ApostasLive> Read();

        void Create (ApostasLive apostas);
    }
}
   