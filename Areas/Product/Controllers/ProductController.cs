using Ecommerce.DAL.Product;
using Ecommerce.DAL.User;
using Login.BAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Data;

namespace Ecommerce.Areas.Product.Controllers
{
    [CheckAccess]
    [Area("Product")]
    [Route("Product/[Controller]/[action]")]

    public class ProductController : Controller
    {
        Product_Base dal = new Product_Base();
        #region Index
        public IActionResult Index()
        {
         
            DataTable products = dal.PR_SELECT_ALL_PRODUCTS();
            
            
            if (products != null)
            {
                return View("AllProductsPage", products);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
        #endregion

        #region AddToCart
        public IActionResult AddToCart(int UserID, int ProductID)
        {
            var result = dal.PR_INSERT_CART(UserID,ProductID);
            dynamic jsonResult = result.Value;
            bool isSuccess = jsonResult.issuccess;
            bool ismatched = jsonResult.ismatched;

            if (isSuccess)
            {
                if (!ismatched)
                {
                    TempData["insertmsg"] = "Insert Successfully.";
                    SEC_DAL sc = new SEC_DAL();
                    int count = sc.PR_GET_CART_ITEMS_COUNT_BY_USERID(int.Parse(HttpContext.Session.GetString("UserID")));
                    if (count != -1)
                    {
                        HttpContext.Session.SetString("DefaultUserCartCOunt", count.ToString());
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "something wrong.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
               
                if (!ismatched)
                {
                    TempData["error"] = "something wrong.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "already added.";
                    return RedirectToAction("Index");
                }
            }

        }
        #endregion

        #region SELECT_ALL_CART_ITEMS
        public IActionResult Cart_Items()
        {
            int userid = Convert.ToInt32(@CV.UserID().ToString());
            DataTable CartItems = dal.SELECT_ALL_CART_ITEMS_BY_USERID(userid);
            Console.WriteLine(CartItems);


            if (CartItems != null)
            {
                
                    return View("AllCartItems", CartItems);
            }
            else
            {
                return RedirectToAction("Index", "Product");

            }
        }
        #endregion

        #region DeleteFromCart
        public IActionResult Delete_From_Cart(int CartID)
        {
            bool IsSuccess = dal.PR_DELETE_CART_BY_USERID(CartID, int.Parse(HttpContext.Session.GetString("UserID")));

            if (IsSuccess)
            {
                SEC_DAL sc = new SEC_DAL();
                int count = sc.PR_GET_CART_ITEMS_COUNT_BY_USERID(int.Parse(HttpContext.Session.GetString("UserID")));
                if (count != -1)
                {
                    HttpContext.Session.SetString("DefaultUserCartCOunt", count.ToString());
                }
                return RedirectToAction("Cart_Items", "Product");
            }
            else
            {
                return RedirectToAction("Cart_Items", "Product");
            }
        }
        #endregion
    }
}
