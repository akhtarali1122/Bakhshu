using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Common;

namespace ShopOnline.Models
{
    public partial class Group
    {
        //public List<Group> groupEntity{get;set;}
        //public Int64 groupMembers { get; set; }

        public List<Group> getGroups(Int64 userId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Groups.Where(u => u.User_Id.Equals(userId)).ToList();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public Group getsingleGroup(Int64 groupId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Groups.Where(u => u.GroupId.Equals(groupId)).SingleOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public Group AddGroup(Group g)
        {
            try
            {

                using (shoponlineDataContext context = DbUtils.Create())
                {
                    context.Groups.InsertOnSubmit(g);
                    context.SubmitChanges();
                    return g;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Group UpdateGroup(Group g)
        {
            try
            {
                using (shoponlineDataContext context = DbUtils.Create())
                {
                    Group groupObj = context.Groups.Where(u => u.GroupId.Equals(g.GroupId)).SingleOrDefault();
                    if (groupObj != null)
                    {
                        groupObj.Name = g.Name;
                        groupObj.GroupDescription = g.GroupDescription;
                        groupObj.GroupType=g.GroupType;
                        context.SubmitChanges();
                        return groupObj;
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}