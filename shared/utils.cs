
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Shared
{
    public class UtilsProject
    {   
        public decimal retornaPorcentagem(decimal stake, decimal pl){
        var sum  = (pl/stake)*100;
        return decimal.Round(sum,2);
        } 

        public Estatisticas calculoMediaVarianciaDesvio(List<decimal?> valores){
                Estatisticas estatisticas= new Estatisticas();
                List<int> valoresInt = valores.ConvertAll(x => (int)x);
                double media = valoresInt.Average();
                double variancia = valoresInt.Sum(d => Math.Pow(d - media, 2)/ valoresInt.Count());
                double desvioPadrao =  Math.Sqrt(variancia);
                estatisticas.DesvioPadrao = Math.Round(desvioPadrao, 2);
                estatisticas.Media = Math.Round(media, 2);
                estatisticas.Variancia = Math.Round(variancia, 2);
                return estatisticas;
        }
    }
}