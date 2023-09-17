using System;
using System.Data;

namespace WebApplication3
{
    internal class sqlConnection
    {
        private string strcon;

        public sqlConnection(string strcon)
        {
            this.strcon = strcon;
        }

        public ConnectionState State { get; internal set; }

        internal void open()
        {
            throw new NotImplementedException();
        }
    }
}