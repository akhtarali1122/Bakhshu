using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Common;

namespace ShopOnline.Models
{
    public partial class Shop_Item
    {
        public Shop_Item UpdateShopItem(Shop_Item s_item)
        {
            try
            {
                using (shoponlineDataContext context = DbUtils.Create())
                {
                    Shop_Item itemObj = context.Shop_Items.Where(u => u.ItemId.Equals(s_item.ItemId)).SingleOrDefault();
                    if (itemObj != null)
                    {
                        itemObj.User_Id = s_item.User_Id;
                        itemObj.ItemName = s_item.ItemName;
                        itemObj.ItemCompany = s_item.ItemCompany;
                        itemObj.ItemBrand = s_item.ItemBrand;
                        itemObj.ItemPrice = s_item.ItemPrice;
                        itemObj.IsActive = s_item.IsActive;
                        context.SubmitChanges();
                        return itemObj;
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public List<Shop_Item> getAllItems()
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_Items.Where(u=>u.IsActive.Equals(true)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Shop_Item AddItem(Shop_Item SI)
        {
            try
            {

                using (shoponlineDataContext context = DbUtils.Create())
                {
                    context.Shop_Items.InsertOnSubmit(SI);
                    context.SubmitChanges();
                    return SI;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Shop_Item GetShopItem(Int64 Id)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_Items.Where(u => u.ItemId.Equals(Id)).SingleOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public List<Shop_Item> GetShopItemList(Int64 Id)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_Items.Where(s => s.User_Id.Equals(Id)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Boolean DeleteItem(Int64 Item_Id)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    Shop_Item dbItem = context.Shop_Items.Where(u => u.ItemId.Equals(Item_Id)).SingleOrDefault();

                    if (dbItem != null)
                    {
                        context.Shop_Items.DeleteOnSubmit(dbItem);
                        context.SubmitChanges();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }


    }
}