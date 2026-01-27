using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class TextAddress : IAdresa
    {
        public string Ulice { get; set; } = string.Empty;
        public string Mesto { get; set; } = string.Empty;
        public string PSC { get; set; } = string.Empty;

        public string FullAddress()
        {
            return $"{Ulice}, {Mesto}, {PSC}";
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Ulice) == false &&
                   string.IsNullOrEmpty(Mesto) == false &&
                   string.IsNullOrEmpty(PSC) == false;
        }
    }
}
