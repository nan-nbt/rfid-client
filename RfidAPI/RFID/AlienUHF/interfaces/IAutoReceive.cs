using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFAPP.interfaces
{
    public interface IAutoReceive
    {       
         bool Connect();
         void DisConnect();
    }
}
