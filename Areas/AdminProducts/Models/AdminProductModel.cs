using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Areas.AdminProducts.Models
{
    public class AdminProductModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public double DiscountPercentage { get; set; }

        public int Rating { get; set; }

        public int Stock { get; set; }

        public string Brand { get; set; }

        public IFormFile? File { get; set; }
        public string? PhotoPath { get; set; }

        public string? CategoryName { get; set; }
        public int CategoryID { get; set; }

        public bool isDeleted { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
   
}
