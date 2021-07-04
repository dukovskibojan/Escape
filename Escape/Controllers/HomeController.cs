using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Escape.Models;

namespace Escape.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Веб апликација со наслов Escape, каде е овозможено креирање на профили и " +
                "поставување на дела креирани со сопствен труд(art). Апликацијата не е наменета задружење и" +
                " поставување на секојдневни приказни, туку на дела и собирање информации за артисти." + 
                "Функционалности: поставување на ставка - арт(Add), со тоа што е дозволено поле за прикачување " +
                "на слика и внес на текст за опис на арт-от и авторот, бришење на ставка(Delete), измена(Edit)," +
                " преглед на сите ставки(или одредена ставка), можности за доделување наулоги на " +
                "корисници(овозможено само за Admin) и друго. Апликацијата ги содржи сите CRUD операции," +
                " но има додадено дополнителни функционалности за истата да функционира во целост и да " +
                "изгледа одлично.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}