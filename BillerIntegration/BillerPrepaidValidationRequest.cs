using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BillerIntegration
{
    public class BillerPrepaidValidationRequest
    {
        [XmlElement(elementName: "CardId")]
        public String CardId { get; set; }

        [XmlElement(elementName: "SubscriptionNumber")]
        public String SubscriptionNumber { get; set; }

        [XmlElement(elementName: "BillNo")]
        public String BillNo { get; set; }


        public Boolean valid { get; set; }


    }
}