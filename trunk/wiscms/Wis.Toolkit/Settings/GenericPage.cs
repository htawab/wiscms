using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Wis.Toolkit.Settings
{
    public class GenericPage
    {
        public GenericPage()
        { }

        public GenericPage(string pattern)
        {
            pagePattern = pattern;
        }

        private string pagePattern;
        [XmlAttribute(DataType = "string", AttributeName = "Pattern")]
        public string Pattern
        {
            get { return pagePattern; }
            set { pagePattern = value; }
        }
    }
}
