using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public enum PaymentStatus
    {
        PmtNew = 1,
        PmtUpdated = 2,
        PmtTransf = 3,
        PmtReversal = 5,
        PmtRecon = 6,
        PmtMisMatch = 7,
        PmtNotInBank = 8,
        PmtNotInMFEP = 9,
        PmtNotAdviced = 10,
        PmtAlreadyRecon = 11,
        PmtDuplicate = 12,
        PmtBillerMistmatch = 13,
        PmtComplt = 14,
        PmtSent = 17
    }
}
