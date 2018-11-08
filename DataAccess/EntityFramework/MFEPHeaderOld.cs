using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class MFEPHeaderOld
    {
        [XmlElement(ElementName = "TmStp")]
        public string TimeStamp { get; set; }
        [XmlElement(ElementName = "GUID")]
        public string Guid { get; set; }
        [XmlElement(ElementName = "Token")]
        public string Token { get; set; }
        [XmlElement(ElementName = "TrsInf")]
        public TransmitInfoOld TransmitInfo { get; set; }
        [XmlElement(ElementName = "Sequence")]
        public SequenceNumber Sequence { get; set; }
        [XmlElement(ElementName = "Result")]
        public ResultUpdated Result { get; set; }
    }
}