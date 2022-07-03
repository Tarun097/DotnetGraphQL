using GraphQL.Instrumentation;
using System;

namespace Orders.Schema
{
    public class OrdersSchema : GraphQL.Types.Schema
    {
        public OrdersSchema(IServiceProvider provider) : base(provider)
        {
            Query = (OrdersQuery)provider.GetService(typeof(OrdersQuery)) ?? throw new InvalidOperationException();

            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }
}
