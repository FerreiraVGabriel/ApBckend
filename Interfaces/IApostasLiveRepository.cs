
using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Interfaces
{
 public interface IApostasLiveRepository
    {
        List<ApostasLive> Read();

        List<ApostasLive> Read(int mercadoId);

        void Create (ApostasLive apostas);

        List<ApostasLiveComTempo> GetEstatisticasApostasLive(List<ApostasLive> apostasLive);

    }
}
   