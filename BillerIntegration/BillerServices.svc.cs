using DataAccess;
using MFEP.Common.Security;
using MFEP.Common.Utilities;
using MFEP.ProjectHelpers.Helpers.Entites.MFEPMessage;
using MFEP.ProjectHelpers.Helpers.Entites.Services.BillInquiry;
using MFEP.ProjectHelpers.Helpers.Entites.Services.BillUpload;
using MFEP.ProjectHelpers.Helpers.Entites.Services.PaymentNotification;
using MFEP.ProjectHelpers.Helpers.Enums;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Serialization;
namespace BillerIntegration
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BillerServices : IBillerServices, IBillerPrepaid
    {
        #region Properties

        private static bool IsTestReponse { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsTestReponse"]); } }
        private static bool EnableSecuity { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSecuity"]); } }
        private static string SecurityMode { get { return Convert.ToString(ConfigurationManager.AppSettings["SecurityMode"]); } }
        private static string XPath { get { return Convert.ToString(ConfigurationManager.AppSettings["XPath"]); } }
        private static string MFEPCertificateSerialNumber { get { return Convert.ToString(ConfigurationManager.AppSettings["MFEPCertificateSerialNumber"]); } }
        private static string BillerCertificateSerialNumber { get { return Convert.ToString(ConfigurationManager.AppSettings["BillerCertificateSerialNumber"]); } }
        private static bool AllowPart { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["AllowPart"]); } }
        private static decimal Lower { get { return Convert.ToDecimal(ConfigurationManager.AppSettings["Lower"]); } }
        private static decimal Upper { get { return Convert.ToDecimal(ConfigurationManager.AppSettings["Upper"]); } }
        private static string ExceptionPath { get { return Convert.ToString(ConfigurationManager.AppSettings["ExceptionPath"]); } }
        private static string SuccessPMTNotiRequestPath { get { return Convert.ToString(ConfigurationManager.AppSettings["SuccessPMTNotiRequestPath"]); } }
        private static string SuccessPMTNotiResponsePath { get { return Convert.ToString(ConfigurationManager.AppSettings["SuccessPMTNotiResponsePath"]); } }
        private static string FailedPMTNotiRequestPath { get { return Convert.ToString(ConfigurationManager.AppSettings["FailedPMTNotiRequestPath"]); } }
        private static string FailedPMTNotiResponsePath { get { return Convert.ToString(ConfigurationManager.AppSettings["FailedPMTNotiResponsePath"]); } }
        private static int BillerCode { get { return Convert.ToInt32(ConfigurationManager.AppSettings["BillerCode"]); } }
        private static int MFEPCode { get { return Convert.ToInt32(ConfigurationManager.AppSettings["MFEPCode"]); } }
        private static string ServiceType { get { return Convert.ToString(ConfigurationManager.AppSettings["ServiceType"]); } }
        private static string BillPullRequestPath { get { return Convert.ToString(ConfigurationManager.AppSettings["BillPullRequestPath"]); } }
        private static string BillPullResponsePath { get { return Convert.ToString(ConfigurationManager.AppSettings["BillPullResponsePath"]); } }
        private static int TimeDifference { get { return Convert.ToInt32(ConfigurationManager.AppSettings["TimeDifference"]); } }

        #endregion

        #region Public Methods

        [OperationBehavior]
        public XElement BillPull(string guid, XElement billPullRequest)
        {
            MFEPMessage<BillsRecord> deserializedResponse = new MFEPMessage<BillsRecord>();
            XElement serializedResponse = null;
            string fileGuid = Guid.NewGuid().ToString();
            try
            {
                // Write the request to physical path
                WriteToPhysicalPath(billPullRequest, BillPullRequestPath, fileGuid);
                MFEPMessage<BillInquiryRequestEntity> deserializedRequest = billPullRequest.FromXElement<MFEPMessage<BillInquiryRequestEntity>>();

                deserializedResponse.Header.TimeStamp = DateTime.Now.ToString("s");
                deserializedResponse.Header.Guid = guid;
                deserializedResponse.Header.TransmitInfo = new TransmitInfo()
                {
                    SenderCode = deserializedRequest.Header.TransmitInfo.ReceiverCode,
                    ResponseType = ProcessesCodes.BILPULRS
                };

                deserializedResponse.Header.Result = new Result();
                bool isTestReponse = IsTestReponse;
                if (!isTestReponse)
                {
                    // Verifying
                    if (EnableSecuity)
                    {
                        if (deserializedRequest == null || deserializedRequest.Footer.Security == null || deserializedRequest.Footer.Security.Signature == null || string.IsNullOrWhiteSpace(deserializedRequest.Footer.Security.Signature.Trim()))
                        {
                            Result result_1 = new Result();
                            result_1.ErrorCode = (int)ServiceErrors.InvalidSignature;
                            result_1.ErrorDesc = ServiceErrors.InvalidSignature.ToString();
                            result_1.Severity = Severity.Error;
                            deserializedResponse.Header.Result = result_1;
                            serializedResponse = deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();
                            // Write the request to physical path
                            WriteToPhysicalPath(serializedResponse, BillPullResponsePath, fileGuid);

                            return serializedResponse;
                        }
                        else
                        {
                            VerifyMode verifyMode = VerifyMode.WithFormat;
                            if (SecurityMode.Equals("WithOutFormat", StringComparison.CurrentCultureIgnoreCase))
                            {
                                verifyMode = VerifyMode.WithOutFormat;
                            }
                            Collection<string> xPath = new Collection<string> { XPath };
                            bool verifiedSuccessfully = CertificateManager.VerifyMessageSignature(deserializedRequest.ToXElement<MFEPMessage<BillInquiryRequestEntity>>(), xPath, deserializedRequest.Footer.Security.Signature, BillerCertificateSerialNumber, verifyMode);
                            if (!verifiedSuccessfully)
                            {
                                Result result_2 = new Result();
                                result_2.ErrorCode = (int)ServiceErrors.InvalidSignature;
                                result_2.ErrorDesc = ServiceErrors.InvalidSignature.ToString();
                                result_2.Severity = Severity.Error;
                                deserializedResponse.Header.Result = result_2;
                                serializedResponse = deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();

                                WriteToPhysicalPath(serializedResponse, BillPullResponsePath, fileGuid);

                                return serializedResponse;
                            }
                        }


                        if (deserializedRequest.Header.TransmitInfo.SenderCode != MFEPCode)
                        {
                            Result result_3 = new Result();
                            result_3.ErrorCode = (int)ServiceErrors.InvalidSenderCode;
                            result_3.ErrorDesc = ServiceErrors.InvalidSenderCode.ToString();
                            result_3.Severity = Severity.Error;
                            deserializedResponse.Header.Result = result_3;
                            serializedResponse = deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();
                            // Write the request to physical path
                            WriteToPhysicalPath(serializedResponse, BillPullResponsePath, fileGuid);
                            return serializedResponse;
                        }

                        // Validate Biller code
                        if (deserializedRequest.Header.TransmitInfo.ReceiverCode != BillerCode)
                        {
                            Result result_4 = new Result();
                            result_4.ErrorCode = (int)ServiceErrors.UnsuccessfulBillPullRequest;
                            result_4.ErrorDesc = ServiceErrors.UnsuccessfulBillPullRequest.ToString();
                            result_4.Severity = Severity.Error;
                            deserializedResponse.Header.Result = result_4;
                            serializedResponse = deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();
                            // Write the request to physical path
                            WriteToPhysicalPath(serializedResponse, BillPullResponsePath, fileGuid);

                            return serializedResponse;
                        }

                        // Validate Service Type
                        if (!ServiceType.Equals(deserializedRequest.Body.ServiceType, StringComparison.CurrentCultureIgnoreCase))
                        {
                            Result result_5 = new Result();
                            result_5.ErrorCode = (int)ServiceErrors.UnrecognizedServiceType;
                            result_5.ErrorDesc = ServiceErrors.UnrecognizedServiceType.ToString();
                            result_5.Severity = Severity.Error;
                            deserializedResponse.Header.Result = result_5;
                            serializedResponse = deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();
                            // Write the request to physical path
                            WriteToPhysicalPath(serializedResponse, BillPullResponsePath, fileGuid);


                            return serializedResponse;
                        }

                        // Get Bill and generate the response
                       DAL dal = new DAL();
                        BillRecord billRecord = dal.GetBillInfo(deserializedRequest.Body.AccountInfo.BillNumber, deserializedRequest.Body.AccountInfo.BillingNumber, deserializedRequest.Body.ServiceType);
                        if (billRecord != null)
                        {
                            billRecord.IssueDate = billRecord.IssueDate.Value.AddMinutes(TimeDifference);
                            billRecord.DueDate = billRecord.DueDate.Value.AddMinutes(TimeDifference);
                            deserializedResponse.Body = new BillsRecord() { RecCount = 1 };
                            deserializedResponse.Body.BillRecords.Add(billRecord);
                        }
                        else
                        {
                            deserializedResponse.Body = new BillsRecord() { RecCount = 0, BillRecords = null };
                        }
                        // Sign the response
                        if (EnableSecuity)
                        {
                            SignMode signMode = SignMode.WithFormat;
                            if (SecurityMode.Equals("WithOutFormat", StringComparison.CurrentCultureIgnoreCase))
                            {
                                signMode = SignMode.WithOutFormat;
                            }
                            Collection<string> xPath = new Collection<string> { XPath };
                            deserializedResponse.Footer = new MFEPFooter();
                            deserializedResponse.Footer.Security.Signature = CertificateManager.SignMessage(deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>(), xPath, BillerCertificateSerialNumber, signMode);
                        }
                        else
                        {
                            SignMode signMode = SignMode.WithFormat;
                            if (SecurityMode.Equals("WithOutFormat", StringComparison.CurrentCultureIgnoreCase))
                            {
                                signMode = SignMode.WithOutFormat;
                            }
                            Collection<string> xPath = new Collection<string> { XPath };
                            deserializedResponse.Footer = new MFEPFooter();
                            deserializedResponse.Footer.Security.Signature = CertificateManager.SignMessage(deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>(), xPath, BillerCertificateSerialNumber, signMode);
                        }
                        serializedResponse = deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();
                        // Write the request to physical path
                        WriteToPhysicalPath(serializedResponse, BillPullResponsePath, fileGuid);


                        return serializedResponse;

                    }



                    // Sign the response
                    return GenerateAutoResponse(guid, billPullRequest);
                    /* serializedResponse = deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();
                     // Write the request to physical path
                     WriteToPhysicalPath(serializedResponse, BillPullResponsePath, fileGuid);

                    */
                    /*return serializedResponse;*/
                }
                else
                {
                    return GenerateAutoResponse(guid, billPullRequest);
                }
            }
            catch (Exception exception)
            {
                string ex = exception.Message + "\n" + exception.StackTrace;
                WriteException(ex, ExceptionPath);
                Result result = new Result();
                result.ErrorCode = (int)ServiceErrors.InternalError;
                result.ErrorDesc = ServiceErrors.InternalError.ToString();
                result.Severity = Severity.Error;
                deserializedResponse.Header.Result = result;
                return deserializedResponse.ToXElement<MFEPMessage<BillsRecord>>();
            }
        }


        [OperationBehavior]
        public XElement ReceivePaymentNotification(string guid, XElement paymentNotification)
        {
            MFEPMessage<PaymentNotificationTransaction> deserializedResponse = new MFEPMessage<PaymentNotificationTransaction>();
            XElement serializedResponse = null;
            string fileName = Guid.NewGuid().ToString();
            try
            {
                MFEPMessage<PaymentNotificationTransaction> deserializedRequest = paymentNotification.FromXElement<MFEPMessage<PaymentNotificationTransaction>>();
                deserializedResponse.Header.TimeStamp = DateTime.Now.ToString("s");
                deserializedResponse.Header.Guid = guid;
                deserializedResponse.Header.TransmitInfo = new TransmitInfo()
                {
                    SenderCode = deserializedRequest.Header.TransmitInfo.ReceiverCode,
                    ResponseType = ProcessesCodes.BLRPMTNTFRS
                };

                deserializedResponse.Header.Result = new Result();
                fileName = deserializedRequest.Body.Transactions[0].ServiceType.SrvsTypValue.ToString() + fileName;
                if (!IsTestReponse)
                {
                    // Verifying
                    if (EnableSecuity)
                    {
                        if (deserializedRequest == null || deserializedRequest.Footer.Security == null || deserializedRequest.Footer.Security.Signature == null || string.IsNullOrWhiteSpace(deserializedRequest.Footer.Security.Signature.Trim()))
                        {
                            Result result = new Result();
                            result.ErrorCode = (int)ServiceErrors.InvalidSignature;
                            result.ErrorDesc = ServiceErrors.InvalidSignature.ToString();
                            result.Severity = Severity.Error;
                            deserializedResponse.Header.Result = result;
                            WriteToPhysicalPath(paymentNotification, FailedPMTNotiRequestPath, fileName); // Write to failed notifications 
                            serializedResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();
                            WriteToPhysicalPath(serializedResponse, FailedPMTNotiResponsePath, fileName);
                            return serializedResponse;
                        }
                        else
                        {
                            VerifyMode verifyMode = VerifyMode.WithFormat;
                            if (SecurityMode.Equals("WithOutFormat", StringComparison.CurrentCultureIgnoreCase))
                            {
                                verifyMode = VerifyMode.WithOutFormat;
                            }
                            Collection<string> xPath = new Collection<string> { XPath };
                            bool verifiedSuccessfully = CertificateManager.VerifyMessageSignature(paymentNotification, xPath, deserializedRequest.Footer.Security.Signature, BillerCertificateSerialNumber, verifyMode);
                            if (!verifiedSuccessfully)
                            {
                                Result result = new Result();
                                result.ErrorCode = (int)ServiceErrors.InvalidSignature;
                                result.ErrorDesc = ServiceErrors.InvalidSignature.ToString();
                                result.Severity = Severity.Error;
                                deserializedResponse.Header.Result = result;
                                WriteToPhysicalPath(paymentNotification, FailedPMTNotiRequestPath, fileName); // Write to failed notifications 
                                serializedResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();
                                WriteToPhysicalPath(serializedResponse, FailedPMTNotiResponsePath, fileName);
                                return serializedResponse;
                            }
                        }
                    }

                    // Validation
                    // Validate  MFEP code
                    if (deserializedRequest.Header.TransmitInfo.SenderCode != MFEPCode)
                    {
                        Result result = new Result();
                        result.ErrorCode = (int)ServiceErrors.InvalidSenderCode;
                        result.ErrorDesc = ServiceErrors.InvalidSenderCode.ToString();
                        result.Severity = Severity.Error;
                        deserializedResponse.Header.Result = result;
                        WriteToPhysicalPath(paymentNotification, FailedPMTNotiRequestPath, fileName); // Write to failed notifications 
                        serializedResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();
                        WriteToPhysicalPath(serializedResponse, FailedPMTNotiResponsePath, fileName);
                        return serializedResponse;
                    }

                    // Validate Biller code
                    if (deserializedRequest.Header.TransmitInfo.ReceiverCode != BillerCode)
                    {
                        Result result = new Result();
                        result.ErrorCode = (int)ServiceErrors.UnsuccessfulPaymentNotification;
                        result.ErrorDesc = ServiceErrors.UnsuccessfulPaymentNotification.ToString();
                        result.Severity = Severity.Error;
                        deserializedResponse.Header.Result = result;
                        WriteToPhysicalPath(paymentNotification, FailedPMTNotiRequestPath, fileName); // Write to failed notifications 
                        serializedResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();
                        WriteToPhysicalPath(serializedResponse, FailedPMTNotiResponsePath, fileName);
                        return serializedResponse;
                    }
                    // if the serviceType is IndividualSecurity then insert it to database otherwise just write it ti physical directory 
                    if (ServiceType.Equals(deserializedRequest.Body.Transactions[0].ServiceType.SrvsTypValue.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Insert this payment
                        DAL dal = new DAL();
                        dal.InsertPaymentNotification(deserializedRequest.Body.Transactions[0].AccountInfo.BillingNumber);
                    }
                    PaymentNotificationTransaction transaction = new PaymentNotificationTransaction
                    {
                        Transactions = new Collection<PaymentNotificationEntity>()
                    };
                    foreach (PaymentNotificationEntity pne in deserializedRequest.Body.Transactions)
                    {
                        PaymentNotificationEntity notificationEntity = new PaymentNotificationEntity
                        {
                            JOEBPPSTrx = pne.JOEBPPSTrx,
                            ProcessDate = pne.ProcessDate,
                            STMTDate = pne.STMTDate,
                            Result = new Result()
                        };
                        transaction.Transactions.Add(notificationEntity);
                    }
                    deserializedResponse.Body = transaction;
                    // Sign the response
                    if (EnableSecuity)
                    {
                        SignMode signMode = SignMode.WithFormat;
                        if (SecurityMode.Equals("WithOutFormat", StringComparison.CurrentCultureIgnoreCase))
                        {
                            signMode = SignMode.WithOutFormat;
                        }
                        Collection<string> xPath = new Collection<string> { XPath };
                        deserializedResponse.Footer = new MFEPFooter();

                        deserializedResponse.Footer.Security.Signature = CertificateManager.SignMessage(deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>(), xPath, BillerCertificateSerialNumber, signMode);
                    }
                    else
                    {
                        deserializedResponse.Footer = new MFEPFooter();
                    }
                    WriteToPhysicalPath(paymentNotification, SuccessPMTNotiRequestPath, fileName); // Write to success notifications 
                    serializedResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();
                    WriteToPhysicalPath(serializedResponse, SuccessPMTNotiResponsePath, fileName);
                    return serializedResponse;
                }
                else
                {
                    return GenerateAutoResponse(guid, deserializedRequest, guid);
                }
            }
            catch (Exception exception)
            {

                string ex = exception.Message + "\n" + exception.StackTrace;
                WriteException(ex, ExceptionPath);

                Result result = new Result();
                result.ErrorCode = (int)ServiceErrors.InternalError;
                result.ErrorDesc = ServiceErrors.InternalError.ToString();
                result.Severity = Severity.Error;
                deserializedResponse.Header.Result = result;
                WriteToPhysicalPath(paymentNotification, FailedPMTNotiRequestPath, fileName); // Write to success notifications 
                serializedResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();
                WriteToPhysicalPath(serializedResponse, FailedPMTNotiResponsePath, fileName);
                return serializedResponse;
            }
        }

        public XElement PrepaidValidation(string guid, XElement billerPrepaidValidationRequest, string username = null, string password = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private static XElement GenerateAutoResponse(string guid, XElement billPullRequest)
        {
            MFEPMessage<BillInquiryRequestEntity> deserializedRequest = billPullRequest.FromXElement<MFEPMessage<BillInquiryRequestEntity>>();

            MFEPMessage<BillsRecord> response = new MFEPMessage<BillsRecord>();

            response.Header.TimeStamp = DateTime.Now.ToString("s");
            response.Header.Guid = guid;
            response.Header.TransmitInfo = new TransmitInfo()
            {
                SenderCode = deserializedRequest.Header.TransmitInfo.ReceiverCode,
                ResponseType = ProcessesCodes.BILPULRS
            };

            response.Header.Result = new Result();

            response.Body = new BillsRecord() { RecCount = 1 };

            Collection<BillRecord> billRecords = new Collection<BillRecord>();

            BillRecord o = new BillRecord();
            o.PmtConst = new PmtConst();
            o.PmtConst.AllowPart = AllowPart;
            if (o.PmtConst.AllowPart != false)
            {
                o.PmtConst.Lower = 50;
                o.PmtConst.Upper = 100;
            }
            BillRecord billRecord = new BillRecord()
            {
                AccountInfo = new AccountInfo
                {
                    BillingNumber = deserializedRequest.Body.AccountInfo.BillingNumber,
                    BillNumber = deserializedRequest.Body.AccountInfo.BillNumber
                    // BillerCode = 191
                },
                BillStatus = BillStatusCodes.BillNew,
                DueAmount = new Random().Next(50, 100),

                IssueDate = DateTime.Now.AddMinutes(TimeDifference),
                DueDate = DateTime.Now.AddMinutes(TimeDifference),
                ServiceType = deserializedRequest.Body.ServiceType,
                // PmtConst = o.PmtConst
            };


            billRecords.Add(billRecord);
            response.Body.BillRecords = billRecords;


            XElement billPullResponse = response.ToXElement<MFEPMessage<BillsRecord>>();

            string signatureValue = string.Empty;
            if (EnableSecuity)
            {
                signatureValue = CertificateManager.SignMessage(billPullResponse, new Collection<string>() { ".//MsgBody" }, BillerCertificateSerialNumber, (SignMode)Enum.Parse(typeof(SignMode), SecurityMode));
            }

            response.Footer = new MFEPFooter()
            {
                Security =
                    new Security()
                    {
                        Signature = signatureValue
                    }
            };

            //return response;

            return response.ToXElement<MFEPMessage<BillsRecord>>();
        }

        static string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }
        private static XElement GenerateAutoResponse(string guid, MFEPMessage<PaymentNotificationTransaction> deserializedRequest, string fileGuid)
        {
            XElement serializedResponse = null;
            MFEPMessage<PaymentNotificationTransaction> deserializedResponse = new MFEPMessage<PaymentNotificationTransaction>();

            deserializedResponse.Header.TimeStamp = DateTime.Now.ToString("s");
            deserializedResponse.Header.Guid = guid;
            deserializedResponse.Header.TransmitInfo = new TransmitInfo()
            {
                SenderCode = deserializedRequest.Header.TransmitInfo.ReceiverCode,
                ResponseType = ProcessesCodes.BLRPMTNTFRS
            };

            deserializedResponse.Header.Result = new Result();

            PaymentNotificationTransaction transaction = new PaymentNotificationTransaction
            {
                Transactions = new Collection<PaymentNotificationEntity>()
            };
            foreach (PaymentNotificationEntity paymentNotificationEntity in deserializedRequest.Body.Transactions)
            {
                PaymentNotificationEntity notificationEntity = new PaymentNotificationEntity
                {
                    JOEBPPSTrx = paymentNotificationEntity.JOEBPPSTrx,
                    ProcessDate = paymentNotificationEntity.ProcessDate,
                    STMTDate = paymentNotificationEntity.STMTDate,
                    Result = new Result()
                };
                transaction.Transactions.Add(notificationEntity);
            }

            deserializedResponse.Body = transaction;

            XElement pmtResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();

            string signatureValue = string.Empty;
            if (EnableSecuity)
            {
                signatureValue = CertificateManager.SignMessage(pmtResponse, new Collection<string>() { ".//MsgBody" }, BillerCertificateSerialNumber, (SignMode)Enum.Parse(typeof(SignMode), SecurityMode));
            }

            deserializedResponse.Footer = new MFEPFooter()
            {
                Security =
                    new Security()
                    {
                        Signature = signatureValue
                    }
            };

            serializedResponse = deserializedResponse.ToXElement<MFEPMessage<PaymentNotificationTransaction>>();
            WriteToPhysicalPath(serializedResponse, SuccessPMTNotiResponsePath, fileGuid);
            return serializedResponse;
        }

        private static void WriteToPhysicalPath(XElement file, string path, string fileGuid)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = string.Format("{0}.xml", fileGuid);
            string directoryFullPath = Path.Combine(path, fileName);
            file.Save(directoryFullPath);
        }

        private static void WriteException(string exception, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = string.Format("Exception_{0}.txt", Guid.NewGuid());
            string directoryFullPath = Path.Combine(path, fileName);
            File.WriteAllText(directoryFullPath, exception);
        }

        #endregion
    }
}
