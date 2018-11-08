using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Xml.XPath;
using System.ServiceModel;

using System.ServiceModel.Channels;
using DataAccess;
using MFEP.Common.Security;
using MFEP.Common.Utilities;
using MFEP.ProjectHelpers.Helpers.Entites.MFEPMessage;
using MFEP.ProjectHelpers.Helpers.Entites.Services.BillInquiry;
using MFEP.ProjectHelpers.Helpers.Entites.Services.BillUpload;
using MFEP.ProjectHelpers.Helpers.Entites.Services.PaymentNotification;
using MFEP.ProjectHelpers.Helpers.Enums;
using System.Collections.ObjectModel;
using System.Configuration;

using System.Runtime.Serialization;
using System.Text;
namespace BillerIntegration
{
  //  [XmlRoot(Namespace = "http://tempuri.org/IBillerServices/BillPull")]
    public class BillPullResponse
    {
        
        public MFEP MFEP { get; set; }

    }
}