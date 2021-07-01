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
        public ApplicationDbContext database = new ApplicationDbContext();
       
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

            var creatorForBase = database.Creators.Single(x => x.email == User.Identity.Name);
            var entry = database.Arts.Add(anc.art);
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
                string userName = null;
                for (int i = 0; i < User.Identity.Name.Length; i++)
                {
                    if (User.Identity.Name[i].ToString() == "@") { break; }
                    userName += User.Identity.Name[i];
                }

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
    }
}