using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class MFEPMessageOld<T> where T : new()
    {
        public MFEPMessageOld() { }

        [XmlElement("MsgHeader")]
        public MFEPHeaderOld Header { get; set; }
        [XmlElement("MsgBody")]
        public T Body { get; set; }
        [XmlElement("MsgFooter")]
        public MFEPFooterOld Footer { get; set; }
        [XmlIgnore]
        public bool HeaderSpecified { get; }
        [XmlIgnore]
        public bool BodySpecified { get; }
        [XmlIgnore]
        public bool FooterSpecified { get; }
    }
}