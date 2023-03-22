
using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Interfaces
{
    public interface IMercadosRepository
    {
        List<Mercados> Read();

        void Create (Mercados mercados);

        List<ApostasInfo> ReadMercadoInfo(List<Apostas> apostas, List<Mercados> mercados, string ano);
    }
}