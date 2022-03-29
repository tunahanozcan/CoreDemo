using BussinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        private readonly UserManager<AppUser> _userManager;

        public AdminMessageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        Context c = new();
        public IActionResult Inbox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterId).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerId);
            return View(values);
        }
        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterId).FirstOrDefault();
            var values = mm.GetSendBoxListByWriter(writerId);
            return View(values);
        }
        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View(Tuple.Create<Message2, AppUser>(new Message2(),new AppUser()));
        }
        [HttpPost]
        public async Task<IActionResult> ComposeMessage([Bind(Prefix ="Item1")]Message2 message,[Bind(Prefix ="Item2")] AppUser writer)
        {
            var sender = await _userManager.FindByNameAsync(User.Identity.Name);
            var receiver = await _userManager.FindByEmailAsync(writer.Email);
            message.SenderId = sender.Id;
            message.ReceiverId =receiver.Id;
            message.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            message.MessageStatus = true;
            mm.TAdd(message);
            return RedirectToAction("Sendbox");
        }
    }
}
