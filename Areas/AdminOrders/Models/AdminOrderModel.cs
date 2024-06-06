namespace Ecommerce.Areas.AdminOrders.Models
{
    public class AdminOrderModel
    {
       public  int OrderID { get; set; }
       public  int CartID { get; set; }
       public  int UserID { get; set; }
       public  int AddressID { get; set; }

       public  DateTime Created { get; set; }
       public  DateTime Modified { get; set; }
       public int ProductID { get; set; }
       public string?  ProductName { get; set; }

        public string? Status { get; set; }
         
       public string? Address { get; set; }

    }
}
