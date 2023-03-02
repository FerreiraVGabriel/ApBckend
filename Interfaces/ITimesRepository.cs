using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Interfaces
{
       public interface ITimesRepository
    {
        List<Times> Read();

        void Create (Times competicao);

        List<ApostasInfo> ReadTimesInfo(List<Apostas> apostas, List<Times> times);
    }
}