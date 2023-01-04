using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class EstatisticasApostasLive
    {
        public string Titulo {get; set;}
        public double MediaValoresMaior {get; set;}
        public double VarianciaValoresMaior {get; set;}
        public double DesvioPadraoValoresMaior {get; set;}

        public double MediaValoresMenores {get; set;}
        public double VarianciaValoresMenores {get; set;}
        public double DesvioPadraoValoresMenores {get; set;}

        
        public double MediaValoresMaiorVencedor {get; set;}
        public double VarianciaValoresMaiorVencedor {get; set;}
        public double DesvioPadraoValoresMaiorVencedor {get; set;}

        public double MediaValoresMenoresVencedor {get; set;}
        public double VarianciaValoresMenoresVencedor {get; set;}
        public double DesvioPadraoValoresMenoresVencedor {get; set;}

    }
}