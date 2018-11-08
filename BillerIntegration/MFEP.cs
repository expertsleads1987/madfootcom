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
    

    public class MFEP
    {
  
       public Headers MsgHeader { get; set; }

     
        public Body MsgBody
        {
            get; set;
        }

     
        public Footer MsgFooter
        {
            get; set;
        }

    }
}
