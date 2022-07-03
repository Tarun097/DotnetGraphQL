using GraphQL;
using GraphQL.Types;
using GraphQL.Resolvers;
using Orders.Services;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;


namespace Orders.Schema
{
    public class OrdersSubscription : ObjectGraphType<object>
    {
        private readonly IOrderEventService orderEventService;

        public OrdersSubscription(IOrderEventService orderEventService)
        {
            this.orderEventService = orderEventService;
            Name = "Subscription";

            AddField(new FieldType
            {
                Name = "orderEvent",
                Arguments = new QueryArguments(new QueryArgument<ListGraphType<OrderStatusesEnum>>
                {
                    Name = "statuses"
                }),
                Type = typeof(OrderEventType),
                Resolver = new FuncFieldResolver<OrderEvent>(ResolveEvent),
                StreamResolver = new SourceStreamResolver<OrderEvent>(Subscribe)
            });

        }

        private IObservable<OrderEvent> Subscribe(IResolveFieldContext context)
        {
            var statusList = context.GetArgument<IList<OrderStatuses>>("statuses", new List<OrderStatuses>());

            if (statusList.Count > 0)
            {
                OrderStatuses statuses = 0;

                foreach (var status in statusList)
                {
                    statuses = statuses | status;
                }
                return orderEventService.EventStream().Where(e => (e.Status & statuses) == e.Status);
            }
            else
            {
                return orderEventService.EventStream();
            }
        }

        private OrderEvent ResolveEvent(IResolveFieldContext context)
        {
            var orderEvent = context.Source as OrderEvent;
            return orderEvent;
        }

    }
}
