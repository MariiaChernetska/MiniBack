using ProcTest.Providers;
using System;
using System.Data.SqlClient;

namespace ProcTest.Services
{
    public class BaseService : IDisposable
    {
        private ConnectionStringProvider _provider;

        protected SqlConnection Connection { get; private set; }
        
        public BaseService()
        {
            _provider = new ConnectionStringProvider();
            Connection = new SqlConnection(_provider.ConnectionString);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}