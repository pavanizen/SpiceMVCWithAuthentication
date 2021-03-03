using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spicee.DataLayer;
using Spicee.DomainModels;
using System.Data.Entity;
using Spicee.DomainModels.ViewModel;

namespace SpiceMVCWithAuthentication.Controllers
{
    public class SubCategoryController : Controller
    {
        SpiceDbContext db = new SpiceDbContext();
        public SubCategoryController()
        {
            
        }
        // GET: SubCategory
        public ActionResult Index()
        {
            var subCategories = db.SubCategory.Include(s => s.Category).ToList();

            return View(subCategories);
            
        }
        public ActionResult Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = db.Category.ToList(),
                SubCategory = new Spicee.DomainModels.SubCategory(),

                //order the sub cat in the db by name, select all of them by name, then get only the names
                SubCategoryList = db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToList()
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                //read all sub categories from db
                //include the category data with include
                //but only where the name is correct for the current selected model
                //and its in the same cat id
                var doesSubCategoryExists = db.SubCategory.Include(s => s.Category)
                    .Where(s => s.Name == model.SubCategory.Name
                    && s.Category.Id == model.SubCategory.CategoryId);

                if (doesSubCategoryExists.Count() > 0)
                {
                    //Bad, we dont want duplicate sub categories
                  
                   // StatusMessage = "Error : Sub Category already exist under " + doesSubCategoryExists.First().Category.Name + " please choose another name";
                }
                else
                {
                    db.SubCategory.Add(model.SubCategory);
                    db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            //the model passeed in is not in a valid state so we have to make 
            //a new one to reset to and tell the user the issue
            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = db.Category.ToList(),
                SubCategory = model.SubCategory,
                SubCategoryList = db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToList(),
                //StatusMessage = StatusMessage
            };

            return View(modelVM);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var subCategory = db.SubCategory.Include(s => s.Category).SingleOrDefaultAsync(m => m.Id == id);

            if (subCategory == null)
            {
                return HttpNotFound();
            }

            return View(subCategory);
        }


    }
}