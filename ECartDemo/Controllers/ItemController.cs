using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECartDemo.Models;
using ECartDemo.ViewModel;
using System.IO;

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

        [HttpPost]
        public  JsonResult Index(ItemViewModel objItemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName);
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + NewImage;
            objItem.CategoryId = objItemViewModel.CategoryId;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            objItem.ItemName = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;

            objECartDbEntities.Items.Add(objItem);
            objECartDbEntities.SaveChanges();

            return Json(new {Success = true, Message = "Item is added Successfully."}, JsonRequestBehavior.AllowGet);
        }
    }
}