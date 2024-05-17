 
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace EL
{
    public static class StaticCache
    {
        private static readonly string InifilePath = @"C:\SmartLoading\SmartLoading.ini";
        private static readonly string DefaultLogPath = @"C:\SmartLoading\Log";

        private static readonly Encoding encoding = Encoding.GetEncoding("Shift_JIS");
        public static DBInfoEntity DBInfoEntity { get; private set; }
        public static LogInfoEntity LogInfo { get; private set; }

        private static readonly object _lockObject = new object();

        public static string DBPath { get; set; }
        

        public static void SetIniInfo()
        { 
            if (!System.IO.Directory.Exists(Path.GetDirectoryName(InifilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(InifilePath));
            }
            if (!System.IO.Directory.Exists(DefaultLogPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DefaultLogPath));
            }
            
            LogInfo = GetLogInfo();
            DBInfoEntity = GetDBInfo();
        }
        public static DBInfoEntity GetDBInfo()
        { 
            var serverFileName = GetValue("Database", "serverFileName").ToString(); 
            var ExeTimeOut = GetValue("Database", "ExeTimeout").ToString();

            return new DBInfoEntity(serverFileName, ExeTimeOut)
            {
            
            };
        }
        private static string GetValue(string category, string key)
        {
            IniFileReader ifr = new IniFileReader(InifilePath);
            return ifr.IniReadValue(category, key);
        }
        public static LogInfoEntity GetLogInfo()
        {

            return new LogInfoEntity()
            {
                Path = GetValue("Log", "LOG_PATH").ToStringOrNull() ?? DefaultLogPath,
                Level = GetValue("Log", "LOG_LEVEL").ToInt32(3) 
            };
        }
    }
    public class IniFileReader
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);
         
        public IniFileReader(string INIPath)
        {
            path = INIPath;
        } 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        } 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
            return temp.ToString();
        }
    }
}
