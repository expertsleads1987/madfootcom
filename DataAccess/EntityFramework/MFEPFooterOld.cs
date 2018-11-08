using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace DataAccess.EntityFramework
{
    public class MFEPFooterOld
    {
        [XmlArray("Extra")]
        [XmlArrayItem("CustomField")]
        public List<CustomField> Extra { get; set; }
        [XmlElement("Security")]
        public SecurityOld Security { get; set; }
    }
}