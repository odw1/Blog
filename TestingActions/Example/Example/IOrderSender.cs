using System.Collections.Generic;

namespace Example
{
    public interface IOrderSender
    {
        void Send(Order order);
        void Send(List<Order> orders);
    }
}