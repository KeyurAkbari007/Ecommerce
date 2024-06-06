using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace Ecommerce.DAL.AdminOrders
{
    public class Admin_OrdersDALBase:DAL_Helper
    {
        #region PR_SELECT_ALL_ORDER
        public DataTable PR_SELECT_ALL_ORDER()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_ORDER");

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
        #region PR_CHANGE_STATUS_BY_ID
        public bool PR_CHANGE_STATUS_BY_ID(int orderid , string newStatus)
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CHANGE_STATUS_BY_ID");

            sqlDB.AddInParameter(dbCMD, "OrderID", SqlDbType.Int, orderid);
            sqlDB.AddInParameter(dbCMD, "Status", SqlDbType.VarChar, newStatus);
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
