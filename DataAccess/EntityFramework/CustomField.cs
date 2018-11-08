using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class CustomField
    {
        [XmlElement("FieldID")]
        public int FieldID { get; set; }
        [XmlElement("FieldName")]
        public string FieldName { get; set; }
        [XmlElement("value")]
        public string Value { get; set; }
        [XmlElement("ParentTagXPath")]
        public string ParentTagXPath { get; set; }
    }
}