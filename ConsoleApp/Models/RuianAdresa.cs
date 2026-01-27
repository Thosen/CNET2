using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class RuianAdresa : IAdresa
    {
        public int KodObce { get; set; }
        public string NazevObce { get; set; } = string.Empty;
        public int? KodUlice { get; set; }
        public string NazevUlice { get; set; } = string.Empty;
        public int CisloDomovni { get; set; }
        public int PSC { get; set; }

        public string FullAddress()
        {
            return $"{NazevUlice}, {PSC}, {NazevObce}, {CisloDomovni}";
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
