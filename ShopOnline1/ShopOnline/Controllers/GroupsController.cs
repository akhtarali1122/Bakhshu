using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Common;
using ShopOnline.Filters;
using ShopOnline.Models;
using System.Web.Routing;

namespace ShopOnline.Controllers
{
    public class GroupsController : Controller
    {
        //
        // GET: /Groups/

        public ActionResult Index()
        {
            return View();
        }
        [SessionExpireFilter]
        public ActionResult CreateGroup(string groupId)
        {
            ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_GROUPS;
            ViewData[Constants.MENU] = Constants.CREATE_GROUP;
            ViewBag.TitleDes = "Free shop advertisement";

            //List<Group> g = new List<Group>();
            if (!string.IsNullOrWhiteSpace(groupId))
            {
                string GroupId = new SecurityManager().DecodeString(groupId);
                Group g = new Group().getsingleGroup(Convert.ToInt64(GroupId));
                ViewData["txtGroupName"] = g.Name;
                ViewData["txtDescription"] = g.GroupDescription;
                ViewData["radType"] = g.GroupType;
                ViewData["hidGroupId"] = groupId;
            }
            return View();
        }
        public ActionResult AddGroup(string txtGroupName, string txtDescription, string radType, string hidGroupId)
        {
            try
            {
                ViewData["txtGroupName"] = txtGroupName;
                ViewData["txtDescription"] = txtDescription;
                ViewData["radType"] = radType;
                Boolean validInput = true;
                if (string.IsNullOrWhiteSpace(txtGroupName))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter Group Name");
                }
                if (string.IsNullOrWhiteSpace(txtDescription))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please enter Description of the Group");
                }
                if (string.IsNullOrWhiteSpace(radType))
                {
                    validInput = false;
                    ViewData[Constants.OPERATIONALMESSAGE] = Utils.getErrorMessage("Input field required", "Please select Group Type");
                }

                if (validInput == true)
                {
                    Group g = new Group();
                    Group updateGroup = new Group();
                    g.Name = txtGroupName;
                    g.GroupDescription = txtDescription;
                    g.GroupType = radType;
                    if (!string.IsNullOrWhiteSpace(hidGroupId))
                    {

                        g.GroupId = Convert.ToInt64(new SecurityManager().DecodeString(hidGroupId));


                    }
                    if (g.GroupId > 0)
                    {
                        new Group().UpdateGroup(g);
                        return RedirectToRoute(new RouteValueDictionary(new { controller = "Groups", action = "ManageGroups", RecUpdateOperation = "1" }));
                    }

                    else
                    {
                        g.User_Id = new ShopOnline.Filters.UserSession().getUserSession().UserId;
                        g.CreatedOn = DateTime.UtcNow;
                        Group group = g.AddGroup(g);
                        GroupMember gm = new GroupMember();

                        gm.Group_Id = group.GroupId;
                        gm.User_Id = new ShopOnline.Filters.UserSession().getUserSession().UserId;
                        gm.MemberRole = Convert.ToInt32(GroupMemberRolesType.GroupAdmin);
                        gm.CreatedOn = DateTime.UtcNow;
                        gm.AddBy = new ShopOnline.Filters.UserSession().getUserSession().UserId;
                        gm = gm.MakeGroupMembers(gm);
                        if (group != null && gm!=null)
                        {
                            return RedirectToRoute(new RouteValueDictionary(new { controller = "Groups", action = "ManageGroups", RecCreateOperation = "1" }));
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
            return View();
        }
        [SessionExpireFilter]
        public ActionResult ManageGroups(string RecCreateOperation, string RecUpdateOperation)
        {
            ViewData[Constants.MENU_PARENT] = Constants.MENU_MANAGE_GROUPS;
            ViewData[Constants.MENU] = Constants.MANAGE_GROUPS;
            ViewBag.TitleDes = "Free shop advertisement";
            if (!String.IsNullOrEmpty(RecCreateOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Group Created successfully.");
            }
            if (!String.IsNullOrWhiteSpace(RecUpdateOperation))
            {
                ViewData[Constants.OPERATIONALMESSAGE] = Utils.getSuccessMessage("Success", "Group setting changed successfully.");
            }
            List<Group> g = new List<Group>();
            Int64 userId = new ShopOnline.Filters.UserSession().getUserSession().UserId;
            g = new Group().getGroups(Convert.ToInt64(userId));

            ViewData.Model = g;
            return View();
        }
    }
}
