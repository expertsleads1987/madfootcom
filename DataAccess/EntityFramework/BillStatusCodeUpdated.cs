using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public enum BillStatusCodes
    {
        BillNew = 1,
        BillUpdated = 2,
        BillExpired = 3,
        BillPaid = 4,
        BillPartiallyPaid = 5,
        BillOverPaid = 6,
        BillClosed = 7
    }
}
