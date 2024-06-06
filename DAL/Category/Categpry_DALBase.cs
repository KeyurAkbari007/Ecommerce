using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Ecommerce.DAL.Category
{
    public class Categpry_DALBase:DAL_Helper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
        #region SELECT_ALL_USER_CATEGORY
        public DataTable SELECT_ALL_USER_CATEGORY()
        {
            try
            {
               
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("SELECT_ALL_USER_CATEGORY");

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

     
    }
}
