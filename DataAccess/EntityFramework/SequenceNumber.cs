using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class SequenceNumber
    {
        public SequenceNumber() { }

        [XmlElement("Sess")]
        public int Session { get; set; }
        [XmlElement("Seq")]
        public int Sequence { get; set; }
    }
}