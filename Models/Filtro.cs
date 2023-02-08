using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoGabrielAPI.Models
{
    public class Filtro
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public String Nome {get; set;}
        [Required]
        public DateTime DataInicio {get; set;}
        [Required]
        public DateTime DataFim {get; set;}
        [Required]
        public int Mes {get; set;}
        [Required]
        public int Ano {get; set;}

    }
}