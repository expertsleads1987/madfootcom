using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.ServiceModel.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
namespace BillerIntegration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBillPullService" in both code and config file together.
    [ServiceContract]
    public interface IBillerService
    {
        [OperationContract(Action = "http://192.168.10.91:31033/BillPull.svc/BillPull_")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IBillerServices/BillPull", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("BillPullResponse", Namespace = "http://tempuri.org/")]
        XElement BillPull_(string guid, XElement billPullRequest);

        [OperationContract(Action = "http://192.168.10.91:31033/BillPull.svc/PaymentNotification")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        XElement ReceivePaymentNotification(string guid, XElement prePaidRequest);
    }
}
