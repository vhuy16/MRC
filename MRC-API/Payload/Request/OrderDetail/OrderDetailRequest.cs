﻿namespace MRC_API.Payload.Request.OrderDetail
{
    public class OrderDetailRequest
    {
       public Guid paymentId { get; set; }
       public Guid productId {  get; set; }
       public int quantity { get; set; }
    }
}
