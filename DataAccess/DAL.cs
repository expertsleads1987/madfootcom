using DataAccess;
using MFEP.Common.Security;
using MFEP.Common.Utilities;
using MFEP.ProjectHelpers.Helpers.Entites.Services.BillInquiry;
using MFEP.ProjectHelpers.Helpers.Entites.Services.BillUpload;
using MFEP.ProjectHelpers.Helpers.Entites.Services.PaymentNotification;
using MFEP.ProjectHelpers.Helpers.Enums;
using MFEP.ProjectHelpers.Helpers.Entites.MFEPMessage;
using System.Runtime;
using System.Globalization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess;

namespace DataAccess
{
    public class DAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public BillRecord GetBillInfo(string billNo, string billingNo, string serviceType)
        {

            using (OracleConnection connection = new OracleConnection())
            {
                OracleCommand command = new OracleCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "madfooat_utility.GET_AGREEMENT_INFO",
                    Connection = new OracleConnection(connectionString),
                };
                command.Parameters.AddWithValue("P_CENTER_ID", "7").Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("P_COLLECTOR", "ADMIN").Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("P_AGREEMENT_ID", "1562").Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("P_BALANCE_TYPE", "3").Direction = ParameterDirection.Input;



                command.Parameters.Add("P_NAME",   OracleType.Char, 255).Direction = ParameterDirection.Output;
                command.Parameters.Add("P_BALANCE", OracleType.Int32).Direction = ParameterDirection.Output;
                command.Parameters.Add("P_RESULT", OracleType.Char , 1024).Direction = ParameterDirection.Output;

                command.Connection.Open();
                command.ExecuteNonQuery();
                BillRecord billRecord = null;
                decimal dueAmount = 0;
                dueAmount = command.Parameters["P_BALANCE"].Value != DBNull.Value ? Convert.ToDecimal(command.Parameters["P_BALANCE"].Value) : 0;
                billRecord = new BillRecord()
                {
                    AccountInfo = new AccountInfo()
                    {
                        BillingNumber = billingNo,
                        BillNumber = billNo,
                    },
                    BillStatus = BillStatusCodes.BillNew,
                    DueAmount = dueAmount,
                    IssueDate = DateTime.Now,
                    DueDate = DateTime.Now,
                    ServiceType = "Payment",
                };

                command.Connection.Close();
                return billRecord;
            }
        }
        public void InsertPaymentNotification(string billingNo)
        {
            OracleCommand command = new OracleCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_IND_PAYMENT_PAY",
                Connection = new OracleConnection(connectionString),
            };
            command.Parameters.AddWithValue("REQ_REFERENCE", billingNo);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
    }

    
}
