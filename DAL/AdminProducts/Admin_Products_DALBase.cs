using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Ecommerce.Models;
using Ecommerce.Areas.AdminProducts.Models;

namespace Ecommerce.DAL.AdminProducts
{
    public class Admin_Products_DALBase:DAL_Helper
    {
  

        #region PR_SELECT_ALL_ADMIN_PRODUCTS
        public DataTable PR_SELECT_ALL_ADMIN_PRODUCTS()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_ADMIN_PRODUCTS");

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

        #region PR_CATEGORY_COMBOBOX
        public DataTable PR_CATEGORY_COMBOBOX()
        {

            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CATEGORY_COMBOBOX");
            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }

            return dt;

        }
        #endregion

        #region PR_INSERT_PRODUCT
        public bool ProductSave(AdminProductModel productModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (productModel.ProductID == 0)
                {
                    if (productModel.File != null)
                    {
                        string FilePath = "wwwroot\\images";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileNameWithPath = Path.Combine(path, productModel.File.FileName);

                        productModel.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + productModel.File.FileName;

                        using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            productModel.File.CopyTo(fileStream);
                        }
                    }
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_INSERT_PRODUCT");
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, productModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductName", DbType.String, productModel.ProductName);
                    sqlDatabase.AddInParameter(dbCommand, "@Price", DbType.Int32, productModel.Price);
                    sqlDatabase.AddInParameter(dbCommand, "@DiscountPercentage", DbType.Double, productModel.DiscountPercentage);
                    sqlDatabase.AddInParameter(dbCommand, "@Rating", DbType.Int32, productModel.Rating);
                    sqlDatabase.AddInParameter(dbCommand, "@Stock", DbType.Int32, productModel.Stock);
                    sqlDatabase.AddInParameter(dbCommand, "@Brand", DbType.String, productModel.Brand);
                    sqlDatabase.AddInParameter(dbCommand, "@PhotoPath", DbType.String, productModel.PhotoPath);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    if (productModel.File != null)
                    {
                        string FilePath = "wwwroot\\images";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileNameWithPath = Path.Combine(path, productModel.File.FileName);

                        productModel.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + productModel.File.FileName;

                        using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            productModel.File.CopyTo(fileStream);
                        }
                    }

                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UPDATE_PRODUCT");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, productModel.ProductID);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductName", DbType.String, productModel.ProductName);
                    sqlDatabase.AddInParameter(dbCommand, "@Price", DbType.Int32, productModel.Price);
                    sqlDatabase.AddInParameter(dbCommand, "@DiscountPercentage", DbType.Double, productModel.DiscountPercentage);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, productModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@PhotoPath", DbType.String, productModel.PhotoPath);
                    sqlDatabase.AddInParameter(dbCommand, "@Stock", DbType.Int32, productModel.Stock);
                    sqlDatabase.AddInParameter(dbCommand, "@Rating", DbType.Int32, productModel.Rating);
                    sqlDatabase.AddInParameter(dbCommand, "@Brand", DbType.String, productModel.Brand);
                    sqlDatabase.AddInParameter(dbCommand, "@isDeleted", SqlDbType.Bit, productModel.isDeleted);

                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
             }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region PR_SELECT_BY_ID_PRODUCT

        public AdminProductModel PR_SELECT_BY_ID_PRODUCT(int ProductID)
        {
            AdminProductModel productModel = new AdminProductModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDatabase.GetStoredProcCommand("PR_SELECT_BY_ID_PRODUCT");


                sqlDatabase.AddInParameter(dbCMD, "@ProductID", DbType.Int32, ProductID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCMD))
                {
                    dataTable.Load(dataReader);
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    productModel.ProductID = Convert.ToInt32(dataRow["ProductID"]);
                    productModel.ProductName = dataRow["ProductName"].ToString();
                    productModel.Price = Convert.ToInt32(dataRow["Price"]);
                    productModel.DiscountPercentage = Convert.ToDouble(dataRow["DiscountPercentage"]);
                    productModel.PhotoPath = dataRow["PhotoPath"].ToString();
                    productModel.Brand = dataRow["Brand"].ToString();
                    productModel.isDeleted = Convert.ToBoolean(dataRow["isDeleted"]);
                    productModel.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    productModel.CategoryName = dataRow["CategoryName"].ToString();
                    productModel.Rating = Convert.ToInt32(dataRow["Rating"].ToString());
                    productModel.Stock = Convert.ToInt32(dataRow["Stock"].ToString());
                    productModel.Created = Convert.ToDateTime(dataRow["Created"]);
                    productModel.Modified = Convert.ToDateTime(dataRow["Modified"]);
                }
                return productModel;
            }
            catch(Exception e)
            {
                return null;
            }
          

        }
        #endregion

        public DataTable PR_SELECT_ALL_DELETED_PRODUCT()
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_DELETED_PRODUCT");
            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }

            return dt;
        } 
        public DataTable PR_SELECT_ALL_NON_DELETED_PRODUCT()
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SELECT_ALL_NON_DELETED_PRODUCT");
            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }

            return dt;
        }


        #region PR_CATEGORY_SOFT_HARD_DELETE_BY_ID

        public bool PR_PRODUCT_SOFT_HARD_DELETE_BY_ID(int ProductID)
        {
            try
            {
                AdminProductModel dt = PR_SELECT_BY_ID_PRODUCT(ProductID);

                // Check the value of the "IsDeleted" column
                bool isDeleted = Convert.ToBoolean(dt.isDeleted);

                // Now you can use the 'isDeleted' variable as needed
                if (isDeleted)
                {
                    SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_PRODUCT_HARD_DELETE_BY_ID");
                    sqlDB.AddInParameter(dbCMD, "ProductID", SqlDbType.VarChar, ProductID);
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
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_PRODUCT_SOFT_DELETE_BY_ID");
                    sqlDB.AddInParameter(dbCMD, "ProductID", SqlDbType.VarChar, ProductID);
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
            catch (Exception e)
            {
            /*    DataTable dt = PR_CATEGORY_SELECT_BY_PK(CategoryID);

                DataRow row = dt.Rows[0];

                // Check the value of the "IsDeleted" column
                bool isDeleted = Convert.ToBoolean(row["IsDeleted"]);
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("LogAuditInfo");
                if (isDeleted)
                {
                    sqlDB.AddInParameter(dbCMD, "Category", SqlDbType.VarChar, "Category");
                    sqlDB.AddInParameter(dbCMD, "Hard Delete", SqlDbType.VarChar, "HARD DELETE");

                }
                else
                {
                    sqlDB.AddInParameter(dbCMD, "Category", SqlDbType.VarChar, "Category");
                    sqlDB.AddInParameter(dbCMD, "Soft Delete", SqlDbType.VarChar, "SOFT DELETE");
                }
                sqlDB.AddInParameter(dbCMD, "IsDone", SqlDbType.Bit, 0);
                sqlDB.ExecuteNonQuery(dbCMD);*/
                return false;
            }
        }
        #endregion

    }
}
