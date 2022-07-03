using GraphQL.Types;

namespace Orders.Schema
{
    public class OrderStatusesEnum : EnumerationGraphType
    {
        public OrderStatusesEnum()
        {
            Name = "OrderStatus";
            Add("CREATED", 2, "Order was created");
            Add("PROCESSING", 4, "Order is being processed");
            Add("COMPLETED", 8,  "Order is completed");
            Add("CANCELLED", 16 , "Order was cancelled");
            Add("CLOSED", 32, "Order was closed");
        }
    }
}
