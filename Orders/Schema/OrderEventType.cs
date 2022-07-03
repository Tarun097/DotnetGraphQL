using GraphQL.Types;
using Orders.Models;

namespace Orders.Schema
{
    public class OrderEventType : ObjectGraphType<OrderEvent>
    {
        public OrderEventType()
        {
            Field(e => e.Id);
            Field(e => e.Name);
            Field(e => e.OrderId);
            Field(e => e.Status);
            Field(e => e.Timestamp);
        }
    }
}
