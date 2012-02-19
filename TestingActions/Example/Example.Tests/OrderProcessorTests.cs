using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;

namespace Example.Tests
{
    [TestFixture]
    public class OrderProcessorTests
    {
        private Mock<IOrderBatcher> _orderBatcher;
        private Mock<IOrderSender> _orderSender;
        private Mock<IOrderRecorder> _orderRecorder;
        private OrderProcessor _orderProcessor;

        [SetUp]
        public void SetUp()
        {
            _orderBatcher = new Mock<IOrderBatcher>();
            _orderSender = new Mock<IOrderSender>();
            _orderRecorder = new Mock<IOrderRecorder>();

            _orderProcessor = new OrderProcessor(_orderBatcher.Object, _orderSender.Object, _orderRecorder.Object);            
        }

        [Test]
        public void when_processing_orders_in_batches()
        {
            var order1 = new Order();
            var order2 = new Order();
            var orders = new List<Order> {order1, order2};

            Action<List<Order>> batchProcessor = null;
            _orderBatcher.Setup(x => x.ProcessBatches(orders, It.IsAny<Action<List<Order>>>())).Callback<List<Order>, Action<List<Order>>>((x, y) => batchProcessor = y);

            _orderProcessor.Process(orders, true);

            _orderBatcher.Verify(x => x.ProcessBatches(orders, batchProcessor), "It should process the order in batches");

            batchProcessor(orders);
            _orderSender.Verify(x => x.Send(orders), "It should send the orders");
            _orderRecorder.Verify(x => x.OrderSent(order1), "It should record the 1st order");
            _orderRecorder.Verify(x => x.OrderSent(order2), "It should record the 2nd order");
        }

        [Test]
        public void when_processing_orders_individually()
        {
            var order1 = new Order();
            var order2 = new Order();
            var orders = new List<Order> { order1, order2 };

            _orderProcessor.Process(orders, false);

            _orderSender.Verify(x => x.Send(order1), "It should send the 1st order");
            _orderSender.Verify(x => x.Send(order2), "It should send the 2nd order");

            _orderRecorder.Verify(x => x.OrderSent(order1), "It should record the 1st order");
            _orderRecorder.Verify(x => x.OrderSent(order2), "It should record the 2nd order");
        }
    }
}
