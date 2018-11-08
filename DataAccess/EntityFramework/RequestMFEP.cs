using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.EntityFramework
{
    public class RequestMFEP
    {
        public RequestMsgHeader MsgHeader {get; set;}

        public RequestMsgBody MsgBody { get; set; }

        public RequestMsgFooter MsgFooter { get; set; }

    }
}