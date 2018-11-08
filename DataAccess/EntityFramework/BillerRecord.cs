using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DataAccess.EntityFramework
{
  public  class BillerRecord 
    {
        public BillerRecord() { }

        [XmlElement(ElementName = "DueAmountWithFees")]
        public decimal? DueAmountWithFees { get; set; }
        [XmlElement(ElementName = "Extra")]
        public string Extra { get; set; }
        [XmlElement(ElementName = "IsFeesAdded")]
        public bool? IsFeesAdded { get; set; }
        [XmlElement(ElementName = "CorridorID")]
        public int? CorridorID { get; set; }
        [XmlIgnore]
        public bool BillIDSpecified { get; }
        [XmlIgnore]
        public bool BillStatusSpecified { get; }
        [XmlIgnore]
        public bool DueAmountSpecified { get; }
        [XmlIgnore]
        public bool ProxyDueDateSpecified { get; }
        [XmlIgnore]
        public bool ProxyIssueDateSpecified { get; }
        [XmlIgnore]
        public bool ProxyOpenDateSpecified { get; }
        [XmlIgnore]
        public bool ProxyCloseDateSpecified { get; }
        [XmlIgnore]
        public bool ProxyExpiryDateSpecified { get; }
        [XmlIgnore]
        public bool ServiceTypeSpecified { get; }
        [XmlIgnore]
        public bool BillTypeSpecified { get; }
        [XmlIgnore]
        public bool BillPaymentsSpecified { get; }
        [XmlIgnore]
        public bool SubPaymentsSpecified { get; }
        [XmlIgnore]
        public bool FeesAmtSpecified { get; }
        [XmlIgnore]
        public bool DueAmountWithFeesSpecified { get; }
        [XmlIgnore]
        public bool CbjFeesSpecified { get; }
        [XmlIgnore]
        public bool MFEPFeesSpecified { get; }
        [XmlIgnore]
        public bool ExtraSpecified { get; }
        [XmlElement(ElementName = "MFEPFees")]
        public decimal? MFEPFees { get; set; }
        [XmlElement(ElementName = "CbjFees")]
        public decimal? CbjFees { get; set; }
        [XmlIgnore]
        public bool CorridorIDSpecified { get; }
        [XmlElement(ElementName = "Result", Type = typeof(ResultUpdated))]
        public ResultUpdated Result { get; set; }
        public long? BillID { get; set; }
        [XmlElement(ElementName = "AcctInfo", Type = typeof(AccountInfoOld))]
        public AccountInfoOld AccountInfo { get; set; }
        [XmlElement(ElementName = "BillStatus", Type = typeof(BillStatusCodes?))]
        public BillStatusCodes? BillStatus { get; set; }
        [XmlElement(ElementName = "DueAmount", Type = typeof(decimal?))]
        public decimal? DueAmount { get; set; }
        [XmlElement(ElementName = "FeesAmt", Type = typeof(decimal?))]
        public decimal? FeesAmt { get; set; }
        [XmlIgnore]
        public DateTime? IssueDate { get; set; }
        [XmlElement(ElementName = "IssueDate")]
        public string ProxyIssueDate { get; set; }
        [XmlIgnore]
        public DateTime? OpenDate { get; set; }
        [XmlElement(ElementName = "OpenDate")]
        public string ProxyOpenDate { get; set; }
        [XmlIgnore]
        public bool IsFeesAddedSpecified { get; }
        [XmlIgnore]
        public DateTime? DueDate { get; set; }
        [XmlIgnore]
        public DateTime? ExpiryDate { get; set; }
        [XmlElement(ElementName = "ExpiryDate")]
        public string ProxyExpiryDate { get; set; }
        [XmlIgnore]
        public DateTime? CloseDate { get; set; }
        [XmlElement(ElementName = "CloseDate")]
        public string ProxyCloseDate { get; set; }
        [XmlElement(ElementName = "ServiceType", Type = typeof(string))]
        public string ServiceType { get; set; }
        [XmlElement(ElementName = "BillType", Type = typeof(BillType?))]
        public BillType? BillType { get; set; }
        [XmlElement(ElementName = "PmtConst", Type = typeof(PmtConstUpdated))]
        public PmtConstUpdated PmtConst { get; set; }
        [XmlElement(ElementName = "SubPmts")]
        public SubPmts SubPayments { get; set; }
        [XmlArray(ElementName = "BillPmts")]
        [XmlArrayItem(ElementName = "BillPmt")]
        public Collection<BillPaymentUpdated> BillPayments { get; set; }
        [XmlElement(ElementName = "DueDate")]
        public string ProxyDueDate { get; set; }





    }
}
