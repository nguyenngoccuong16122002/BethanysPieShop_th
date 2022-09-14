using BethanysPieShop_th.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact contact)
        {

            if (ModelState.IsValid)
            {
                _contactRepository.CreateContact(contact);
                return RedirectToAction("CheckoutComplete");
            }
            return View(contact);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your comments. we will take note and will develop more!";
            return View();
        }
            
        
    }
}
