using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PromoIt.Entitis.Logger;

namespace PromoIt.Entitis
{
    public class BasePromotion
    {
        public BasePromotion(Logger Log)
        {
            LogManager = Log;
        }

        public Logger LogManager { get; set; }

    }
}
