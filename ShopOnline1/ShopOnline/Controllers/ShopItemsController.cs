using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Filters;
using ShopOnline.Common;
using ShopOnline.Models;
using System.Web.Routing;

namespace ShopOnline.Controllers
{
    public class ShopItemsController : Controller
    {
        //
        // GET: /ShopItems/
        [SessionExpireFilter]
        public ActionResult Index(string RecSaveOperation, string hidItemId)
        {
            if (!String.IsNullOrWhiteSpace(hidItemId))
            {
                ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_ITEMS;
                ViewData[Constants.MENU] = Constants.MANAGE_ITEMS;
                ViewBag.TitleDes = "Free shop advertisement";
                string Item_Id = new SecurityManager().DecodeString(hidItemId);
                Shop_Item SI = new Shop_Item().GetShopItem(Convert.ToInt64(Item_Id));
                ViewData["txtItemName"] = SI.ItemName;
                ViewData["txtItemCompany"] = SI.ItemCompany;
                ViewData["txtItemBrand"] = SI.ItemBrand;
                ViewData["txtPrice"] = SI.ItemPrice;
                ViewData["chkActive"] = SI.IsActive;
                ViewData["hidItemId"] = hidItemId;

            }
            else
            {
                ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_ITEMS;
                ViewData[Constants.MENU] = Constants.ADD_ITEMS;
                ViewBag.TitleDes = "Free shop advertisement";
                if (!String.IsNullOrEmpty(RecSaveOperation))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record saved successfully.");
                }
                if (!String.IsNullOrEmpty(hidItemId))
                {
                    ViewData.Model = new Shop_Item().GetShopItem(Convert.ToInt64(hidItemId));
                }
            }
            return View("AddItems");
        }
        [SessionExpireFilter]
        public ActionResult ManageShopItems(string RecDelOperation, string RecDelErrorOperation, string RecUpdateOperation)
        {
            ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_ITEMS;
            ViewData[Constants.MENU] = Constants.MANAGE_ITEMS;
            if (!String.IsNullOrEmpty(RecDelOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record Deleted successfully.");
            }
            if (!String.IsNullOrEmpty(RecDelErrorOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Success", "Sorry Record Could not be Deleted.");
            }
            if (!String.IsNullOrEmpty(RecUpdateOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Record Updated successfully.");
            }
            ViewBag.TitleDes = "Free shop advertisement";
            ViewData.Model = new Shop_Item().GetShopItemList(new ShopOnline.Filters.UserSession().getUserSession().UserId);

            return View();
        }
        //public ActionResult EditShopItems(string ItemId)
        //{
        //    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_ITEMS;
        //    ViewData[Constants.MENU] = Constants.MANAGE_ITEMS;

        //    ViewBag.TitleDes = "Free shop advertisement";

        //    string Item_Id = new SecurityManager().DecodeString(ItemId);

        //    ViewData.Model = new Shop_Item().GetShopItem(Convert.ToInt64(Item_Id));

        //    return View("EditItems");
        //}
        [HttpPost]
        [SessionExpireFilter]
        public ActionResult AddItem(string txtItemName, string txtItemCompany, string txtItemBrand, string txtPrice, string chkActive, string hidItemId)
        {
            ViewBag.TitleDes = "Free shop advertisement";
            try
            {
                if (!String.IsNullOrWhiteSpace(hidItemId))
                {
                    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_ITEMS;
                    ViewData[Constants.MENU] = Constants.MANAGE_ITEMS;
                }
                else
                {
                    ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_ITEMS;
                    ViewData[Constants.MENU] = Constants.ADD_ITEMS;
                }

                ViewData["txtItemName"] = txtItemName;
                ViewData["txtItemCompany"] = txtItemCompany;
                ViewData["txtItemBrand"] = txtItemBrand;
                ViewData["txtPrice"] = txtPrice;
                ViewData["chkActive"] = chkActive;
                ViewData["hidItemId"] = hidItemId;


                Boolean isValid = true;

                if (string.IsNullOrWhiteSpace(txtItemName))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input Field Required", "Please Provide Item Name");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtItemCompany))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input Field Required", "Please Provide Item Company");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtItemBrand))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input Field Required", "Please Provide Item Brand");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtPrice))
                {
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input Field Required", "Please Provide Item Price");
                    isValid = false;
                }

                if (isValid == true)
                {
                    Shop_Item SI = new Shop_Item();
                    SI.User_Id = new ShopOnline.Filters.UserSession().getUserSession().UserId;
                    SI.ItemName = txtItemName;
                    SI.ItemCompany = txtItemCompany;
                    SI.ItemBrand = txtItemBrand;
                    SI.ItemPrice = txtPrice;
                    SI.IsActive = false;
                    if (!string.IsNullOrWhiteSpace(chkActive) && chkActive.Trim().Equals("1"))
                    {
                        SI.IsActive = true;
                    }

                    if (!string.IsNullOrWhiteSpace(hidItemId))
                    {
                        SI.ItemId = Convert.ToInt64(new SecurityManager().DecodeString(hidItemId));
                    }
                    if (SI.ItemId > 0)
                    {
                        new Shop_Item().UpdateShopItem(SI);
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopItems", action = "ManageShopItems", RecUpdateOperation = "1" }));
                    }
                    else
                    {
                        SI.Created_On = DateTime.UtcNow;
                        Shop_Item shop_item = new Shop_Item().AddItem(SI);
                        if (shop_item != null)
                        {
                            return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopItems", action = "Index", RecSaveOperation = "1" }));
                        }
                        else
                        {
                            ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Sorry record could not be saved");
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return View("AddItems");
        }
        public ActionResult DeleteItem(string ItemId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ItemId))
                {
                    string Item_Id = new SecurityManager().DecodeString(ItemId);
                    Boolean retItem = new Shop_Item().DeleteItem(Convert.ToInt64(Item_Id));
                    if (retItem == true)
                    {
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopItems", action = "ManageShopItems", RecDelOperation = "1" }));
                    }
                    else
                    {
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopItems", action = "ManageShopItems", RecDelErrorOperation = "1" }));
                    }
                }
            }
            catch (Exception)
            {
            }
            return RedirectToRoute(new RouteValueDictionary(new { controller = "ShopItems", action = "ManageShopItems" }));
        }
    }
}
