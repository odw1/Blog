using System;
using System.Collections.Generic;
using System.Text;

namespace Example
{
    public class OrderProcessor
    {
        private readonly IOrderBatcher _orderBatcher;
        private readonly IOrderSender _orderSender;
        private readonly IOrderRecorder _orderRecorder;

        public OrderProcessor(IOrderBatcher orderBatcher, IOrderSender orderSender, IOrderRecorder orderRecorder)
        {
            _orderBatcher = orderBatcher;
            _orderSender = orderSender;
            _orderRecorder = orderRecorder;
        }

        public void Process(List<Order> orders, bool processInBatches)
        {
            if (processInBatches)
            {
                Action<List<Order>> batchProcessor = batchedOrders =>
                                                         {
                                                             _orderSender.Send(batchedOrders);
                                                             batchedOrders.ForEach(x => _orderRecorder.OrderSent(x));
                                                         };

                _orderBatcher.ProcessBatches(orders, batchProcessor);
            }
            else
            {
                foreach (var order in orders)
                {
                    _orderSender.Send(order);
                    _orderRecorder.OrderSent(order);
                }
            }
        }
    }
}
