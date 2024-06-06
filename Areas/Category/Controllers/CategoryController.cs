using Ecommerce.DAL.Category;
using Login.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Ecommerce.Areas.Category.Controllers
{
    [CheckAccess]
    [Area("Category")]
    [Route("Category/[Controller]/[action]")]
    public class CategoryController : Controller
    {
        Category_DAL dal = new Category_DAL();
        public IActionResult Index()
        {
            DataTable Categories = dal.SELECT_ALL_USER_CATEGORY();

            if (Categories != null)
            {
                return View("AllUserCategory", Categories);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

       
    }
}
