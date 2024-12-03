using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public class NewConnectionUtility
    {
        private readonly IConfiguration _configuration;

        public NewConnectionUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConfiguration()
        {
            var connection = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connection);
        }
    }
}
