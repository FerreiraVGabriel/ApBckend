
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Models;

namespace ProjetoGabrielAPI.Repositories
{
    public interface ITimesRepository
    {
        List<Times> Read();

        void Create (Times competicao);
    }
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
            return _context.Times.ToList();
            
        }
    }
}

