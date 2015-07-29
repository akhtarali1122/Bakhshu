using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ShopOnline.Models;

namespace ShopOnline.Common
{
    public static class DbUtils
    {
        public static shoponlineDataContext Create()
        {
            try
            {
                String CONN_STRING = WebConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_KEY].ConnectionString;
                shoponlineDataContext db = new shoponlineDataContext(@CONN_STRING);
                return db;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static String getString(object fieldValue)
        {
            if (fieldValue == null)
                return String.Empty;
            else
                return Convert.ToString(fieldValue).Trim();
        }
        public static DateTime getDateTime(object fieldValue)
        {
            if (fieldValue == null)
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(fieldValue);
        }


        public static DateTime getDateTimeNotMinimum(object fieldValue)
        {
            if (fieldValue == null)
                return DateTime.UtcNow;
            else if (Convert.ToDateTime(fieldValue) == DateTime.MinValue)
                return DateTime.UtcNow;
            else
                return Convert.ToDateTime(fieldValue);
        }

        public static int getInt(object fieldValue)
        {
            if (fieldValue == null)
                return 0;
            else
                return Convert.ToInt32(fieldValue);
        }
        public static Int16 getInt16(object fieldValue)
        {
            if (fieldValue == null)
                return 0;
            else
                return Convert.ToInt16(fieldValue);
        }
        public static int getInt32(object fieldValue)
        {
            if (fieldValue == null)
                return 0;
            else
                return Convert.ToInt32(fieldValue);
        }
        public static Boolean getBoolean(object fieldValue)
        {
            if (fieldValue == null)
                return false;
            else
                return Convert.ToBoolean(fieldValue);
        }

        public static Int64 getInt64(object fieldValue)
        {
            if (fieldValue == null)
                return 0;
            else
                return Convert.ToInt64(fieldValue);
        }


        public static double getDouble(object fieldValue)
        {
            if (fieldValue == null || fieldValue.Equals(String.Empty))
                return 0;
            else
                return Convert.ToDouble(fieldValue);
        }
    }
}