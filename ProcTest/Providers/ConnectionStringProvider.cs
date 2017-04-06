using System.Configuration;

namespace ProcTest.Providers
{
    public class ConnectionStringProvider
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }
    }
}