using DalApi; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    static public class DalFactory
    {
        static public IDal GetDal(string s)
        {
            if (s == "DalObject")
                return DalObject.Instance;
        //    if (s == "DalXml ")
        //        return new DalXml.DalXml();
            throw new ParamsException("Wrong params");
        }
    }
}

