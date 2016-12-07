using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkAnalyzePolls
{
    static class Settings
    {
        public static int ApplicationId
        {
            get { return Int32.Parse(ConfigurationSettings.AppSettings["appId"]); }
        }

        public static int UserId
        {
            get { return Int32.Parse(ConfigurationSettings.AppSettings["userId"]); }
        }
    }
}
