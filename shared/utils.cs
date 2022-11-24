
namespace ProjetoGabrielAPI.Shared
{
    public class UtilsProject
    {   
        public decimal percentageReturns(decimal stake, decimal pl){
        var sum  = (pl/stake)*100;
        return decimal.Round(sum,2);
        } 
    }
}