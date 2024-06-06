using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace Ecommerce.DAL.User
{
    public class SEC_DALBase : DAL_Helper
    {
        #region Method: dbo_PR_SEC_User_SelectByUserNamePassword
        public DataTable dbo_PR_SEC_User_SelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SEC_User_SelectByUserNamePassword");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_SEC_USER_INSERT

        public bool PR_SEC_USER_INSERT(String UserName, String Password)
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CHECK_USER_EXIST_OR_NOT_SIGNUP");
            sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                DbCommand dbCMD1 = sqlDB.GetStoredProcCommand("PR_SEC_USER_INSERT");
                sqlDB.AddInParameter(dbCMD1, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD1, "Password", SqlDbType.VarChar, Password);
                if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD1)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region PR_GET_CART_ITEMS_COUNT_BY_USERID
        public int PR_GET_CART_ITEMS_COUNT_BY_USERID(int userid)
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_GET_CART_ITEMS_COUNT_BY_USERID");
            sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, userid);

            int cartCount = 0;

            try
            {
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // The result set contains at least one row
                        cartCount = Convert.ToInt32(dr["CartCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during database access
                // Log or throw the exception as appropriate for your application
                return -1;
            }

            return cartCount;
        }
        #endregion

    }
}
