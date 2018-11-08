using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class TransmitInfoOld
    {
        [XmlElement("SdrCode")]
        public int SenderCode { get; set; }
        [XmlElement("RcvCode")]
        public int ReceiverCode { get; set; }
        [XmlElement("ResTyp")]
        public ProcessesCodesUpdated? ResponseType { get; set; }
        [XmlIgnore]
        public bool ResponseTypeSpecified { get; }
        [XmlElement("ReqTyp")]
        public ProcessesCodesUpdated? RequestType { get; set; }
        [XmlIgnore]
        public bool RequestTypeSpecified { get; }
        [XmlIgnore]
        public bool ReceiverCodeSpecified { get; }
        [XmlIgnore]
        public bool SenderCodeSpecified { get; }
    }
}