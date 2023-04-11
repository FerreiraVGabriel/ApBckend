
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Shared;

namespace ProjetoGabrielAPI.Repositories
{
    public class MercadosRepository : IMercadosRepository
    {
        private readonly DataContext _context;

        private readonly UtilsProject _utilsProject;

        public MercadosRepository(DataContext context, UtilsProject utilsProject)
        {
            _context = context;
            _utilsProject = utilsProject;
        }

        public void Create(Mercados mercados)
        {
            _context.Add(mercados);
            _context.SaveChanges();
            
        }

        public List<Mercados> Read()
        {
            return _context.Mercados.ToList();
            
        }

        public List<ApostasInfo> ReadMercadoInfo(List<Apostas> apostas, List<Mercados> mercados, string ano){
            List<Apostas> apostasFiltro = new List<Apostas>();
            List<ApostasInfo> listMercadosInfo = new List<ApostasInfo>();

            int totalMes = 12;

            
            foreach(Mercados mercado in mercados){
                
                ApostasInfo mercadosInfo = new ApostasInfo();
                mercadosInfo.Titulo = mercado.Nome;
                mercadosInfo.ApostasInfoDetalhes = new List<ApostasInfoDetalhes>();

                for(int mes=0; mes<= totalMes; mes++ ){

                    if(mes == 0 && !string.IsNullOrEmpty(ano)){
                        apostasFiltro = apostas.Where(x=>x.Mercados_id == mercado.Id).ToList();

                        if(apostasFiltro.Any())
                            mercadosInfo.ApostasInfoDetalhes.Add(GetMercadosInfo(apostasFiltro, mes, ano));
                    }
                    else{
                        apostasFiltro = apostas.Where(x=>x.Mercados_id == mercado.Id && x.DataAposta.Month == mes).ToList();

                        if(apostasFiltro.Any())
                            mercadosInfo.ApostasInfoDetalhes.Add(GetMercadosInfo(apostasFiltro, mes, string.Empty));
                    }
                }  
                if(mercadosInfo.ApostasInfoDetalhes.Any())
                    listMercadosInfo.Add(mercadosInfo); 
            }
            return listMercadosInfo.OrderBy(x=>x.Titulo).ToList();
        }

        public ApostasInfoDetalhes GetMercadosInfo(List<Apostas> listApostasFiltro, int mes, string ano){

            ApostasInfoDetalhes mercadosInfoDetalhes= new ApostasInfoDetalhes();

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
    }
}

