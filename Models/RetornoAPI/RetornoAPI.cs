using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoGabrielAPI.Models
{
    public class RetornoAPI<T>
    {
        public T[] items {get; set;}
        public bool hasNext {get; set;}
    }
}