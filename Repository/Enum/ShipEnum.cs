using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Enum
{
    public enum ShipEnum
    {
        NewOrder = 900,
        WaitingForPickup = 901,
        PickingUp = 902,  
        PickedUp = 903,
        Delivering = 904,            
        DeliveredSuccessfully = 905,  
        DeliveryFailed = 906,        
        Returning = 907,              
        Returned = 908,               
        ReconciledWithCompany = 909,  
        ReconciledWithCustomer = 910,  
        CodTransferredToCustomer = 911,
        AwaitingCodPayment = 912,  
        Completed = 913,
        Canceled = 914, 
        PickupOrDeliveryDelay = 915,   
        PartialDelivery = 916,          
        ErrorOrder = 1000
    }
}
