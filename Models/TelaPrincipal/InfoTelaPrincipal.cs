using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoGabrielAPI.Models
{
    public class InfoTelaPrincipal
    {
        public Informacoes Informacoes {get; set;}
        public List<Apostas> ListApostas {get; set;}
        public List<RoiByDays> ListRoiByDays{get; set;}
    }
}