using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class ShopUserEntities
    {
        public List<Shop_New> ShopNewsList { get; set; }
        public List<Shop_Image> ShopImagesList { get; set; }
        public List<User> ShopUserList { get; set; }
    }
}