using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Enums
{
    public enum VolunteerApplicationAction
    {
        Pending,
        Approved,
        Rejected,
        Started,
        CompletionRequested,
        WithdrawalRequested, // 👈 add this
        Completed,
        Cancelled
    }
}
