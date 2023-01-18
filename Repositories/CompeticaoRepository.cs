
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public interface ICompeticaoRepository
    {
        List<Competicao> Read();

        void Create (Competicao competicao);
    }
    public class CompeticaoRepository : ICompeticaoRepository
    {
        private readonly DataContext _context;

        public CompeticaoRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Competicao competicao)
        {
            _context.Add(competicao);
            _context.SaveChanges();
            
        }

        public List<Competicao> Read()
        {
            return _context.Competicao.OrderBy(competition=>competition.Nome).ToList();
            
        }
    }
}

