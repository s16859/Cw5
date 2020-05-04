using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Cw5.Models;

namespace Cw5.Services
{
    interface IStudentsDbService
    {
        public List<object[]> ExecuteSelect(SqlCommand command);
        public void ExecuteInsert(SqlCommand command);
        public SqlConnection GetConnection();
    }
}
