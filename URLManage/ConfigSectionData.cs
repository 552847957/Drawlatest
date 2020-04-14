using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LJJSCAD.URLManage
{
    class ConfigSectionData : ConfigurationSection
    {
        [ConfigurationProperty("id")]
        public int Id
        {
            get { return (int)this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("jh")]
        public string JH
        {
            get { return (string)this["jh"]; }
            set { this["jh"] = value; }
        }
        [ConfigurationProperty("entityId")]
        public string EntityID
        {
            get { return (string)this["entityId"]; }
            set { this["entityId"] = value; }
        }
        [ConfigurationProperty("glStyle")]
        public string GLStyle
        {
            get { return (string)this["glStyle"]; }
            set { this["glStyle"] = value; }
        }
        [ConfigurationProperty("url")]
        public string URL
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

    }

}
