using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class Student : Person
    {
        // Jmeno, Prijmeni, RokNarozeni, Trida, Adresa

        public string Trida { get; set; } = string.Empty;
        public IAdresa Adresa { get; set; }



        override public string ToString()
        {
            return $"{Surname} ({Age()}), {Trida}, {Adresa.FullAddress()} ";
        }
    }
}
