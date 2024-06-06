using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using NuGet.Protocol.Plugins;

namespace Ecommerce.DAL.AdminProductsCategories
{
    public class Admin_Products_Categories_DALBase:DAL_Helper
    {
        #region PR_SELECT_ALL_CATEGORIES
        public DataTable PR_SELECT_ALL_CATEGORIES()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_CATEGORIES");

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

        #region PR_INSERT_UPDATE_CATEGORY_BY_ADMIN
        public bool PR_INSERT_UPDATE_CATEGORY_BY_ADMIN(int? CategoryID, String CategoryName,String Discription,bool IsDeleted )
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD;
                if (CategoryID == null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_INSERT_CATEGORY_BY_ADMIN");
                }
                else
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_CATEGORY_UPDATE_PK");
                    sqlDB.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, CategoryID);
                    sqlDB.AddInParameter(dbCMD, "IsDeleted", SqlDbType.Bit, IsDeleted);

                }
                sqlDB.AddInParameter(dbCMD, "CategoryName", SqlDbType.VarChar, CategoryName);
                sqlDB.AddInParameter(dbCMD, "Discription", SqlDbType.VarChar, Discription);
                if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD)))
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(Exception e)
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD= sqlDB.GetStoredProcCommand("LogAuditInfo");
                if (CategoryID == null)
                { 
                    sqlDB.AddInParameter(dbCMD, "TableName", SqlDbType.VarChar, "Category");
                    sqlDB.AddInParameter(dbCMD, "Operation", SqlDbType.VarChar, "Insert");

                }
                else
                {
                    sqlDB.AddInParameter(dbCMD, "TableName", SqlDbType.VarChar, "Category");
                    sqlDB.AddInParameter(dbCMD, "Operation", SqlDbType.VarChar, "Update");
                }
                sqlDB.AddInParameter(dbCMD, "IsDone", SqlDbType.Bit, 0);
                sqlDB.ExecuteNonQuery(dbCMD);
                return false;
            }
            
          
        }
        #endregion

        #region  PR_CATEGORY_SELECT_BY_PK
        public DataTable PR_CATEGORY_SELECT_BY_PK(int CategoryID)
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CATEGORY_SELECT_BY_PK");
            sqlDB.AddInParameter(dbCMD, "CategoryID", SqlDbType.VarChar, CategoryID);

            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }

            return dt;

        }
        #endregion

        #region PR_CATEGORY_SOFT_HARD_DELETE_BY_ID

        public bool PR_CATEGORY_SOFT_HARD_DELETE_BY_ID(int CategoryID)
        {
            try {
                DataTable dt = PR_CATEGORY_SELECT_BY_PK(CategoryID);

                DataRow row = dt.Rows[0];

                // Check the value of the "IsDeleted" column
                bool isDeleted = Convert.ToBoolean(row["IsDeleted"]);

                // Now you can use the 'isDeleted' variable as needed
                if (isDeleted)
                {
                    SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CATEGORY_HARD_DELETE_BY_ID");
                    sqlDB.AddInParameter(dbCMD, "CategoryID", SqlDbType.VarChar, CategoryID);
                    if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CATEGORY_SOFT_DELETE_BY_ID");
                    sqlDB.AddInParameter(dbCMD, "CategoryID", SqlDbType.VarChar, CategoryID);
                    if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                DataTable dt = PR_CATEGORY_SELECT_BY_PK(CategoryID);

                DataRow row = dt.Rows[0];

                // Check the value of the "IsDeleted" column
                bool isDeleted = Convert.ToBoolean(row["IsDeleted"]);
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("LogAuditInfo");
                if (isDeleted)
                {
                    sqlDB.AddInParameter(dbCMD, "TableName", SqlDbType.VarChar, "Category");
                    sqlDB.AddInParameter(dbCMD, "Operation", SqlDbType.VarChar, "HARD DELETE");
                   
                }
                else
                {
                    sqlDB.AddInParameter(dbCMD, "TableName", SqlDbType.VarChar, "Category");
                    sqlDB.AddInParameter(dbCMD, "Operation", SqlDbType.VarChar, "SOFT DELETE");
                }
                sqlDB.AddInParameter(dbCMD, "IsDone", SqlDbType.Bit, 0);
                sqlDB.ExecuteNonQuery(dbCMD);
                return false;
            } 
        }
        #endregion

        #region PR_SELECT_ALL_DELETED_CATEGORY

        public DataTable PR_SELECT_ALL_DELETED_CATEGORY()
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_DELETED_CATEGORY");
            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }

            return dt;
        }
        #endregion

        #region PR_SELECT_ALL_NON_DELETED_CATEGORY
        public DataTable PR_SELECT_ALL_NON_DELETED_CATEGORY()
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_NON_DELETED_CATEGORY");
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