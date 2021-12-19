using BlApi;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BlApi
{
    static public class DalFactory
    {
        static public IBL GetBL()
        {
            return BL.Instance;
        }
    }
}

