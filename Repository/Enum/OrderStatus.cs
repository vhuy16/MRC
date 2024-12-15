using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Enum
{
    public enum OrderStatus
    {
        PENDING_PAYMENT,
        SHIPPING,
        PENDING_DELIVERY,
        COMPLETED,
        CANCELLED
    }
}
