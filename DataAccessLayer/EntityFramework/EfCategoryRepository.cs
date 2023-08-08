using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
   public class EfCategoryRepository:GenericRepository<Category>, ICategoryDal
    {
        public List<Blog> GetCategoriesWithBlogCount()
        {
            using (var c=new Context())
            {
                return c.Blogs.Include(c => c.Category).GroupBy(x =>new { x.Category.CategoryName,x.CategoryId } ).Select(m => new Blog()
                {
                    BlogImage=m.Key.CategoryName,
                    CategoryId=m.Key.CategoryId,
                    BlogId = m.Count()
                }).ToList();
            }
        }
    }
}
