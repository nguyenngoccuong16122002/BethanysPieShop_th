
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Models
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _appDbContext;
        public ContentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void CreatContent(Content content)
        {
            content.CreatedDate = DateTime.Now;
        
            _appDbContext.Add(content);
            _appDbContext.SaveChanges();
        }
     
        public IEnumerable<Content> GetList()
        {
            return _appDbContext.contents.AsEnumerable();
        }

        public void Delete(Content content, int Id)
        {
            var contentId = _appDbContext.contents.AsNoTracking().FirstOrDefault(c => c.Id ==Id);
            _appDbContext.Remove(content);
            _appDbContext.SaveChanges();
        }

        public void Edit(Content content, int Id)
        {
            var contenEdit= _appDbContext.contents.AsNoTracking().FirstOrDefault(c => c.Id == Id);
            _appDbContext.Update(contenEdit);
            _appDbContext.SaveChanges();
          
           
        }
    }
}
