using System;
using System.Collections.Generic;
using System.Linq;

namespace Example
{
    public interface IOrderBatcher
    {
        void ProcessBatches(List<Order> orders, Action<List<Order>> batchProcessor);
    }

    public class OrderBatcher : IOrderBatcher
    {
        private readonly int _batchSize = 5;

        public void ProcessBatches(List<Order> orders, Action<List<Order>> batchProcessor)
        {
            var batches = new List<List<Order>>();

            while (true)
            {
                var batch = orders.Skip(batches.Count * _batchSize).Take(_batchSize).ToList();

                if (!batch.Any())
                {
                    break;
                }

                batches.Add(batch);
            }

            batches.ForEach(batchProcessor);
        }
    }
}