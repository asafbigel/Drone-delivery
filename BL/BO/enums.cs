using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum DroneStatuses
    {
        vacant, maintenance, sending
    }
    
    public enum WeightCategories
    {
        light, medium, heavy
    }
    public enum Priorities
    {
        regular, fast, emergency
    }
    public enum ParcelStatuses
    {
        Defined, Belongs, picked_up, delivered
    }
}
