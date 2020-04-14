using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.DrawingElement
{
    struct StrValueProperty
    {
        public string PropertyName;
        public string PropertyValue;
        public StrValueProperty(string propertyName, string propertyValue)
        {
            PropertyName = "";
            PropertyValue = "";
            if (!string.IsNullOrEmpty(propertyName))
            PropertyName = propertyName.Trim();
            if(!string.IsNullOrEmpty(propertyValue))
            PropertyValue = propertyValue.Trim();
        }
    }
}
