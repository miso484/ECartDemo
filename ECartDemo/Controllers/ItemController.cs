using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECartDemo.Models;
using ECartDemo.ViewModel;

namespace ECartDemo.Controllers
{
    public class ItemController : Controller
    {
        private ECartDBEntities objECartDbEntities;
        public ItemController()
        {
            objECartDbEntities = new ECartDBEntities();
        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();
            objItemViewModel.CategorySelectListItem = (from objCat in objECartDbEntities.Categories
                                                       select new SelectListItem()
                                                       {
                                                           Text = objCat.CategoryName,
                                                           Value = objCat.CategoryId.ToString(),
                                                           Selected = true
                                                       });
            return View(objItemViewModel);
        }
    }
}