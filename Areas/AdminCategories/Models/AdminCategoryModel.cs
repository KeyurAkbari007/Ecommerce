using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Areas.AdminCategories.Models
{
    public class AdminCategory
    {
        public int? CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public int ProductCount { get; set; }

        [Required]
        public String Discription { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public bool IsDeleted { get; set; }


    }
    public class Category_DropDown
    {
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }

    }
}
