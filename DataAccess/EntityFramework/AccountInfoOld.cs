using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class AccountInfoOld
    {
        [XmlElement("BillingNo")]
        public string BillingNumber { get; set; }
        [XmlElement("BillNo")]
        public string BillNumber { get; set; }
        [XmlElement("BillerCode", Type = typeof(int?))]
        public int? BillerCode { get; set; }
        [XmlIgnore]
        public bool BillerCodeSpecified { get; }
        [XmlIgnore]
        public bool BillNumberSpecified { get; }
        [XmlIgnore]
        public bool BillingNumberSpecified { get; }
    }
}