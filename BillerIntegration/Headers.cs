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

    public class Headers
    {
      
        public string TmStp
        {
            get;set;
        }

       
        public string Guid
        {
            get; set;
        }

        
     public TrsInf TrsInf
        {
            get; set;

        }

        public ResultCoreBus Result { get; set; }

    }
}
