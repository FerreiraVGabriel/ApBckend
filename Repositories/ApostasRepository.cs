
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public interface IApostasRepository
    {
        List<Apostas> Read();

        void Create (Apostas apostas);
    }
    public class ApostasRepository : IApostasRepository
    {
        private readonly DataContext _context;

        public ApostasRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Apostas apostas)
        {
            _context.Add(apostas);
            _context.SaveChanges();
        }

        public List<Apostas> Read()
        {
            //return _context.Apostas.OrderBy(bet=>bet.DataAposta).ToList();
            var apostas =  _context.Apostas.Include(x=>x.Competicao).Include(x=>x.Mercados).Include(x=>x.TimeMandante)
                                    .Include(x=>x.TimeVisitante).OrderBy(bet=>bet.DataAposta).ToList();
            foreach(var aposta in apostas){
                aposta.CompeticaoNome = aposta.Competicao.Nome;
                aposta.MercadoNome = aposta.Mercados.Nome;
                aposta.TimeMandanteNome = aposta.TimeMandante.Nome;
                aposta.TimeVisitanteNome = aposta.TimeVisitante.Nome;
            }
            return apostas;
        }
    }
}

