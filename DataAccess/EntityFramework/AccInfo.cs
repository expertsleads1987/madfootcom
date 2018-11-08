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
   
    public class AccInfo
    {
        
        public string BillingNo
        {
            get; set;
        }

        
        public string BillNo
        {
            get; set;
        }

        
        public string BillingStatus
        {
            get; set;
        }

       
        public string DueAmmount
        {
            get; set;
        }

        
        public string Currency
        {
            get; set;
        }

        
        public string IssueDate
        {
            get; set;
        }

        
        public string DueDate
        {
            get; set;
        }

      
        public string ServiceType
        {
            get; set;
        }


    }
}
