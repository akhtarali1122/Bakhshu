using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Common;
using ShopOnline.Models;

namespace ShopOnline.Models
{
    public partial class Shop_New
    {
        public List<Shop_New> getAllNews()
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_News.ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Shop_New GetShopNews(Int64 Id)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_News.Where(u => u.NewsId.Equals(Id)).SingleOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public Shop_New UpdateShopNews(Shop_New s_News)
        {
            try
            {
                using (shoponlineDataContext context = DbUtils.Create())
                {
                    Shop_New NewsObj = context.Shop_News.Where(u => u.NewsId.Equals(s_News.NewsId)).SingleOrDefault();
                    if (NewsObj != null)
                    {
                        NewsObj.News_HeadLine = s_News.News_HeadLine;
                        NewsObj.News_Detail = s_News.News_Detail;
                        context.SubmitChanges();
                        return NewsObj;
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public Shop_New AddNews(Shop_New SN)
        {
            try
            {

                using (shoponlineDataContext context = DbUtils.Create())
                {
                    context.Shop_News.InsertOnSubmit(SN);
                    context.SubmitChanges();
                    return SN;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Shop_New> GetShopNewsList(Int64 Id)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    return context.Shop_News.Where(s => s.User_Id.Equals(Id)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Boolean DeleteNews(Int64 News_Id)
        {
            try
            {
                using (var context = DbUtils.Create())
                {
                    Shop_New dbNews = context.Shop_News.Where(u => u.NewsId.Equals(News_Id)).SingleOrDefault();

                    if (dbNews != null)
                    {
                        context.Shop_News.DeleteOnSubmit(dbNews);
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