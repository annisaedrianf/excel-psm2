using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excel.Module.BusinessObjects
{
   public enum eStatusPym
    {
        [XafDisplayName("Unpaid")]
        Unpaid = 0,
        [XafDisplayName("Under-payment")]
        UnderPayment = 1,
        [XafDisplayName("Paid")]
        Paid = 2
    }
}
