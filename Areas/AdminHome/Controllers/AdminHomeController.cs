using Ecommerce.DAL.AdminHome;
using Login.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Ecommerce.Areas.AdminHome.Controllers
{
    [CheckAccess]
    [Area("AdminHome")]
    [Route("AdminHome/[Controller]/[action]")]
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            Admin_HomeDAL dal = new Admin_HomeDAL();
            DataTable dataTable = dal.PR_DASHBOARD_COUNTS();
            DataTable Modifications = dal.PR_SELECT_ALL_AUDITLOG();

            // Convert DataTable to Dictionary for simplicity
            Dictionary<string, int> dataDictionary = new Dictionary<string, int>
            {
                { "ProductCount", Convert.ToInt32(dataTable.Rows[0]["ProductCount"]) },
                { "CategoryCount", Convert.ToInt32(dataTable.Rows[0]["CategoryCount"]) },
                { "UserCount", Convert.ToInt32(dataTable.Rows[0]["UserCount"]) },

            };

            // Pass data to view using ViewBag or ViewData
            ViewBag.DashboardData = dataDictionary;


            return View("Home",Modifications);
        }
    }
}
