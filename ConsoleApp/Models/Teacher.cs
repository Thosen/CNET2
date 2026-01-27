using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class Teacher : GeneralPerson
    {
        public int Payment { get; set; }

        override public string ToString()
        {
            return $"{Surname} ({Age()}), {Payment} ";
        }
    }
}
