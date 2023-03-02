
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public class MercadosRepository : IMercadosRepository
    {
        private readonly DataContext _context;

        public MercadosRepository(DataContext context)
        {
            _context = context;
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

        public List<ApostasInfo> ReadMercadoInfo(List<Apostas> apostas, List<Mercados> mercados){
            List<Apostas> apostasFiltro = new List<Apostas>();
            List<ApostasInfo> listMercadosInfo = new List<ApostasInfo>();

            int totalMes = 12;

            for(int mes=0; mes<= totalMes; mes++ ){
                foreach(Mercados mercado in mercados){
                    if(mes == 0){
                        apostasFiltro = apostas.Where(x=>x.Mercados_id == mercado.Id).ToList();

                        if(apostasFiltro.Any())
                            listMercadosInfo.Add(GetMercadosInfo(apostasFiltro, mes));
                    }
                    else{
                        apostasFiltro = apostas.Where(x=>x.Mercados_id == mercado.Id && x.DataAposta.Month == mes).ToList();

                        if(apostasFiltro.Any())
                            listMercadosInfo.Add(GetMercadosInfo(apostasFiltro, mes));
                    }
                }
            }

            return listMercadosInfo.OrderBy(x=>x.Titulo).ToList();
        }

        public ApostasInfo GetMercadosInfo(List<Apostas> listApostasFiltro, int mes){

            ApostasInfo mercadosInfo = new ApostasInfo();

            mercadosInfo.Titulo = listApostasFiltro[0].MercadoNome;
            mercadosInfo.PeriodoNome = mes.ToString();
            mercadosInfo.LucroPerda = listApostasFiltro.Select(x=>x.PL).Sum();
            mercadosInfo.Green = listApostasFiltro.Where(x=> x.PL >= 0).Count();
            mercadosInfo.Red = listApostasFiltro.Where(x=> x.PL < 0).Count();
            mercadosInfo.NumApostas = listApostasFiltro.Count();

            return mercadosInfo;
        }
    }
}

