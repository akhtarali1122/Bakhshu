using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Common;

namespace ShopOnline.Models
{
    public partial class User
    {
        public List<Shop_Image> Shop_Image_List { get; set; }
        public User UserEntity { get; set; }

        //public string Shop_Profile_Photo { get; set; }
        //public string User_Profile_Photo { get; set; }

        public List<User> getAllUsers()
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Users.Where(a=>a.Is_ShopCreated.Equals(true)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User AuthnticateUser(User objUser)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    User dbUser = context.Users
                    .Where(u => u.UserLoginName.Equals(objUser.UserLoginName)
                     && u.UserPassword.Equals(objUser.UserPassword)
                     && u.UserStatus.Equals(objUser.UserStatus)
                        ).SingleOrDefault();

                    return dbUser;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public Boolean CheckUserAvailablity(string LoginName)
        {
            using (shoponlineDataContext context = DbUtils.Create())
            {
                try
                {
                    User dbUser = null;
                    dbUser = context.Users.Where(u => u.UserLoginName.Equals(LoginName)).SingleOrDefault();
                    if (dbUser != null)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                }
            }
            return true;
        }
        public long CreateUser(User userObj)
        {
            try
            {

                using (shoponlineDataContext context = DbUtils.Create())
                {
                    context.Users.InsertOnSubmit(userObj);
                    context.SubmitChanges();
                    return userObj.UserId;

                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public User UpdateUser(User userObj)
        {
            try
            {
                using (shoponlineDataContext context = DbUtils.Create())
                {
                    User dbUserObj = context.Users.Where(u => u.UserId.Equals(userObj.UserId)).SingleOrDefault();
                    if (dbUserObj != null)
                    {
                        dbUserObj.UserLoginName = userObj.UserLoginName;
                        dbUserObj.UserFirstName = userObj.UserFirstName;
                        dbUserObj.UserLastName = userObj.UserLastName;
                        dbUserObj.Contact_Email = userObj.Contact_Email;
                        dbUserObj.Gender = userObj.Gender;
                        dbUserObj.DOB = userObj.DOB;
                        dbUserObj.Last_Updated_On = userObj.Last_Updated_On;
                        context.SubmitChanges();
                        return dbUserObj;
                    }
                }
            }
            catch (Exception)
            { }
            return null;
        }
        public User CreateShop(User userObj)
        {
            try
            {
                using (shoponlineDataContext context = DbUtils.Create())
                {
                    User dbUserObj = context.Users.Where(u => u.UserId.Equals(userObj.UserId)).SingleOrDefault();
                    if (dbUserObj != null)
                    {
                        dbUserObj.ShopName = userObj.ShopName;
                        dbUserObj.ShopCountry = userObj.ShopCountry;
                        dbUserObj.ShopCity = userObj.ShopCity;
                        dbUserObj.ShopAddress = userObj.ShopAddress;
                        dbUserObj.ShopMapUrl = userObj.ShopMapUrl;
                        dbUserObj.ShopDescription = userObj.ShopDescription;
                        dbUserObj.Shop_Last_Updated_On = userObj.Shop_Last_Updated_On;
                        dbUserObj.Is_ShopCreated= userObj.Is_ShopCreated;
                        context.SubmitChanges();
                        return dbUserObj;
                    }
                }
            }
            catch (Exception)
            { }
            return null;
        }

        public User GetUser(Int64 UserId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Users.Where(u => u.UserId.Equals(UserId)).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Boolean ChangeUserStatus(Int64 UserId, Int32 StatusTypeId)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    User dbUser = context.Users.Where(u => u.UserId.Equals(UserId)).SingleOrDefault();

                    if (dbUser != null)
                    {
                        dbUser.UserStatus = StatusTypeId;
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

        public string ChangeProfilePhoto(Int64 UserId, string profilePhoto)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    User dbUser = context.Users.Where(u => u.UserId.Equals(UserId)).SingleOrDefault();
                    string filename = dbUser.ProfilePhoto;
                    if (dbUser != null)
                    {
                        dbUser.ProfilePhoto = profilePhoto;
                        context.SubmitChanges();
                        return filename;
                    }
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
        public string ChangeProfileShopPhoto(Int64 UserId, string profileShopPhoto)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    User dbUser = context.Users.Where(u => u.UserId.Equals(UserId)).SingleOrDefault();
                    string filename = dbUser.ShopPhoto;
                    if (dbUser != null)
                    {
                        dbUser.ShopPhoto = profileShopPhoto;
                        context.SubmitChanges();
                        return filename;
                    }
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
        public Boolean ChangeUserPassword(Int64 UserId, String password)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    User dbUser = context.Users.Where(u => u.UserId.Equals(UserId)).SingleOrDefault();

                    if (dbUser != null)
                    {
                        dbUser.UserPassword = password;
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