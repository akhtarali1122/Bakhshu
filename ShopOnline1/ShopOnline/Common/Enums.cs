using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Common
{
    public enum OperationMessageType : int
    {
        Error = 1,
        Notics = 2,
        Information = 3,
        Success = 4
    }
        public enum UserRolesType : int
        {
            Admin = 1,
            RegisteredUser = 2,
        }
        public enum GroupMemberRolesType : int
        {
            GroupAdmin = 1,
            GroupMember = 2,
        }
        public enum UserStatusType : int
        {
            Active = 1,
            InActive = 2,
            Suspended = 3,
            Blocked = 4
        }
}