using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DataAccess.EntityFramework
{
    public class SubPmts
    {
        [XmlElement(ElementName = "SubPmt")]
        public List<SubPaymentUpdated> SubPayment { get; set; }
    }
}