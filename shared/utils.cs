
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Shared
{
    public class UtilsProject
    {   
        public static decimal retornaRoiStake(decimal stake, decimal pl){
        var sum  = (pl/stake)*100;
        return decimal.Round(sum,2);
        } 

        public Estatisticas calculoMediaVarianciaDesvio(List<decimal?> valores){
                Estatisticas estatisticas= new Estatisticas();
                List<int> valoresInt = valores.ConvertAll(x => (int)x);
                double media = valoresInt.Average();
                double variancia = valoresInt.Sum(d => Math.Pow(d - media, 2)/ valoresInt.Count());
                double desvioPadrao =  Math.Sqrt(variancia);
                estatisticas.DesvioPadrao = Math.Round(desvioPadrao, 2);
                estatisticas.Media = Math.Round(media, 2);
                estatisticas.Variancia = Math.Round(variancia, 2);
                return estatisticas;
        }

        public string RetornaMes(int mes){
             switch(mes)
            {
                case 1 : 
                    return "Janeiro";
                
                case 2 : 
                    return "Fevereiro";

                case 3 : 
                    return "Março";
                
                case 4 : 
                    return "Abril";

                case 5 : 
                    return "maio";
                
                case 6 : 
                    return "Junho";
                    
                case 7 : 
                    return "Julho";
                
                case 8 : 
                    return "Agosto";

                case 9 : 
                    return "Setembro";

                case 10 : 
                    return "Outubro";
                
                case 11 : 
                    return "Novembro";
                    
                case 12 : 
                    return "Dezembro";

                default:
                    return "";
                
            }
        }

        public List<T> filtroPaginacao<T>(List<T> lista, int page, int pageSize){
            return lista.GetRange((page - 1) * pageSize, page * pageSize);
        }
    }
}