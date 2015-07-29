using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class ShopProfileEntity
    {
        public List<Shop_New> ShopNewsList { get; set; }
        public List<Shop_Image> ShopImagesList { get; set; }
        public User ShopUserList { get; set; }
        public List<Shop_Item> ShopItemsList { get; set; }
    }
}