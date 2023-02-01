using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

public interface IInformacoesRepository
    {
        List<Informacoes> Read();

        void Create (Informacoes informacoes);
    }