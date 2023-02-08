using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

public interface IInformacoesRepository
    {
        List<Informacoes> Read();

        void Create (Informacoes informacoes);

        Informacoes Read(int mes, int ano);

        void Update (Informacoes informacoes);

        Informacoes GetInformacoesCompletaMesAno(Filtro filtro, List<Apostas> listApostas);
    }