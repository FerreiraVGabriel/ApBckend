using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class ApostasInfoDetalhes
    {
        public string PeriodoNome {get; set;}
        public decimal LucroPerda {get; set;}
        public int Green {get; set;}
        public int Red {get; set;}
        public int NumApostas {get; set;}
    }
}