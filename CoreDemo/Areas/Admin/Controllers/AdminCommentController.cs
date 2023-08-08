using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCommentController : Controller
    {
        CommentManager commentManager = new(new EfCommentRepository());
        public IActionResult Index()
        {
            var values = commentManager.GetCommentWithBlog();
            return View(values);
        }
        public IActionResult CommentDelete(int id)
        {
            var com = commentManager.GetCommentById(id);
            try
            {
                commentManager.TDelete(com);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CommentEdit(int id)
        {
            var comment=commentManager.GetCommentById(id);
            return View(comment);
        }
        [HttpPost]
        public IActionResult CommentEdit(Comment comment)
        {
            var com = commentManager.GetCommentById(comment.CommentId);
            com.CommentContent = comment.CommentContent;
            com.CommentStatus = comment.CommentStatus;
            try
            {
                commentManager.CommentEdit(com);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            return RedirectToAction("Index");

        }
    }
}
