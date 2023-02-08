
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Shared;

namespace ProjetoGabrielAPI.Repositories
{
    public class TelaPrincipalRepository : ITelaPrincipalRepository
    {
         private readonly DataContext _context;

        public TelaPrincipalRepository(DataContext context)
        {
            _context = context;
        }

        public InfoTelaPrincipal Read()
        {
            return null;
        }

        public List<RoiByDays> GetROIByDay (List<Apostas> listApostas , int mes, int ano, decimal inicio){
            List<RoiByDays> listRoiByDays = new List<RoiByDays>();
            int dias = System.DateTime.DaysInMonth(ano, mes);
            for(int cont=1; cont<= dias; cont++ ){
                RoiByDays roiByDays = new RoiByDays();
                List<Apostas> listApostasPorDia = listApostas.Where(x=>x.DataAposta.Day == cont).ToList();
                if(listApostasPorDia.Any()){
                    roiByDays.Data = listApostasPorDia.First().DataAposta;
                    roiByDays.PL = listApostasPorDia.Sum(x => x.PL);
                    roiByDays.ROI = UtilsProject.retornaRoiStake(inicio, roiByDays.PL);
                    listRoiByDays.Add(roiByDays);
                }
            }

            return listRoiByDays;
        }
    }
}

