using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {
        // KAV ORECH 
        public double longitude { get; set; }
        // KAV ROHAV
        public double latitude { get; set; }

        public override string ToString()
        {
            // north or sorsh
            string ns = "N";
            if (latitude < 0)
            {
                ns = "S";
                latitude = -latitude;
            }
            int deg1 = (int)latitude;
            int min1 = (int)(60 * (latitude - deg1));
            double sec1 = (latitude - deg1) * 3600 - min1 * 60;
            string lat = $"{deg1}°{min1}′{sec1:0.0}″{ns}";

            // east or west
            string ew = "E";
            if (longitude < 0)
            {
                ew = "W";
                longitude = -longitude;
            }

            int deg2 = (int)longitude;
            int min2 = (int)(60 * (longitude - deg2));
            double sec2 = (longitude - deg2) * 3600 - min2 * 60;
            string lon = $"{deg2}°{min2}′{sec2:0.0}″{ew}";


            string result = "";
            result += lat;
            result += "\t";
            result+= lon;
            return result;
        }
    }

}

