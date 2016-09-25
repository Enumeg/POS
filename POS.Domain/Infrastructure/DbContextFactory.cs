using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Infrastructure
{
    public class DbContextFactory
    {
        static PosContext context;

        public static PosContext GetContext(int tenantId = 0)
        {            
            if (context == null)
            {
                context = new PosContext();
                if (tenantId > 0)
                {
                    context.Database.Connection.Open();
                    var storeConnection = ((SqlConnection)context.Database.Connection);
                    new SqlCommand(string.Format("set CONTEXT_INFO {0}", tenantId), storeConnection).
                        ExecuteNonQuery();
                }
            }
            return context;
        }
        public static void SetContext(PosContext Context)
        {
            context = Context;
        }
    }
}
