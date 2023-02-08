using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoGabrielAPI.Models
{
    public class RoiByDays
    {
        public DateTime Data {get; set;}
        public decimal PL {get; set;}
        public decimal ROI {get; set;}
    }
}