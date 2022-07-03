using GraphQL.Types;
using Orders.Services;

namespace Orders.Schema
{
    public class OrdersQuery : ObjectGraphType<object>
    {
        public OrdersQuery(IOrderService orders)
        {
            Name = "Query";

            FieldAsync<ListGraphType<OrderType>>("orders", resolve: async context => await orders.GetOrdersAsync());
        }
    }
}