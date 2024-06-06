namespace Ecommerce.DAL
{
    public class DAL_Helper
    {
        public static string ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConnectingString");

    }
}
