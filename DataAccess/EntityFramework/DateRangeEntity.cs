using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class DateRangeEntity
    {
        [XmlIgnore]
        public bool StartDtSpecified { get; }
        [XmlIgnore]
        public bool EndDtSpecified { get; }
        [XmlElement(ElementName = "StartDt", Type = typeof(DateTime?))]
        public DateTime? StartDt { get; set; }
        [XmlElement(ElementName = "EndDt", Type = typeof(DateTime?))]
        public DateTime? EndDt { get; set; }
    }
}