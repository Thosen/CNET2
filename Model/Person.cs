using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Person
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Range(0, 20)]
        public int ChildrenCount { get; set; }
        [Range(0, 99)]
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address? Address { get; set; }
        public List<Contract> Contracts { get; set; }
    }
}
