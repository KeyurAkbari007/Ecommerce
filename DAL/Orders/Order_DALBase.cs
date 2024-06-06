using Ecommerce.Areas.Order.Models;
using Login.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Ecommerce.DAL.Orders
{
    public class Order_DALBase:DAL_Helper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);

        #region PR_SELECT_ALL_ADDRESSES_BY_USERID

        public DataTable PR_SELECT_ALL_ADDRESSES_BY_USERID(int UserID)
        {
            try
            {

                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_ADDRESSES_BY_USERID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
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

        #region PR_ADD_USER_ADDRESS
        public bool PR_ADD_USER_ADDRESS(AddressModel model)
        {

            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ADD_USER_ADDRESS");
            int UserID = Convert.ToInt32(CV.UserID().ToString());
            sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
            sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, model.Address);
            sqlDB.AddInParameter(dbCMD, "cityname", SqlDbType.VarChar, model.cityname);
            sqlDB.AddInParameter(dbCMD, "statename", SqlDbType.VarChar, model.statename);
            sqlDB.AddInParameter(dbCMD, "pincode", SqlDbType.VarChar, model.pincode);
            sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.VarChar, model.Name);

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

        #region PR_INSERT_ORDER

        public bool PR_INSERT_ORDER(int AddressID,int CartID)
        {
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_INSERT_ORDER");
            int UserID = Convert.ToInt32(CV.UserID().ToString());
            sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
            sqlDB.AddInParameter(dbCMD, "AddressID", SqlDbType.Int, AddressID);
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


        #region SELECT_SHOPDETAIL_BY_USERID 
        public DataTable SELECT_SHOPDETAIL_BY_USERID()
        {
            int UserID = Convert.ToInt32(CV.UserID().ToString());
            try
            {

                DbCommand dbCMD = sqlDB.GetStoredProcCommand("SELECT_SHOPDETAIL_BY_USERID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
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
