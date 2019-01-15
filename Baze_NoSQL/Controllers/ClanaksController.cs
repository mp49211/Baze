using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Baze_NoSQL.Models;
using MongoDB.Bson;

namespace Baze_NoSQL.Controllers
{
    public class ClanaksController : Controller
    {
        public Baza baza = new Baza();

        // GET: Clanaks
        public ActionResult Index()
        {
            IEnumerable<Clanak> clanc = baza.GetClanci(10);
            List<Clanak> clanci = new List<Clanak>();
            foreach(var clanak in clanc)
            {
                clanak.Slika = baza.GetSlika(clanak.SlikaId);
                //var base64 = Convert.ToBase64String(clanak.Slika);
                //clanak.SlikaStr = String.Format("data:image/jpg;base64,{0}", base64);

                string mimeType = "image/jpg";
                string base64 = Convert.ToBase64String(clanak.Slika);
                clanak.SlikaStr = string.Format("data:{0};base64,{1}", mimeType, base64);
                Console.WriteLine(clanak.SlikaStr);
                clanci.Add(clanak);
            }
            return View(clanci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string id, string text)
        {
            try
            {

                baza.AddKomentar(text, new ObjectId(id));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Clanaks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Clanaks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clanaks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Clanaks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Clanaks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Clanaks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Clanaks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}