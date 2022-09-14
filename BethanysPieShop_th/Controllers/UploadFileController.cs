using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Controllers
{
    public class UploadFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadFile(IFormFile myfile)
        {
            if (myfile != null)
            {
                //chi dinh duong luu
                string fullpat = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MyFiles", myfile.FileName);
                //copy file vao thu muc chi dinh
                using (var file = new FileStream(fullpat, FileMode.Create))
                {
                    myfile.CopyTo(file);
                }
            }
            return View("Index");
        }
    }
}
