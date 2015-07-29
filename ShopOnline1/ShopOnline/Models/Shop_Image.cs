using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Common;

namespace ShopOnline.Models
{
    public partial class Shop_Image
    {
        public List<Shop_Image> getAllImages()
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_Images.Where(a => a.Is_For_Home_Page_Slider.Equals(true)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Shop_Image> GetShopImagesList(Int64 UserId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_Images.Where(c => c.User_Id.Equals(UserId)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Shop_Image GetShopImage(Int64 shopImageId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_Images.Where(u => u.Shop_Image_Id.Equals(shopImageId)).SingleOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public Boolean DeleteShopImage(Int64 shopImageId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    Shop_Image dbShopImage = context.Shop_Images.Where(u => u.Shop_Image_Id.Equals(shopImageId)).SingleOrDefault();
                    if (dbShopImage != null)
                    {
                        context.Shop_Images.DeleteOnSubmit(dbShopImage);
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
        public long AddShopImage(Shop_Image shopImage)
        {
            try
            {

                using (shoponlineDataContext context = DbUtils.Create())
                {
                    context.Shop_Images.InsertOnSubmit(shopImage);
                    context.SubmitChanges();
                    return shopImage.Shop_Image_Id;

                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}