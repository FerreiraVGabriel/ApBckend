using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class Times
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string Nome {get; set;}
        [ForeignKey("PaisId")]
        [Required]
        public int Pais_id {get; set;}

    }
}