using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class Competicao
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        
        [ForeignKey("PaisId")]
        [Required]
        public int Pais_id {get; set;}

    }
}