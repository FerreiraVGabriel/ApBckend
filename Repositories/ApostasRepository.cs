
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Shared;

namespace ProjetoGabrielAPI.Repositories
{
    public class ApostasRepository : IApostasRepository
    {
        private readonly DataContext _context;
        private readonly UtilsProject _utilsProject;

        public ApostasRepository(DataContext context, UtilsProject utilsProject)
        {
            _context = context;
            _utilsProject = utilsProject;
        }

        public void Create(Apostas apostas)
        {
            _context.Add(apostas);
            _context.SaveChanges();
        }

        public List<Apostas> Read()
        {
            var apostas =  _context.Apostas.Include(x=>x.Competicao).Include(x=>x.Mercados).Include(x=>x.TimeMandante)
                                           .Include(x=>x.TimeVisitante).OrderBy(bet=>bet.DataAposta).ToList();
            foreach(var aposta in apostas){
                aposta.CompeticaoNome = aposta.Competicao.Nome;
                aposta.MercadoNome = aposta.Mercados.Nome;
                aposta.TimeMandanteNome = aposta.TimeMandante.Nome;
                aposta.TimeVisitanteNome = aposta.TimeVisitante.Nome;
                aposta.DataApostaString = aposta.DataAposta.ToString();
            }
            return apostas;
        }

        public List<Apostas> ReadApostasPorData(DateTime dataInicio, DateTime dataFim)
        {
            List<Apostas> apostas = new List<Apostas>();
            if(dataInicio == null || dataFim == null){
                 apostas =  _context.Apostas.Include(x=>x.Competicao).Include(x=>x.Mercados).Include(x=>x.TimeMandante)
                                           .Include(x=>x.TimeVisitante).OrderBy(bet=>bet.DataAposta).ToList();
            }
            else{
                apostas =  _context.Apostas.Include(x=>x.Competicao).Include(x=>x.Mercados).Include(x=>x.TimeMandante)
                                           .Include(x=>x.TimeVisitante).Where(x=>x.DataAposta>= dataInicio && x.DataAposta<= dataFim)
                                           .OrderBy(bet=>bet.DataAposta).ToList();
            }

            foreach(var aposta in apostas){
                aposta.CompeticaoNome = aposta.Competicao.Nome;
                aposta.MercadoNome = aposta.Mercados.Nome;
                aposta.TimeMandanteNome = aposta.TimeMandante.Nome;
                aposta.TimeVisitanteNome = aposta.TimeVisitante.Nome;
            }
            return apostas;
        }

        public void Update(int id, Apostas aposta)
        {
            var apostaNew = _context.Apostas.Find(id);

            apostaNew.DataAposta = Convert.ToDateTime(aposta.DataApostaString);
            apostaNew.Stake = aposta.Stake;
            apostaNew.PL = aposta.PL;
            apostaNew.Competicao_id = aposta.Competicao_id;
            apostaNew.Mandante_id = aposta.Mandante_id;
            apostaNew.Visitante_id = aposta.Visitante_id;
            apostaNew.Mercados_id = aposta.Mercados_id;
            apostaNew.RoiStake = UtilsProject.retornaRoiStake(apostaNew.Stake, apostaNew.PL);


            _context.Entry(apostaNew).State = EntityState.Modified;
            _context.SaveChanges();
        }

         public List<Apostas> FiltroAposta(List<Apostas> apostas, string page, string pageSize)
        {
            return _utilsProject.filtroPaginacao(apostas, Convert.ToInt32(page, 10), Convert.ToInt32(pageSize, 10));
        }
    }
}

