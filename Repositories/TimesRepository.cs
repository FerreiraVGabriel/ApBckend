
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public class TimesRepository : ITimesRepository
    {
        private readonly DataContext _context;

        public TimesRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Times times)
        {
            _context.Add(times);
            _context.SaveChanges();
            
        }

        public List<Times> Read()
        {
            return _context.Times.Include(x=>x.Pais).OrderBy(teams=>teams.Nome).ToList();
            
        }

         public List<ApostasInfo> ReadTimesInfo(List<Apostas> apostas, List<Times> times){
            List<Apostas> apostasFiltro = new List<Apostas>();
            List<ApostasInfo> listTimesInfo = new List<ApostasInfo>();

            int totalMes = 12;

            for(int mes=0; mes<= totalMes; mes++ ){
                foreach(Times time  in times){
                    if(mes == 0){
                        apostasFiltro = apostas.Where(x=>x.Mandante_id == time.Id || x.Visitante_id == time.Id).ToList();

                        if(apostasFiltro.Any())
                            listTimesInfo.Add(GetTimesInfo(apostasFiltro, mes, time.Nome));
                    }
                    else{
                        apostasFiltro = apostas.Where(x=>(x.Mandante_id == time.Id || x.Visitante_id == time.Id) && x.DataAposta.Month == mes).ToList();

                        if(apostasFiltro.Any())
                            listTimesInfo.Add(GetTimesInfo(apostasFiltro, mes, time.Nome));
                    }
                }
            }

            return listTimesInfo.OrderBy(x=>x.Titulo).ToList();
        }

         public ApostasInfo GetTimesInfo(List<Apostas> listApostasFiltro, int mes, string nomeTime){

            ApostasInfo mercadosInfo = new ApostasInfo();

            mercadosInfo.Titulo = nomeTime;
            mercadosInfo.PeriodoNome = mes.ToString();
            mercadosInfo.LucroPerda = listApostasFiltro.Select(x=>x.PL).Sum();
            mercadosInfo.Green = listApostasFiltro.Where(x=> x.PL >= 0).Count();
            mercadosInfo.Red = listApostasFiltro.Where(x=> x.PL < 0).Count();
            mercadosInfo.NumApostas = listApostasFiltro.Count();

            return mercadosInfo;
        }
    }
}

