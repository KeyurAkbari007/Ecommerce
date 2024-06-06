namespace Ecommerce.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public double? DiscDiscountPercentage { get; set; }

        public int? Rating { get; set; }

        public int Stock { get; set; }

        public string Brand { get; set; }

        public string PhotoPath { get; set; }

        public string CategoryName { get; set; }

    }
    public class CartModel
    {
        public int  CartID { get; set; }

        public  int ProductID { get; set; }

        public int UserID { get; set; }

        public int Quantity { get; set; }
    }
    public class SelectAllCartModel
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }

        public bool IsPurchased { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

         public double? DiscDiscountPercentage { get; set; }

        public string PhotoPath { get; set; }

    }
}
