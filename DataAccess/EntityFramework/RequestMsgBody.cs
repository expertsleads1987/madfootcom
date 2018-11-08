using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.EntityFramework
{
    public class RequestMsgBody
    {

        public RequestAcctInfo AcctInfo { get; set; }

        public string ServiceType { get; set; }

    }
}