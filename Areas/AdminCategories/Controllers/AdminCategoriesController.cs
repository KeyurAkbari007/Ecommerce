using Ecommerce.Areas.AdminCategories.Models;
using Ecommerce.DAL.AdminProductsCategories;
using Login.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace Ecommerce.Areas.AdminCategories.Controllers
{
    [CheckAccess]
    [Area("AdminCategories")]
    [Route("AdminCategories/[Controller]/[action]")]
    public class AdminCategoriesController : Controller
    {
        #region Admin_Categories
        public IActionResult Admin_Categories()
        {
            Admin_Products_Categories_DALBase dal = new Admin_Products_Categories_DALBase();
            DataTable Categories = dal.PR_SELECT_ALL_CATEGORIES();


            if (Categories != null)
            {
               
                return View("Admin_Categories", Categories);
            }
            else
            {
                return RedirectToAction("Index", "AdminHome", new { area = "AdminHome" });

            }

        }
        #endregion

        #region Add_Edit_CategoryPage
        public IActionResult Add_Edit_Category(int CategoryID)
        {
            if (ModelState.IsValid)
            {
                if (CategoryID > 0)
                {
                    try
                    {
                        Admin_Products_Categories_DALBase dal = new Admin_Products_Categories_DALBase();

                        // Fetch data from the database
                        DataTable dt = dal.PR_CATEGORY_SELECT_BY_PK(CategoryID);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Instantiate an AdminCategories object
                            AdminCategory LC = new AdminCategory();

                            // Populate AdminCategories properties from the DataTable
                            LC.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
                            LC.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                            LC.Discription = Convert.ToString(dt.Rows[0]["Discription"]);
                            LC.IsDeleted = Convert.ToBoolean(dt.Rows[0]["isDeleted"]);

                            // Pass the AdminCategories object to the view

                            return View(LC);
                        }
                        else
                        {
                            // Handle the case where no data is found for the given CategoryID
                            return NotFound(); // You can customize this response based on your needs
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log, or redirect to an error page
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();

            }

        }


        #endregion

        #region SaveCategory
        public IActionResult PR_INSERT_UPDATE_CATEGORY_BY_ADMIN(AdminCategory data)
        {
            Admin_Products_Categories_DALBase dal = new Admin_Products_Categories_DALBase();
            bool IsSuccess = dal.PR_INSERT_UPDATE_CATEGORY_BY_ADMIN(data.CategoryID, data.CategoryName, data.Discription,data.IsDeleted);

            if (IsSuccess)
            {
                return RedirectToAction("Admin_Categories");
            }
            else
            {
                TempData["Error"] = "something wrong";
                return RedirectToAction("Add_Edit_Category");
            }
        }
        #endregion

        #region DeleteCategoryTemporary
        public IActionResult DeleteCategoryTemporary(int CategoryID)
        {
            Admin_Products_Categories_DALBase dal = new Admin_Products_Categories_DALBase();
            bool IsSuccess = dal.PR_CATEGORY_SOFT_HARD_DELETE_BY_ID(CategoryID);

            if (IsSuccess)
            {
                return RedirectToAction("Admin_Categories");
            }
            else
            {
                return RedirectToAction("Index", "AdminHome",new { area = "AdminHome" });
            }
             
        }
        #endregion

        #region SelectDeletedCategory
        public IActionResult SelectDeletedCategory()
        {
            Admin_Products_Categories_DALBase dal = new Admin_Products_Categories_DALBase();
            DataTable Categories = dal.PR_SELECT_ALL_DELETED_CATEGORY();

            if (Categories != null)
            {
                return View("Admin_Categories", Categories);
            }
            else
            {
                return RedirectToAction("Index", "AdminHome", new { area = "AdminHome" });

            }
        }


        #endregion

        #region SelectNonDeletedCategory
        public IActionResult SelectNonDeletedCategory()
        {
            Admin_Products_Categories_DALBase dal = new Admin_Products_Categories_DALBase();
            DataTable Categories = dal.PR_SELECT_ALL_NON_DELETED_CATEGORY();

            if (Categories != null)
            {
                return View("Admin_Categories", Categories);
            }
            else
            {
                return RedirectToAction("Index", "AdminHome", new { area = "AdminHome" });

            }
        }

        #endregion
    }
}
