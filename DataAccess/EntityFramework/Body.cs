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
   
    public  class Body
    {
        
        public string ResCount
        {
            get; set;
        }

       
        public List<BillRec> BillRecs 
        {
            get; set;
        }
      
    }
}
