using System;
using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Interfaces
{
    public interface IApostasRepository
    {
        List<Apostas> Read();

        List<Apostas> ReadApostasPorData(DateTime dataInicio, DateTime dataFim);

        void Create (Apostas apostas);

        void Update(int id, Apostas apostas);
        
        List<Apostas> FiltroAposta(List<Apostas> apostas, string page, string pageSize);
    }
}