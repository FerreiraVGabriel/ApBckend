using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class Mercados
    {
        public int Id {get; set;}
        [Required]
        public String Nome {get; set;}

    }
}