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
    }
}