using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class SubPaymentUpdated
    {
        public SubPaymentUpdated() { }

        [XmlElement(ElementName = "Amount", Type = typeof(decimal?))]
        public decimal? Amount { get; set; }
        
        [XmlElement(ElementName = "SetBnkCode", Type = typeof(int?))]
        public int? BankCode { get; set; }
        
        [XmlElement(ElementName = "AcctNo", Type = typeof(string))]
        public string BankAccountNo { get; set; }
        [XmlIgnore]
        public bool AmountSpecified { get; }
        [XmlIgnore]
        public bool BankCodeSpecified { get; }
        [XmlIgnore]
        public bool BankAccountNoSpecified { get; }

    }
}
