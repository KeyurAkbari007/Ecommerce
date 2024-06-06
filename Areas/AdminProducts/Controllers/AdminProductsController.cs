using Ecommerce.Areas.AdminCategories.Models;
using Ecommerce.Areas.AdminProducts.Models;
using Ecommerce.DAL.AdminProducts;
using Ecommerce.DAL.AdminProductsCategories;
using Ecommerce.DAL.Product;
using Ecommerce.DAL.User;
using Ecommerce.Models;
using Login.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Ecommerce.Areas.AdminProducts.Controllers
{
    [CheckAccess]
    [Area("AdminProducts")]
    [Route("AdminProducts/[Controller]/[action]")]
    public class AdminProductsController : Controller
    {
        Admin_Products_DAL dal = new Admin_Products_DAL();
        #region productPage
        public IActionResult Index()
        {
          
            DataTable products = dal.PR_SELECT_ALL_ADMIN_PRODUCTS();


            if (products != null)
            {
            
                return View("AllProducts", products);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
        #endregion

        #region AddEditProductFormPage
        public IActionResult AddEditFormPage(int ProductID)
        {
            DataTable Categories = dal.PR_CATEGORY_COMBOBOX();

            List<Category_DropDown> list = new List<Category_DropDown>();

            foreach (DataRow dr in Categories.Rows)
            {
                Category_DropDown dlist = new Category_DropDown();
                dlist.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                dlist.CategoryName = dr["CategoryName"].ToString();
                list.Add(dlist);
            }
            ViewBag.CategoryList = list;

            if (ProductID>0)
            {
                AdminProductModel productModel = dal.PR_SELECT_BY_ID_PRODUCT(ProductID);
                return View("Add_Edit_Product", productModel);

            }
            return View("Add_Edit_Product");
        }
        #endregion


        #region Save Product
        public  IActionResult SaveProduct(AdminProductModel productModel)
        {
      
                if (dal.ProductSave(productModel))
                {
                    return RedirectToAction("Index");

                }
           
            return RedirectToAction("Index");
        }
        #endregion

        #region SelectDeletedProduct
        public IActionResult SelectDeletedProduct()
        {
            DataTable Categories = dal.PR_SELECT_ALL_DELETED_PRODUCT();

            if (Categories != null)
            {
                return View("AllProducts", Categories);
            }
            else
            {
                return RedirectToAction("Index", "AdminHome", new { area = "AdminHome" });

            }
        }   
        public IActionResult SelectNonDeletedProduct()
        {
            DataTable Categories = dal.PR_SELECT_ALL_NON_DELETED_PRODUCT();

            if (Categories != null)
            {
                return View("AllProducts", Categories);
            }
            else
            {
                return RedirectToAction("Index", "AdminHome", new { area = "AdminHome" });

            }
        }


        #endregion

        #region DeleteProduct
        public IActionResult DeleteProduct(int ProductID)
        {
            bool IsSuccess = dal.PR_PRODUCT_SOFT_HARD_DELETE_BY_ID(ProductID);

            if (IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "AdminHome", new { area = "AdminHome" });
            }

        }
        #endregion
    }
}
