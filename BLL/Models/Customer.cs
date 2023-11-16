using DAL;
using System;

namespace BLL
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CustomerModel() { }
        public CustomerModel(Customer c)
        {
            Id = c.id;
            Name = String.Join(" ", c.name, c.surname);
        }
    }
}
