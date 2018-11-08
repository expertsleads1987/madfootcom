using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace BillerIntegration
{
   
  
    public class BillRec
    {
        
        public AccInfo AccInfo
        {
            get; set;
        }

        
        public PmtCost PmtCost
        {
            get; set;

        }

    }
}
