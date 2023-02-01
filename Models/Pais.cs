using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoGabrielAPI.Models
{
    public class Pais
    {
        [Key]
        public int id {get; set;}
        [Required]
        public String Nome {get; set;}
    }
}