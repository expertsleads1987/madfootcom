using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace DataAccess.EntityFramework
{

    [XmlRoot(ElementName = "BillPmt")]
    public class BillPaymentUpdated 
    {
        public BillPaymentUpdated() { }

        [XmlElement(ElementName = "JOEBPPSTrx", Type = typeof(long?))]
        public long? JOEBPPSTrx { get; set; }
        [XmlElement(ElementName = "BankCode", Type = typeof(int?))]
        public int? BankCode { get; set; }
        [XmlElement(ElementName = "DueAmount", Type = typeof(decimal?))]
        public decimal? DueAmt { get; set; }
        [XmlElement(ElementName = "PaidAmt", Type = typeof(decimal?))]
        public decimal? PaidAmt { get; set; }
        [XmlElement(ElementName = "PmtStatus", Type = typeof(PaymentStatus?))]
        public PaymentStatus? PmtStatus { get; set; }
        [XmlElement(ElementName = "ProcessDate", Type = typeof(DateTime?))]
        public DateTime? ProcessDate { get; set; }
        [XmlIgnore]
        public bool JOEBPPSTrxSpecified { get; }
        [XmlIgnore]
        public bool BankCodeSpecified { get; }
        [XmlIgnore]
        public bool PaidAmtSpecified { get; }
        [XmlIgnore]
        public bool DueAmtSpecified { get; }
        [XmlIgnore]
        public bool PmtStatusSpecified { get; }
        [XmlIgnore]
        public bool ProcessDateSpecified { get; }

   
    }
}
