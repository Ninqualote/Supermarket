using System;

namespace Supermarket
{
    public class Customer
    {
        private static Random Random = new Random();
        public string Name { get; set; }

        public int CartSize { get; set; }

        private Customer(int size, string name)
        {
            Name = name;
            CartSize = size;
        }

        public static Customer CreateCustomer()
        {
            return new Customer(Random.Next(1, 6), Consts.NAME_COLLECTION[Random.Next(0, Consts.NAME_COLLECTION.Length)]);
        }
    }
}
