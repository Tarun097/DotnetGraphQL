using GraphQL;
using GraphQL.Types;
using Orders.Models;
using Orders.Services;
using System;

namespace Orders.Schema
{
    public class OrdersMutation : ObjectGraphType<object>
    {
        public OrdersMutation(IOrderService orders)
        {
            Name = "Mutation";

            FieldAsync<OrderType>(
                "createOrder",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OrderCreateInputType>> { Name = "order" }),
                resolve: async context =>
                {
                    var orderInput = context.GetArgument<OrderCreateInput>("order");
                    var id = Guid.NewGuid().ToString();
                    var order = new Order(orderInput.Name, orderInput.Description, orderInput.Created, orderInput.CustomerId, id);
                    return await orders.CreateAsync(order);
                }
            );

            FieldAsync<OrderType>(
                "startOrder",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "orderId" }),
                resolve: async context =>
                {
                    var orderId = context.GetArgument<string>("orderId");
                    return await orders.StartAsync(orderId);
                }
            );
        }
    }
}
