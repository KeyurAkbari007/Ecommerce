using Ecommerce.DAL.User;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Ecommerce.Controllers
{
    public class UserController : Controller
    {
        #region LoginPage
        public IActionResult Index()
        {
            return View("LoginPage");
        }
        #endregion


        #region Login
        [HttpPost]
        public IActionResult Login(SEC_UserModel modelSEC_User)
        {
            string error = null;
            if (modelSEC_User.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelSEC_User.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Index");
            }
            else
            {
                SEC_DAL dal = new SEC_DAL();
                DataTable dt = dal.dbo_PR_SEC_User_SelectByUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                        HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid!";
                    return RedirectToAction("Index");
                }
                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    if (HttpContext.Session.GetString("IsAdmin") == "True")
                    {
                        return RedirectToAction("Index", "AdminHome", new { area = "AdminHome" });
                    }
                    SEC_DAL sc = new SEC_DAL();
                    int count = sc.PR_GET_CART_ITEMS_COUNT_BY_USERID(int.Parse(HttpContext.Session.GetString("UserID")));
                    if (count != -1)
                    {
                        HttpContext.Session.SetString("DefaultUserCartCOunt", count.ToString());
                    }
                    return RedirectToAction("Index", "Category", new { area = "Category" });
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        #endregion

        #region SignUpPage
        public IActionResult SignUp()
        {
            return View("SignUpPage");
        }
        #endregion

        #region Signup
        public IActionResult SignUpUser(SEC_UserModel data)
        {
            SEC_DALBase dal = new SEC_DALBase();
            bool IsSuccess = dal.PR_SEC_USER_INSERT(data.UserName, data.Password);

            if (IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "user allready exists";
                return RedirectToAction("SignUp");
            }
        }
        #endregion
    }
}
