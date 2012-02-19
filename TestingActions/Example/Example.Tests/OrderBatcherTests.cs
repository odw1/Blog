using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Example.Tests
{
    [TestFixture]
    public class OrderBatcherTests
    {
        [Test]
        public void when_processing_batches()
        {
            var orders = Enumerable.Range(1, 20).Select(x => new Order {Id = x}).ToList();

            var batches = new List<List<Order>>();

            Action<List<Order>> action = batches.Add;

            var orderBatcher = new OrderBatcher();
            orderBatcher.ProcessBatches(orders, action);

            Assert.That(batches.Count, Is.EqualTo(4), "It should have processed the orders in 4 batches");

            Assert.That(batches.SingleOrDefault(x => x.First().Id == 1 && x.Last().Id == 5), Is.Not.Null, "It should have processed the 1st batch");
            Assert.That(batches.SingleOrDefault(x => x.First().Id == 6 && x.Last().Id == 10), Is.Not.Null, "It should have processed the 2nd batch");
            Assert.That(batches.SingleOrDefault(x => x.First().Id == 11 && x.Last().Id == 15), Is.Not.Null, "It should have processed the 3rd batch");
            Assert.That(batches.SingleOrDefault(x => x.First().Id == 16 && x.Last().Id == 20), Is.Not.Null, "It should have processed the 4th batch");
        }
    }
}
