using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class TempoJogo
    {
        public string Titulo {get; set;}
        public int? TempoInicial {get; set;}
        public int? TempoFinal {get; set;}
    }
}