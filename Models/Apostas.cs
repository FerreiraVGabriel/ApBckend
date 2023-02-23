using System.Runtime.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProjetoGabrielAPI.Models
{
    public class Apostas
    {
        public int Id {get; set;}
        public DateTime DataAposta {get; set;}
        public decimal Stake {get; set;}
        public decimal PL {get; set;}
        public decimal RoiStake {get; set;}


        [ForeignKey("CompeticaoId")]
        [Required]
        public int Competicao_id {get; set;}
        public Competicao Competicao { get; set; }
        [NotMapped]
        public string CompeticaoNome { get; set; }

        [ForeignKey("MandanteId")]
        [Required]
        public int Mandante_id {get; set;}
        public Times TimeMandante { get; set; }

        [NotMapped]
        public string TimeMandanteNome { get; set; }

        [ForeignKey("VisitanteId")]
        [Required]
        public int Visitante_id {get; set;}
        public Times TimeVisitante { get; set; }

        [NotMapped]
        public string TimeVisitanteNome { get; set; }

         [ForeignKey("MercadosId")]
         [Required]
        public int Mercados_id {get; set;}
        public Mercados Mercados { get; set; }

        [NotMapped]
        public string MercadoNome { get; set; }

    }
}