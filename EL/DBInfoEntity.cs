using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class DBInfoEntity
    {
        public DBInfoEntity(string serverFilename, string ExeTimeout)
        {
            this.serverFilename = serverFilename; 
            SQLConnString = CreateSQLConnString();
            ExecutionTimeout = ExeTimeout;
            ServerPath = (System.IO.Directory.GetCurrentDirectory().ToString().Replace("bin\\Debug", "DBLite").Contains("DBLite") ? System.IO.Directory.GetCurrentDirectory().ToString().Replace("bin\\Debug", "DBLite") : System.IO.Directory.GetCurrentDirectory().ToString().Replace("bin\\Debug", "DBLite") + "\\DBLite") + "\\" + serverFilename;

        }
        public string serverFilename { get; private set; }
        public string DatabaseName { get; private set; }
        public string UserID { get; private set; }
        public string Password { get; private set; }
        public int ConnectionTimeout { get; set; }
        public int CommandTimeout { get; set; }
        public string ExecutionTimeout { get; set; }
        public string SQLConnString { get; private set; } 
        public string ServerPath { get; set; }
        private string CreateSQLConnString()
        {
            string connectionString = $"Data Source={serverFilename};Version=3;";
            return connectionString;
        }
    }
}
