namespace Ecommerce.Areas.Order.Models
{
    public class AddressModel
    {
        public int AddressID { get; set; }

        public int UserID { get; set; }

        public String Address { get; set; }

        public String cityname { get; set; }

        public String statename { get; set; }

        public String pincode { get; set; }

        public String Name { get; set; }
        public int SelectedAddressID { get; set; } = 0;


    }
}
