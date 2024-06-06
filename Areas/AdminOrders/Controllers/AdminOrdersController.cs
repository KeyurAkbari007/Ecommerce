using Ecommerce.Areas.AdminCategories.Models;
using Ecommerce.Areas.AdminProducts.Models;
using Ecommerce.DAL.AdminOrders;
using Login.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace Ecommerce.Areas.AdminOrders.Controllers
{
    [CheckAccess]
    [Area("AdminOrders")]
    [Route("AdminOrders/[Controller]/[action]")]
    public class AdminOrdersController : Controller
    {
        Admin_OrdersDAL dal = new Admin_OrdersDAL();
        public IActionResult Index()
        {
            DataTable dt = dal.PR_SELECT_ALL_ORDER();
            

            return View("AllOrdersDetails",dt);
        }

        #region AddEditOrderFormPage
        public IActionResult AddEditFormPage(int OrderID)
        {
            /* DataTable Categories = dal.PR_CATEGORY_COMBOBOX();

             List<Category_DropDown> list = new List<Category_DropDown>();

             foreach (DataRow dr in Categories.Rows)
             {
                 Category_DropDown dlist = new Category_DropDown();
                 dlist.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                 dlist.CategoryName = dr["CategoryName"].ToString();
                 list.Add(dlist);
             }
             ViewBag.CategoryList = list;*/
/*
            if (OrderID > 0)
            {
                AdminProductModel productModel = dal.PR_SELECT_BY_ID_PRODUCT(ProductID);
                return View("Add_Edit_Product", productModel);

            }
*/
            return View("Add_Edit_Order");
        }
        #endregion
        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string newStatus)
        {
            // Perform the database update based on orderId and newStatus
            bool IsSuccess = dal.PR_CHANGE_STATUS_BY_ID(orderId, newStatus);
            if (IsSuccess)
            {
                ViewBag.Message = "done";
                return Json(new { isSuccess = true, id = orderId,message="done" });
            }
            return RedirectToAction("Index");

            // Return a response, if needed


        }
    }
}
