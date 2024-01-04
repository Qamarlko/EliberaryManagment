using System.Data.SqlClient;

namespace WebApplication3
{
    internal class sqldataadopter : SqlDataadopter
    {
        private SqlCommand cmd;

        public sqldataadopter(SqlCommand cmd)
        {
            this.cmd = cmd;
        }
    }
}