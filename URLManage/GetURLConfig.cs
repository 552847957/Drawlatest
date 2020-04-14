using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LJJSCAD.URLManage
{
    class GetURLConfig
    {
        public static void getTest()
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = "URLManage.config";
            Configuration config =
            ConfigurationManager.OpenMappedExeConfiguration(file,ConfigurationUserLevel.None); 

        }
        public static void CreateConSection()
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = "URLManage.config";
            Configuration config =
            ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None); 


            ConfigSectionData data = new ConfigSectionData();
            data.Id = 1000;
            data.URL = "ceshi";

            config.Sections.Add("add2", data);
            config.Save(ConfigurationSaveMode.Minimal); 

        }


    }
}
