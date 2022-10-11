
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public interface IMercadosRepository
    {
        List<Mercados> Read();

        void Create (Mercados mercados);
    }
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
    }
}

