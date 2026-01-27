using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public interface IAdresa
    {
        public string FullAddress();
        public bool IsValid();
    }
}
