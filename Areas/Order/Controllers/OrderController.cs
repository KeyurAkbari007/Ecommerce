using Ecommerce.Areas.Order.Models;
using Ecommerce.Areas.Order.viewModel;
using Ecommerce.DAL.Orders;
using Ecommerce.DAL.Product;
using Login.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;


namespace Ecommerce.Areas.Order.Controllers
{
    [CheckAccess]
    [Area("Order")]
    [Route("Order/[Controller]/[action]")]
    public class OrderController : Controller
    {
        Order_DAL orderdal = new Order_DAL();


        public IActionResult Index()
        {
            int userid = Convert.ToInt32(@CV.UserID().ToString());
            Product_Base dal = new Product_Base();
            DataTable CartItems = dal.SELECT_ALL_CART_ITEMS_BY_USERID(userid);
            DataTable Addressess = orderdal.PR_SELECT_ALL_ADDRESSES_BY_USERID(userid);

            // Assuming your ViewModel class is named ViewModel
          /*  var viewModel = new View_Model
            {
                cartData = CartItems,
                addressData = Addressess
            };*/
            if (CartItems != null)
            {
                ViewBag.CartData = CartItems;
                ViewBag.AddressData = Addressess;

                return View("AddressPage");
            }
            else
            {
                return RedirectToAction("Index", "Product", new { area = "Product" });

            }
        }


        public IActionResult ADD_USER_ADDRESS(AddressModel view_Model)
        {
            bool IsSuccess = orderdal.PR_ADD_USER_ADDRESS(view_Model);
            if (IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Product", new { area = "Product" });
            }
        }

        #region ShopDetailPage
        public IActionResult ShopDetailPage()
        {

            int userid = Convert.ToInt32(@CV.UserID().ToString());
            DataTable OrdersItems = orderdal.SELECT_SHOPDETAIL_BY_USERID();
            if (OrdersItems != null)
            {
                return View(OrdersItems);
            }
            else
            {
                return RedirectToAction("Index", "Product", new { area = "Product" });

            }
        }
        #endregion

        public IActionResult Save_Order(List<string> cartDataIDs, int addressID)
        {
            // Ensure that cartDataIDs and addressID are not null
            if (cartDataIDs != null && addressID > 0)
            {
                try
                {
                    // Your logic here
                    foreach (var id in cartDataIDs)
                    {
                        bool IsSuccess = orderdal.PR_INSERT_ORDER(addressID, int.Parse(id));
                        if (!IsSuccess)
                        {
                            // If insertion fails for any cart item, redirect to a failure page or handle as needed
                            return RedirectToAction("Index", "Product", new { area = "Product" });
                        }
                    }

                    // If all insertions are successful, redirect to a success page or handle as needed
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception or handle as needed
                    return RedirectToAction("Index", "Product", new { area = "Product" });
                }
            }
            else
            {
                // Redirect to an error page or handle as needed if cartDataIDs or addressID is null or invalid
                return RedirectToAction("Index", "Product", new { area = "Product" });
            }
        }
    }
}
