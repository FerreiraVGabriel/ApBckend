using System.Runtime.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class Apostas
    {
        public int Id {get; set;}
        public DateTime DataAposta {get; set;}
        public decimal Stake {get; set;}
        public decimal PL {get; set;}
        public decimal RoiStake {get; set;}
        public bool AnalisePre {get; set;}
        public bool EntradaPre {get; set;}

        [ForeignKey("CompeticaoId")]
        [Required]
        public int Competicao_id {get; set;}

        [ForeignKey("MandanteId")]
        [Required]
        public int Mandante_id {get; set;}

        [ForeignKey("VisitanteId")]
        [Required]
        public int Visitante_id {get; set;}

        [ForeignKey("MercadosId")]
        [Required]
        public int Mercados_id {get; set;}
    }
}