using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Enum
{
    public enum StatusEnum
    {
        [Description("Available Approval")]
        Available,
        Unavailable,
        Pending,
        Paid,
        Canceled,
        Confirmed,
        Cancelled,
        HasDone,
    }
}
