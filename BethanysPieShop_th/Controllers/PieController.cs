using BethanysPieShop_th.Models;
using BethanysPieShop_th.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BethanysPieShop_th.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository, AppDbContext appDbContext)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _appDbContext = appDbContext;
        }
        /* public IActionResult List()
        {
             PiesListViewModel piesListViewModels = new PiesListViewModel();
             piesListViewModels.Pies = _pieRepository.AllPies;
             piesListViewModels.CurrentCategory = "Cheese Cakes";
             return View(piesListViewModels);
         }*/
        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;

            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                                            .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;

            }
            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        public ActionResult Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            int pageSize = 4;

            var pies = (from s in _appDbContext.Pies select s).OrderBy(x => x.PieId);


            var model = PaginatedList<Pie>.Create(pies, pageNumber ?? 1, pageSize);

            return View(model);

            //return View(pies.ToPagedList(pageNumber, pageSize));

        }


        public IActionResult Search(string SearchString, string sortOrder = "name")
        {
            IEnumerable<Pie> model = null;

            //pie = _pieRepository.AllPies.Where(s => s.Category.CategoryName.Contains(SearchString));
            model = _pieRepository.SearchPie(SearchString, sortOrder);


            return View(model);

        }
        /*  public IActionResult PainatedList(int id,int page)
           {
               IEnumerable<Pie> model = null;
               int pageSize = 5;
               int tolaRow = 0;
               //pie = _pieRepository.AllPies.Where(s => s.Category.CategoryName.Contains(SearchString));
               model = _pieRepository.PiesPanging(id,page,pageSize,out tolaRow);


               return View(model);

           }*/
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }


    }

}
