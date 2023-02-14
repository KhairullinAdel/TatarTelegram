using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatarTelegram.Data
{
    public partial class TatarTelegaEntities
    {
        private static TatarTelegaEntities context;
        public static TatarTelegaEntities GetContext()
        {
            if (context == null)
            {
                context = new TatarTelegaEntities();  
            }
            return context;
        }
    }
}
