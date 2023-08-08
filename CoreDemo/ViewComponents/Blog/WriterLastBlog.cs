using BussinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Blog
{
    public class WriterLastBlog : ViewComponent
    {
            BlogManager bm = new BlogManager(new EfBlogRepository());
            public IViewComponentResult Invoke(int id)
            {
                var values = bm.GetBlogListByWriter(id);
                return View(values);
            }
    }
}
