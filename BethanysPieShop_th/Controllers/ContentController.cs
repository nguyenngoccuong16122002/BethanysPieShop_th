using BethanysPieShop_th.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;
        private readonly AppDbContext _appDbContext;
        public ContentController(IContentRepository contentRepository,AppDbContext appDbContext)
        {
            _contentRepository = contentRepository;
            _appDbContext = appDbContext;
        }
        //public IActionResult Index()
        //{
        //    var model = _contentRepository.GetList();
        //    return View(model);
        //}
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

            var contents = _appDbContext.contents.OrderBy(x => x.Id);


            var model = PaginatedList<Content>.Create(contents, pageNumber ?? 1, pageSize);

            return View(model);

            //return View(pies.ToPagedList(pageNumber, pageSize));

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Content content, IFormFile myfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fullPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot", "MyFiles",
                        myfile.FileName
                        );

                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        myfile.CopyTo(file);
                    }

                    content.UrlImage = String.Format("MyFiles/{0}", myfile.FileName);

                    _contentRepository.CreatContent(content);
                    ModelState.AddModelError("", "Tao Thanh Cong");
                }
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            return View(content);
        }
      public RedirectToActionResult Delete(Content content, int Id)
        {
            _contentRepository.Delete(content,Id);
            return RedirectToAction("Index");
        }
        public  IActionResult Edit(int id, [Bind("Id,Title,ShortDescription,Description,UrlImage,CreatedDate")] Content content, IFormFile myfile)
        {
            if (id != content.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                        string fullPath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot", "MyFiles",
                            myfile.FileName
                            );

                        using (var file = new FileStream(fullPath, FileMode.Create))
                        {
                            myfile.CopyTo(file);
                        }

                        content.UrlImage = String.Format("MyFiles/{0}", myfile.FileName);
                    content.CreatedDate = DateTime.Now;
                        _appDbContext.Update(content);
                        _appDbContext.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(content);
        }
    }
}
