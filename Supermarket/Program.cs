using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Timers;

namespace Supermarket
{
    class Program
    {
        private static ConcurrentQueue<Customer> _shoppingQueue;

        static void Main(string[] args)
        {
            // Initialize queue
            _shoppingQueue = new ConcurrentQueue<Customer>();

            // Set the timer that adds a new customer every second
            SetShopperTimer();

            // Start 5 cashier workers
            for (int i = 0; i < Consts.CASHIER_COUNT; i++)
            {
                StartCashierWorker(i + 1);
            }

            Console.ReadKey();
        }

        private static void StartCashierWorker(int cashierId)
        {
            var worker = new CashierWorker(cashierId);
            Task.Run(() => worker.Work(_shoppingQueue));
        }

        private static void SetShopperTimer()
        {
            var timer = new Timer(Consts.CUSTOMER_APPEARANCE_INTERVAL);
            timer.Elapsed += AddCustomer;
            timer.AutoReset = true;
            timer.Start();
        }

        private static void AddCustomer(Object source, ElapsedEventArgs e)
        {
            _shoppingQueue.Enqueue(Customer.CreateCustomer());
        }
    }
}
