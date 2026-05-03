using System.Configuration;

namespace Inventory.CrossCutting.Configuration
{
    public static class AppSettings
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["DefaultConnection"]
                .ConnectionString;
        }
    }
}