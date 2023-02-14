using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Model
{
    public class Company
    {
        public int IDCompany { get; set; }
        public string NameCompany { get; set; }
        public string OwnerCompany { get; set; } // בעלים
        public string EmailCompany { get; set; }
        public string PhoneCompany { get; set; }

    }
}
