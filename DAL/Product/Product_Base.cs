using Ecommerce.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Ecommerce.DAL.Product
{
    public class Product_Base:DAL_Helper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
        #region PR_SELECT_ALL_PRODUCTS

        public DataTable PR_SELECT_ALL_PRODUCTS()
        {
            try
            {
             
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_PRODUCTS");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception err)
            {
                return null;
            }
        }
        #endregion

        #region PR_SELECT_CART_PRODUCT_BY_USERID_PRODUCTID
        public DataTable PR_SELECT_CART_PRODUCT_BY_USERID_PRODUCTID(int UserID , int ProductID)
        {
         
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_CART_PRODUCT_BY_USERID_PRODUCTID");
            sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
            sqlDB.AddInParameter(dbCMD, "ProductID", SqlDbType.Int, ProductID);
            DataTable dataTable = new DataTable();
            using (IDataReader dataReader = sqlDB.ExecuteReader(dbCMD))
            {
                dataTable.Load(dataReader);
            }
         
            return dataTable;

        }
        #endregion



        #region PR_INSERT_CART
        public JsonResult PR_INSERT_CART(int UserID, int ProductID)
        {
            DataTable CartProducts = PR_SELECT_CART_PRODUCT_BY_USERID_PRODUCTID(UserID, ProductID);
            bool isMatch = false;
            foreach (DataRow row in CartProducts.Rows)
            {
                int cartUserID = Convert.ToInt32(row["UserID"]);
                int cartProductID = Convert.ToInt32(row["ProductID"]);

                if (cartUserID == UserID && cartProductID == ProductID)
                {
                    isMatch = true;
                    break;  // No need to continue checking once a match is found
                }
            }
            if (!isMatch)
            {
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_INSERT_CART");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "ProductID", SqlDbType.Int, ProductID);
                if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD)))
                {
                    return new JsonResult(new { issuccess = true, ismatched = false });
                }
                else
                {
                    return new JsonResult(new { issuccess = false, ismatched = false });
                }
            }
            else
            {
                return new JsonResult(new { issuccess = false, ismatched = true });
            }

           
        }
        #endregion

        #region SELECT_ALL_CART_ITEMS_BY_USERID
        public DataTable SELECT_ALL_CART_ITEMS_BY_USERID(int userid)
        {
            try
            {

                DbCommand dbCMD = sqlDB.GetStoredProcCommand("SELECT_ALL_CART_ITEMS_BY_USERID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, userid);
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception err)
            {
                return null;
            }
        }
        #endregion

        #region PR_DELETE_CART_BY_USERID
        public bool PR_DELETE_CART_BY_USERID(int CartID,int userid)
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_DELETE_CART_BY_USERID");
            sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, userid);
            sqlDB.AddInParameter(dbCMD, "CartID", SqlDbType.Int, CartID);
            if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
