using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spicee.DataLayer;
using Spicee.DomainModels;

namespace SpiceMVCWithAuthentication.Controllers
{
    public class CategoryController : Controller
    {
        SpiceDbContext db = new SpiceDbContext();
        public CategoryController()
        {

        }
        // GET: Category
        public ActionResult Index()
        {
            var categ = db.Category.ToList();


            return View(categ);
            
        }
        //Get:Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category c)
        {
            if (ModelState.IsValid)
            {

                db.Category.Add(c);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(c);
        }

        //Get:Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var emp = db.Category.SingleOrDefault(e => e.Id == id);
            //var category = db.Category.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);

        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit");
            }
            else
            {
                var updatedRes = db.Category.Where(e => e.Id == category.Id).FirstOrDefault();
                updatedRes.Name = category.Name;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        //GET - DELETE
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            var category = db.Category.Find(id);

            if (category == null)
            {
                return View();
            }
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET - DETAILS
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }


    }















}