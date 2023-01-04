using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class Estatisticas
    {
        public double Media {get; set;}
        public double Variancia{get; set;}
        public double DesvioPadrao {get; set;}
    }
}