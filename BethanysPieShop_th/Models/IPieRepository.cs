using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }
        Pie GetPieById(int pieId);
        Pie GetPieName(string Name);

        IEnumerable<Pie> SearchPie(string searchString, string sortOrder);
       // IEnumerable<Pie> PiesPanging(int Id, int page, int pageSize,out int totalRow);
    }
}
