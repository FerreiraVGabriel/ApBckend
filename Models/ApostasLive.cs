using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class ApostasLive
    {
        public int Id {get; set;}
        public DateTime DataAposta {get; set;}

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
        public decimal Stake {get; set;}
        //vendo o jogo ou acompanhando em tempo real
        [ForeignKey("TipoApostaId")]
        [Required]
        public int TipoAposta_id {get; set;}
        public decimal BackCasa {get; set;}
        public decimal BackVisitante {get; set;}
        public decimal BackEmpate {get; set;}
        public decimal Over05HT {get; set;}
        public decimal OverAFrenteHT {get; set;}
        public decimal OverLimite2T {get; set;}
        public decimal OverAFrente2T {get; set;}
        public decimal MH1Casa {get; set;}
        public decimal? MH1Visitante {get; set;}
        public decimal? MH2Casa {get; set;}
        public decimal? MH2Visitante {get; set;}
        public decimal? MH3Casa {get; set;}
        public decimal? MH3Visitante {get; set;}
        public decimal? EXGCasa {get; set;}
        public decimal? EXGVisitante {get; set;}
        public decimal? APM1Casa {get; set;}
        public decimal? APM1Visitante {get; set;}
        public decimal? APM2Casa {get; set;}
        public decimal? APM2Visitante {get; set;}
        public int? CACasa {get; set;}
        public int? CAVisitante {get; set;}
        public int? CFACasa {get; set;}
        public int? CFAVisitante {get; set;}
        public decimal? PL {get; set;}
        public decimal? RoiStake {get; set;}

        //Aposta principal
        public Boolean Principal {get; set;}

    }
}