using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGabrielAPI.Models
{
    public class ApostasInfo
    {
        public String Titulo {get; set;}
        public List<ApostasInfoDetalhes> ApostasInfoDetalhes {get; set;}
    }
}