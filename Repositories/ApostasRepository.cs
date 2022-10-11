
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Apostas.ToList();
            
        }
    }
}

