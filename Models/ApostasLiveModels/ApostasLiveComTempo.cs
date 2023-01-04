using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class ApostasLiveComTempo
    {
        public string Titulo {get; set;}
        public List<ApostasLive> ApostasLive {get; set;}
        public List<EstatisticasApostasLive> EstatisticasApostasLive{get; set;}

    }
}