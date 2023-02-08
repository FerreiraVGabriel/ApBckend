using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class Informacoes
    {
        public int Id {get; set;}
        public int mes {get; set;}
        public int ano {get; set;}
        public decimal inicio {get; set;}
        public decimal pl {get; set;}
        public decimal final {get; set;}
        public decimal roi {get; set;}
        public decimal saque {get; set;}
        public decimal aporte {get; set;}

    }
}