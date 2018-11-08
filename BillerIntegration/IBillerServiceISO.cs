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

namespace BillerIntegration
{
    [ServiceContract]
    public interface IBillerServiceISO
    {
        [OperationContract]
        XElement BillPull(string GUID, RequestMFEP BillPullRequest);

        [OperationContract]
        XElement ReceivePaymentNotification(string guid, XElement paymentNotification);
    }
}
