
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public class ApostasLiveRepository : IApostasLiveRepository
    {
        private readonly DataContext _context;

        public ApostasLiveRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(ApostasLive apostasLive)
        {
            _context.Add(apostasLive);
            _context.SaveChanges();
            
        }

        public List<ApostasLive> Read()
        {
            return _context.ApostasLive.ToList();
            
        }
    }
}

