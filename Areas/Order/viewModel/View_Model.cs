using Ecommerce.Areas.Order.Models;
using System.Data;

namespace Ecommerce.Areas.Order.viewModel
{
    public class View_Model
    {
        public DataTable cartData { get; set; }
        public DataTable addressData { get; set; }
        public AddressModel AddressModell { get; set; }
    }
}
