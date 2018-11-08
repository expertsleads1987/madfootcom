using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DataAccess.EntityFramework
{
    public class BillInquiryRequestEntityUpdated
    {
        [XmlElement(ElementName = "ServiceType", Order = 2)]
        public string ServiceType;
        [XmlElement(ElementName = "DateRange", Type = typeof(DateRangeEntity), Order = 3)]
        public DateRangeEntity DateRange;
        [XmlElement(ElementName = "IncPayments", Type = typeof(bool?), Order = 4)]
        public bool? IncPayments;
        [XmlElement(ElementName = "IncPaidBills", Type = typeof(bool?), Order = 5)]
        public bool? IncPaidBills;

        public BillInquiryRequestEntityUpdated() { }

        [XmlElement(ElementName = "AcctInfo", Order = 1)]
        public AccountInfoOld AccountInfo { get; set; }
        [XmlIgnore]
        public bool AccountInfoSpecified { get; }
        [XmlIgnore]
        public bool ServiceTypeSpecified { get; }
        [XmlIgnore]
        public bool DateRangeSpecified { get; }
        [XmlIgnore]
        public bool IncPaymentsSpecified { get; }
        [XmlIgnore]
        public bool IncPaidBillsSpecified { get; }
    }
}