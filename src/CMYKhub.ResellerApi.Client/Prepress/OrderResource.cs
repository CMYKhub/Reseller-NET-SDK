namespace CMYKhub.ResellerApi.Client.Prepress
{
    public class OrderResource : ApiResource
    {
        public string OrderId { get; set; }
        public string PrepressDepartmentId { get; set; }
        public OrderStatusResource Status { get; set; }
        public FileUploadResource[] Files { get; set; }
    }
}
