using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class TipoAposta
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string Nome {get; set;}

    }
}