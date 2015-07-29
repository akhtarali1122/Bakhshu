using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Common;

namespace ShopOnline.Models
{
    public partial class GroupMember
    {
        public List<GroupMember> getGroupMembers(Int64 groupId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.GroupMembers.Where(u => u.Group_Id.Equals(groupId)).ToList();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public GroupMember MakeGroupMembers(GroupMember gm)
        {
            try
            {
                using (shoponlineDataContext context = DbUtils.Create())
                {
                    context.GroupMembers.InsertOnSubmit(gm);
                    context.SubmitChanges();
                    return gm;

                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}