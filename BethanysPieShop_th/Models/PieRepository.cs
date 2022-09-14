using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;
     
        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        
        }

        public IEnumerable<Pie> AllPies => _appDbContext.Pies.Include(c => c.Category);

        public IEnumerable<Pie> PiesOfTheWeek => _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
        public Pie GetPieByName(string Name)
        {
            return _appDbContext.Pies.FirstOrDefault(w => w.Name.Contains(Name));
        }

        public Pie GetPieName(string Name)
        {
            throw new NotImplementedException();
        }

       /* public IEnumerable<Pie> PiesPanging(int PieId, int page, int pageSize, out int totalRow)
        {
            var query =_appDbContext.Pies.Where(c=>c.CategoryId==PieId);
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
       */
        public IEnumerable<Pie> SearchPie(string searchString, string sortOrder)
        {
            IEnumerable<Pie> result;

            try
            {
                var pies = from s in _appDbContext.Pies
                           select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    pies = pies.Where(s => s.Name.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        pies = pies.OrderByDescending(s => s.Name);
                        break;

                    default:
                        pies = pies.OrderByDescending(s => s.PieId);
                        break;
                }

                result = pies.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

      
    }
}
