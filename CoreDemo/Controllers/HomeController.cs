using BussinessLayer.Concrete;
using CoreDemo.Models;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        BannedListManager blm = new BannedListManager(new EfBannedListRepository());

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(MailModel mailModel)
        {
            var ipAdress = HttpContext.Connection.RemoteIpAddress.ToString();
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(captchaImage))
            {
                ModelState.AddModelError("ReCaptcha", "Captcha doğrulanmamış");
                return View();
                //return Content("doldurulmadı");
            }

            var verified = await CheckCaptcha();
            if (!verified)
            {
                //ModelState.AddModelError("captcha", "Captcha yanlış doğrulanmış");
                //return View();
                return Content("doğrulanmadı");
            }

            if (ModelState.IsValid)
            {
                List<string> spamMessageList = blm.GetSpamMessageList();
                List<string> bannedIpList = blm.GetBannedIpList();
                string[] messagesWords = mailModel.Message.Split(' ');

                foreach (string message in messagesWords)
                {
                    if (spamMessageList.Contains(message) || bannedIpList.Contains(ipAdress))
                    {
                        return View();
                    }

                };
                //MailMessage mailMessage = new();
                //mailMessage.From = new MailAddress("mail", "tunahanozcan.com");
                //mailMessage.To.Add("mail");
                //mailMessage.Subject = "tunahanozcan.com bildirimi";
                //mailMessage.Body = @$"{mailModel.Name} - {mailModel.Mail}, tunahanozcan.com üzerinden mail gönderdi,<br/> {mailModel.Message}<br/> Ip Adresi: {ipAdress}";
                //mailMessage.IsBodyHtml = true;

                //SmtpClient smtp = new();
                //smtp.Host = "mail.tunahanozcan.com";
                //smtp.Port = 587;
                //smtp.EnableSsl = false;
                //smtp.Credentials = new NetworkCredential("mail", "password");
                //smtp.Send(mailMessage);

                return View();

                //Herşey doğru
            }



            
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task<bool> CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
            {
                //new KeyValuePair<string, string>("secret", "kepçeKey"),
                //new KeyValuePair<string, string>("remoteip", HttpContext.Connection.RemoteIpAddress.ToString()),
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])
            };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return (bool)o["success"];
        }
    }
    public class MailModel
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Message { get; set; }
        public string ReCaptcha { get; set; }
    }

}
