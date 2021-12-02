using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelAtTransfer
    {
        public int Id { get; set; }
        public bool SateOfParcel { get; set; }
        public Priorities Priority { get; set; }
        public WeightCategories Weight { get; set; }
        public CustomerAtParcel Sender { get; set; }
        public CustomerAtParcel Getter { get; set; }
        public Location LocationOfPickUp { get; set; }
        public Location LocationOfTarget { get; set; }
        public double DistanceOfDelivery { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Sate Of Parcel: ";
            if (SateOfParcel)
                result += "Parcel in way to the destination";
            else
                result += "Parcel wait to pick up";
        
            result += " Priority: " + Priority;
            result += " Weight: " + Weight;
            result += " Sender: " + Sender;
            result += " Target: " + Getter;
            result += " Location of pick up: " + LocationOfPickUp;
            result += " Location of target: " + LocationOfTarget;
            result += " Location of delivery: " + DistanceOfDelivery;
            result += "\n";
            return result;
        }

    }
}
