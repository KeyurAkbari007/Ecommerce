using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Ecommerce.DAL.AdminHome
{
    public class Admin_HomeDALBase:DAL_Helper
    {
        #region PR_DASHBOARD_COUNTS 
        public DataTable PR_DASHBOARD_COUNTS()
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_DASHBOARD_COUNTS");

            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }

            return dt;

        }
        #endregion

        #region PR_SELECT_ALL_AUDITLOG 
        public DataTable PR_SELECT_ALL_AUDITLOG()
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_AUDITLOG");

            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }

            return dt;
        }
        #endregion
    }
}
