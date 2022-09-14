using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Models
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _appDbContext;
        public ContactRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void CreateContact(Contact contact)
        {
            contact.ContactPlaced = DateTime.Now;
            _appDbContext.contacts.Add(contact);
            _appDbContext.SaveChanges();
        }
       
    }
}
