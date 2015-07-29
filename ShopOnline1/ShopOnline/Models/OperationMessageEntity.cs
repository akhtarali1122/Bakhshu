using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class OperationMessageEntity
    {
        public int MessageType { get; set; }
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
    }
}