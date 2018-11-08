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
  
    public class PmtCost
    {
        
        public string AllowPart
        {
            get; set;
        }

       
        public string Lower
        {
            get; set;
        }

        
        public string Upper
        {
            get; set;
        }

    }
}
