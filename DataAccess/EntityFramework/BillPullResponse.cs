using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Xml.XPath;

using DataAccess;

using System.Collections.ObjectModel;
using System.Configuration;

using System.Runtime.Serialization;
using System.Text;
namespace DataAccess.EntityFramework
{
  //  [XmlRoot(Namespace = "http://tempuri.org/IBillerServices/BillPull")]
    public class BillPullResponse
    {
        
        public MFEP MFEP { get; set; }

    }
}