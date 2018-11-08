using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace BillerIntegration
{
    [ServiceContract]
    public interface IBillerServices
    {
        [OperationContract(Action = "http://tempuri.org/IBillerServices/BillPull", Name = "BillPull")]
        XElement BillPull(string GUID, XElement BillPullRequest);



        [OperationContract(Action = "http://tempuri.org/IBillerServices/ReceivePaymentNotification", Name = "ReceivePaymentNotification")]
        XElement ReceivePaymentNotification(string guid, XElement paymentNotification);
    }
}
