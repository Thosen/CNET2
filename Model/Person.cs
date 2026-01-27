using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address? Address { get; set; }
        public List<Contract> Contracts { get; set; }
    }
}
