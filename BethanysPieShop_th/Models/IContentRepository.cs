using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Models
{
    public interface IContentRepository
    {
        void CreatContent(Content content);
        IEnumerable<Content> GetList();
        void Delete(Content content,int Id);
        void Edit(Content content, int Id);
    }
}
