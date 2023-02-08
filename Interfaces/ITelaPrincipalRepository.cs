using System.Collections.Generic;
using ProjetoGabrielAPI.Models;

public interface ITelaPrincipalRepository
    {
        InfoTelaPrincipal Read();

        List<RoiByDays> GetROIByDay (List<Apostas> listApostas , int mes, int ano, decimal inicio);
    }