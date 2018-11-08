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
    /// <summary>
    /// Summary description for IBillerServices
    /// </summary>
    [WebService]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IBillerServices : System.Web.Services.WebService
    {

        [WebMethod]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IBillerServices/BillPull", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("BillPullResponse", Namespace = "http://tempuri.org/")]
        public XElement BillPullRequest(RequestMFEP BillPullRequest)
        {
            XmlDocument xmlSoapRequest = new XmlDocument();
           
            // Get raw request body
            using (Stream receiveStream = HttpContext.Current.Request.InputStream)
            {
                // Move to begining of input stream and read
                receiveStream.Position = 0;
                using (StreamReader readStream =
                                       new StreamReader(receiveStream, Encoding.UTF8))
                {
                    // Load into XML document
                    xmlSoapRequest.Load(readStream);
                }
            }
            //return xmlSoapRequest;
            // Return

            XDocument xDocument = DocumentToXDocument(xmlSoapRequest);

            XElement xEelement = xDocument.Root;



            var guid = Guid.NewGuid().ToString();



            //XElement xElementResult = BillerServiceUtil.BillPullSearch(guid, xEelement);

            //var str = BillerServiceUtil.InnerXMLCore(xElement_);

            //XmlDocument xmlDocument = new XmlDocument();

            //xmlDocument.LoadXml(str);


            BillPullResponse RESPONSE = new BillPullResponse();
            MFEP fEP = new MFEP();

            Headers headers = new Headers();
            headers.Guid = String.Format("f290025e-c298-4faf-8bad-2fbd3cce03e6");
            headers.TmStp = new DateTime().ToString();

            TrsInf trs = new TrsInf();
            trs.SdrCode = String.Format("192");
            trs.ResType = String.Format("BILPULRS");

            headers.TrsInf = trs;

            ResultCoreBus res = new ResultCoreBus();
            res.ErrorCode = String.Format("0");
            res.ErrorDesc = "success";
            res.Severity = "info";

            headers.Result = res;

            fEP.MsgHeader = headers;

            Body msgBody = new Body();
            BillRec rec = new BillRec();
            AccInfo info = new AccInfo();
            info.BillingNo = String.Format("111");
            info.BillingStatus = String.Format("success");
            info.BillNo = String.Format("111");
            info.Currency = String.Format("JD");
            info.DueAmmount = String.Format("50");
            info.DueDate = new DateTime().Date.ToString();
            info.IssueDate = new DateTime().Date.ToString();
            info.ServiceType = String.Format("Payment");
            rec.AccInfo = info;

            PmtCost cost = new PmtCost();
            cost.AllowPart = String.Format("50");
            cost.Lower = String.Format("40");
            cost.Upper = String.Format("100");

            rec.PmtCost = cost;

            List<BillRec> recs = new List<BillRec>();
            recs.Add(rec);

            msgBody.BillRecs = recs;
            msgBody.ResCount = String.Format("1");

            fEP.MsgBody = msgBody;

            Footer footer = new Footer();
            SecurityVal securityVal = new SecurityVal();
            securityVal.Signature = String.Format("Ns1LZ6er0rZfdYmvV7k6Pe4SDa5qY5U2E3Wgv4A93qNtbJGwJeX9RoBLpE2fo9op4MVjb7PcOTu+lMbHQs7Q+Iwu+VS6QUQtPIAfsMjsIge/dJmh2b/VjwBlALPTcLDZ9WdwCiRFQwd2SftRKM1eV7I1VLibeFrNnzflQC60W86/0dZKGCrTanjZAa5j67//FiNAuvkCj0roZoFKPSvceQvri+Kj6+GQ7WpQjhJFEbL7dhPrCFz9DRH9moQqrceGDUB15/Gu2oB0Kn2Wn/JN5I7v/XVPLHs7XBBTGjaR+aSDjhUio7w/MVbcy+UWhcCg40Ppfr+CfQvVLqVjVgQpcA==");
            footer.Security = securityVal;

            fEP.MsgFooter = footer;


            BillPullResponse billPullResponse = new BillPullResponse();
            billPullResponse.MFEP = fEP;

            XElement xElement = billPullResponse.ToXElement<BillPullResponse>();


            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(BillerServiceUtil.InnerXMLCore(xElement));

            //Message message = Message.CreateMessage(MessageVersion.Soap11 , "http://tempuri.org/IBillerServices/BillPull", billPullResponse);



            //var xEElement___ = BillerServiceUtil.BillPullSearch(guid , xEelement);

            //var elements = xEelement.Elements();

            /*foreach (XElement xml in elements)
            {
                if (xml.Name.LocalName == "Body")
                {
                    var billpull__ = xml.Elements();

                    foreach (XElement billpull_____ in billpull__)
                    {
                        billpull_____.Attributes().Remove();

                        XmlAttribute attribute = xmlDocument.CreateAttribute("xmlns");
                        attribute.Value = "http://tempuri.org/IBillerServices/BillPull";
                        billpull_____.Add(attribute);
                    }

                   
                }
            }*/


            XElement xmlTree = XElement.Parse(xmlDocument.InnerXml);
            return xmlTree;
        }

        private static XDocument DocumentToXDocument(XmlDocument doc)
        {
            return XDocument.Parse(doc.OuterXml);
        }

        private static XDocument DocumentToXDocumentNavigator(XmlDocument doc)
        {
            return XDocument.Load(doc.CreateNavigator().ReadSubtree());
        }

        private static XDocument DocumentToXDocumentReader(XmlDocument doc)
        {
            return XDocument.Load(new XmlNodeReader(doc));
        }

        
    }
}
