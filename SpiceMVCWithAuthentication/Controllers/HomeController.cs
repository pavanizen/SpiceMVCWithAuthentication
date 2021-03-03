using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Spicee.DataLayer;
using SpiceMVCWithAuthentication.Models;
using SpiceMVCWithAuthentication.ViewModel;
using Spicee.DomainModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace SpiceMVCWithAuthentication.Controllers
{
    public class HomeController : Controller
    {
        SpiceDbContext db;
        ApplicationDbContext adb;
        public HomeController()
        {
            db = new SpiceDbContext();
            adb = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            ViewBag.user = User.Identity.GetUserId();
            return View();
        }
        public ActionResult Menu()
        {
            
            MenuViewModel Mvm = new MenuViewModel()
            {

                MenuItem = db.MenuItem.ToList(),
                Category = db.Category.ToList(),
                Coupon = db.Coupon.Where(c => c.IsActive == true).ToList()
            };

            return View(Mvm);
            
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            //if (User.Identity.IsAuthenticated)
            //{

           // ViewBag.user = User.Identity.Name;
            ViewBag.user = User.Identity.GetUserId();

            var OneItem = db.MenuItem.Include(e => e.Category).Include(e => e.SubCategory).Where(e => e.Id == id).SingleOrDefault();

             

                return View(OneItem);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
          

        }

        public ActionResult GetMenuItemByCategory(string name)
        {
            var menuitems = db.MenuItem.Where(e => e.Category.Name == name).ToList();

            return View(menuitems);
        }
        public ActionResult AddtoCart(int a,ShoppingCart shoppingCart)
        {
            ViewBag.uId = User.Identity.GetUserId();
            string auId = ViewBag.uId;
            var cartItems = db.ShoppingCart.ToList().Where(e => e.ApplicationUserId == auId && e.MenuItemId == a).FirstOrDefault();
            if(cartItems==null)
            {
                db.ShoppingCart.Add(shoppingCart);
                db.SaveChanges();
            }

            else
            {
                cartItems.Count = cartItems.Count + 1;
                db.SaveChanges();
            }

            //return View();
           return RedirectToAction("Cart");
        }
        public ActionResult OrderHistory()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [Authorize]
        public ActionResult Cart()
        {

            ViewBag.Message = "Items in your cart";
            ViewBag.uId = User.Identity.GetUserId();
           string auId = ViewBag.uId;
            var cartitems = db.ShoppingCart.Where(e => e.ApplicationUserId == auId).ToList();

            return View(cartitems);


        }
        public ActionResult CartPlus(int cartid)
        {
            var cart = db.ShoppingCart.FirstOrDefault(c => c.Id == cartid);
            cart.Count += 1;

            db.SaveChanges();
            return RedirectToAction("Cart");
        }
       
        public ActionResult CartMinus(int cartId)
        {
            var cart = db.ShoppingCart.FirstOrDefault(c => c.Id == cartId);
            if (cart.Count == 1)
            {
                //they removed the last one, so remove the item from the db too
                db.ShoppingCart.Remove(cart);
                db.SaveChanges();

                var count = db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                
            }
            else
            {
                cart.Count -= 1;

                db.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        public ActionResult CartRemove(int cartId)
        {
            var cart = db.ShoppingCart.FirstOrDefault(c => c.Id == cartId);

            //they removed the last one, so remove the item from the db too
            db.ShoppingCart.Remove(cart);
            db.SaveChanges();

            return RedirectToAction("Cart");
        }
        public ActionResult OrderSummary()
        {
            //// ViewBag.Message = "Items for your order";
            // ViewBag.uId = User.Identity.GetUserId();
            // //string auId = ViewBag.uId;
            // var shoppingcart = db.ShoppingCart.ToList();
            // //var cartitems = db.ShoppingCart.Where(e => e.ApplicationUserId == auId).ToList();
            // var viewModel = new MenuViewModel
            // {
            //     ShoppingCarts = shoppingcart
            // };

            // return View(viewModel);


            ViewBag.Message = "Items in your cart";
            ViewBag.uId = User.Identity.GetUserId();
            string auId = ViewBag.uId;
            var cartitems = db.ShoppingCart.Where(e => e.ApplicationUserId == auId).ToList();

            return View(cartitems);


        }
        public ActionResult Checkout()
        {
            ViewBag.Message = "Order Placed Succesfully\n Thanks for Shopping";
            return View();
        }
        public ActionResult CartRemoves(string uName)
        {
            var cart = db.ShoppingCart.Where(e => e.ApplicationUserId == uName);
            db.ShoppingCart.RemoveRange(cart);
            db.SaveChanges();
            return RedirectToAction("Checkout");

        }
    }

}