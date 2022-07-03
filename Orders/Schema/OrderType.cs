using GraphQL.Types;
using Orders.Models;
using Orders.Services;

namespace Orders.Schema
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(ICustomerService customers)
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Description);
            FieldAsync<CustomerType>("customer",resolve: async context => await customers.GetCustomerByIdAsync(context.Source.CustomerId));
            Field(o => o.Created);
        }
    }
}
