using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Supermarket
{
    public class CashierWorker
    {
        private int _cachierId;
        public CashierWorker(int cachierId)
        {
            _cachierId = cachierId;

            Console.WriteLine(string.Format("Cashier #{0} initialized", _cachierId));
        }
        public void Work(ConcurrentQueue<Customer> customerQueue)
        {
            Console.WriteLine(string.Format("Cashier #{0} Starting Work", _cachierId));
            while (true)
            {
                if (customerQueue.IsEmpty)
                {
                    Console.WriteLine(string.Format("Shopper Queue is empty. Cashier #{0} waiting", _cachierId));
                    Thread.Sleep(Consts.CUSTOMER_APPEARANCE_INTERVAL / 2); // The waiting time between customer to customer
                }
                else
                {
                    Customer customer;
                    if (customerQueue.TryDequeue(out customer))
                    {
                        Console.WriteLine(string.Format("Cashier #{0} attending to {1} with {2} items in cart.", _cachierId, customer.Name, customer.CartSize));
                        Thread.Sleep(1000 * customer.CartSize);
                    }
                }
            }
        }
    }
}
