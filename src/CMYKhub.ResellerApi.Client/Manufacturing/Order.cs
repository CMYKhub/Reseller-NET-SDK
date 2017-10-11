using System;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class Order : ApiResource
    {
        public string OrderId { get; set; }
        public string HubId { get; set; }
        public string ResellerId { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime? DateEstimatedDispatch { get; set; }
        public OrderStatus Status { get; set; }
        public string OrderNumber { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }
    }

}
