using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccess.EntityFramework
{
    [XmlRoot(ElementName = "BillsRec")]
    public class BillsRecordUpdated 
    {
        public BillsRecordUpdated() { }

        [XmlElement(ElementName = "InqRefNo")]
        public long? InqRefNo { get; set; }
        [XmlElement(ElementName = "RecCount")]

        public int? RecCount { get; set; }
        [XmlArray(ElementName = "BillsRec")]
        [XmlArrayItem(ElementName = "BillRec")]
        public List<BillRecordUpdated> BillRecords { get; set; }

        [XmlIgnore]
        public bool RecCountSpecified { get; }
        [XmlIgnore]
        public bool BillRecordsSpecified { get; }
        [XmlIgnore]
        public bool InqRefNoSpecified { get; }

       
    }
}