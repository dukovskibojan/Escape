using Escape.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escape.Controllers
{
    public class ArtController : Controller
    {
        public ApplicationDbContext database;
        public UserManager<ApplicationUser> userManager;
        public ArtController()
        {
            this.database = new ApplicationDbContext();
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.database));

    }

    protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }
        public ActionResult Index()
        {
            return View(database.Arts.ToList());
        }
        [Authorize]
        public ActionResult Add()
        {
            ViewBag.username = userManager.FindById(User.Identity.GetUserId()).usrName;
            ViewBag.Creators = database.Creators.ToList();
            return View(new aNc());
        }
        [HttpPost]
        [Route("Art/AddSaveChanges/{anc}/{file}")]
        public ActionResult AddSaveChanges(aNc anc, HttpPostedFileBase file)
        {
            /*string userName = null;
            for (int i = 0; i < User.Identity.Name.Length; i++)
            {
                if (User.Identity.Name[i].ToString() == "@") { break; }
                userName += User.Identity.Name[i];
            }*/
            var entry = database.Arts.Add(anc.art);

            var creatorForBase = database.Creators.Single(x => x.email == User.Identity.Name);
            entry.creator = creatorForBase;
            //fizicki se zacuvuva vo folderot Poster
            string path = System.IO.Path.Combine(Server.MapPath("~/Poster"), System.IO.Path.GetFileName(file.FileName));
            file.SaveAs(path);

            //zacuvuvam relativno url vo baza 
            path = "~/Poster/" + file.FileName;
            entry.artUrl = path;
            TryUpdateModel(entry);
            database.SaveChanges();

            return RedirectToAction("Index", "Art");

            //return View("BadRequest");
        }
        [Authorize(Roles = "User")]
        public ActionResult Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("User"))
            {
                var line = database.Arts.Single(x => x.id == id);
                var owner = line.creator.email;

                if (owner != User.Identity.Name) return Content("You are not owner of this art...");

                var model = database.Arts.Single(x => x.id == id);
                if (model == null) return HttpNotFound();
                
                return View(model);
            }
            else return Content("Not an admin or user"); //dodaj novo View()
        }
        public ActionResult ArtEditor(Art art)
        {
            var model = database.Arts.Single(x => x.id == art.id);
            if (model == null) return View("BadRequest");
            TryUpdateModel(model);
            database.SaveChanges();
            return RedirectToAction("Details", "Art", new { id = art.id});
        }
        public ActionResult Details(int id)
        {
            return View(database.Arts.Find(id));
        }
        public ActionResult Delete(int id)
        {
            var model = database.Arts.Single(x => x.id == id);
            if (model == null) return View("BadRequest");
            database.Arts.Remove(model);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AdvancedCreatorOption()
        {
            var creators = database.Creators.ToList();
            return PartialView(creators);
        }
        [Route("Art/ChangeUsername/{newUN}")]
        public ActionResult ChangeUsername(string newUN)
        {
            if (newUN == "") return View("BadRequest");
            var check = database.Creators.Where(x => x.username == newUN).FirstOrDefault();
            if (check != null)
            {
                //ova napraj go da bidi partial navistina!!!!!!!!!!!!!!!!!!!
                return View("usernameExists");
            }
            var entry = userManager.FindByEmail(User.Identity.Name);
            entry.usrName = newUN;
            var entry2 = database.Creators.Single(x => x.email == User.Identity.Name);
            entry2.username = newUN;
            TryUpdateModel(entry2);
            database.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }
        public ActionResult CreatorDetails(int id)
        {
            var creator = database.Creators.Single(x => x.id == id);
            List<Art> arts = database.Arts.ToList();
            List<Art> artsToView = new List<Art>();
            foreach (Art art in arts)
            {
                if (art.creator.id == id)
                    artsToView.Add(art);
            }
            ViewBag.Arts = artsToView;
            return View(creator);
        }
    }
}