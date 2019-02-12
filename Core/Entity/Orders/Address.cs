using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity.Orders
{
    public class Address
    {
        public string Street { get; private set; }

        public string City { get; private set; }


        public string Country { get; private set; }


        private Address() { }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            Country = country;
        }
    }
}
