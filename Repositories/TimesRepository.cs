
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Shared;

namespace ProjetoGabrielAPI.Repositories
{
    public class TimesRepository : ITimesRepository
    {
        private readonly DataContext _context;
        private readonly UtilsProject _utilsProject;

        public TimesRepository(DataContext context, UtilsProject utilsProject)
        {
            _context = context;
            _utilsProject = utilsProject;
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

        public List<ApostasInfo> ReadTimesInfo(List<Apostas> apostas, List<Times> times, string ano){
            
            if(!string.IsNullOrEmpty(ano)){
                return GetTimesAno(apostas, times, ano);      
            }
            else{
                return GetTimesMes(apostas, times, ano);
            }
        }

        public ApostasInfoDetalhes GetTimesInfo(List<Apostas> listApostasFiltro, int mes, string nomeTime, string ano){

            ApostasInfoDetalhes mercadosInfoDetalhes = new ApostasInfoDetalhes();

            if(!string.IsNullOrEmpty(ano))
                mercadosInfoDetalhes.PeriodoNome = ano;
            else
                mercadosInfoDetalhes.PeriodoNome = _utilsProject.RetornaMes(mes);
                
            mercadosInfoDetalhes.LucroPerda = listApostasFiltro.Select(x=>x.PL).Sum();
            mercadosInfoDetalhes.Green = listApostasFiltro.Where(x=> x.PL > 0).Count();
            mercadosInfoDetalhes.Red = listApostasFiltro.Where(x=> x.PL < 0).Count();
            mercadosInfoDetalhes.NumApostas = listApostasFiltro.Count();

            return mercadosInfoDetalhes;
        }

        public List<ApostasInfo> GetTimesAno(List<Apostas> apostas, List<Times> times, string ano){
            List<Apostas> apostasFiltro = new List<Apostas>();
            List<ApostasInfo> listTimesInfo = new List<ApostasInfo>();

            foreach(Times time in times){
                
                ApostasInfo timesInfo = new ApostasInfo();
                timesInfo.Titulo = time.Nome;
                timesInfo.ApostasInfoDetalhes = new List<ApostasInfoDetalhes>();
                
                apostasFiltro = apostas.Where(x=>x.Mandante_id == time.Id || x.Visitante_id == time.Id).ToList();

                if(apostasFiltro.Any())
                    timesInfo.ApostasInfoDetalhes.Add(GetTimesInfo(apostasFiltro, 0, time.Nome, ano));
                
                if(timesInfo.ApostasInfoDetalhes.Any())
                    listTimesInfo.Add(timesInfo);   
            }

            return TimesMelhoresPiores(listTimesInfo);
        }

        public List<ApostasInfo> GetTimesMes(List<Apostas> apostas, List<Times> times, string ano){
            List<Apostas> apostasFiltro = new List<Apostas>();
            List<ApostasInfo> listTimesInfo = new List<ApostasInfo>();

            int totalMes = 12;

            foreach(Times time  in times){
                
                ApostasInfo timesInfo = new ApostasInfo();
                timesInfo.Titulo = time.Nome;
                timesInfo.ApostasInfoDetalhes = new List<ApostasInfoDetalhes>();

                if(!string.IsNullOrEmpty(ano)){
                    apostasFiltro = apostas.Where(x=>x.Mandante_id == time.Id || x.Visitante_id == time.Id).ToList();

                    if(apostasFiltro.Any())
                        timesInfo.ApostasInfoDetalhes.Add(GetTimesInfo(apostasFiltro, 0, time.Nome, ano));
                }
                

                for(int mes=1; mes<= totalMes; mes++ ){

                    apostasFiltro = apostas.Where(x=>(x.Mandante_id == time.Id || x.Visitante_id == time.Id) && x.DataAposta.Month == mes).ToList();

                    if(apostasFiltro.Any())
                        timesInfo.ApostasInfoDetalhes.Add(GetTimesInfo(apostasFiltro, mes, time.Nome, string.Empty));
                }

                if(timesInfo.ApostasInfoDetalhes.Any())
                    listTimesInfo.Add(timesInfo);   
            }

            return TimesMelhoresPiores(listTimesInfo);
        }

        public List<ApostasInfo> TimesMelhoresPiores(List<ApostasInfo> listTimesInfo){
            List<ApostasInfo> listTimesInfoMelhores = new List<ApostasInfo>();
            List<ApostasInfo> listTimesInfoPiores = new List<ApostasInfo>();
            List<ApostasInfo> listTimesInfoRetorno = new List<ApostasInfo>();

            listTimesInfoMelhores = listTimesInfo.OrderByDescending(x=>x.ApostasInfoDetalhes[0].LucroPerda).Take(15).ToList();
            listTimesInfoPiores = listTimesInfo.OrderBy(x=>x.ApostasInfoDetalhes[0].LucroPerda).Take(15).ToList();
            listTimesInfoRetorno.AddRange(listTimesInfoMelhores);
            listTimesInfoRetorno.AddRange(listTimesInfoPiores);

            return listTimesInfoRetorno;
        } 
    }
}

