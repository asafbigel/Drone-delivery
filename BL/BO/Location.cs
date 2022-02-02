using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{/// <summary>
/// Location Class to represent a Location point by longitude and latitude   in the Business layer
/// </summary>
    public class Location
    {
        /// <summary>
        /// Longitude of the Location point
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Latitude of the Location point
        /// </summary>
        public double Latitude { get; set; }

        public override string ToString()
        {
            double myLongitude = Longitude;
            double myLatitude = Latitude;
            // north or sorsh
            string ns = "N";
            if (myLatitude < 0)
            {
                ns = "S";
                myLatitude = -myLatitude; //if it was negetive it will be positive
            }
            int deg1 = (int)myLatitude;   // the integer of Latitude is the degree
            int min1 = (int)(60 * (myLatitude - deg1)); //Multiply the rest by 60 and take the integer to calculate minutes
            double sec1 = (myLatitude - deg1) * 3600 - min1 * 60; //Multiply the rest by 3600 and take the integer to calculate second
            string lat = $"{deg1}°{min1}′{sec1:0.0}″{ns}";  // the latitude

            // east or west
            string ew = "E";
            if (myLongitude < 0)
            {
                ew = "W";
                myLongitude = -myLongitude; //if it was negetive it will be positive
            }

            int deg2 = (int)myLongitude;      // the integer of Latitude is the degree
            int min2 = (int)(60 * (myLongitude - deg2));  //Multiply the rest by 60 and take the integer to calculate minutes
            double sec2 = (myLongitude - deg2) * 3600 - min2 * 60; //Multiply the rest by 3600 and take the integer to calculate second
            string lon = $"{deg2}°{min2}′{sec2:0.0}″{ew}"; //the longitude


            string result = "";
            result += lat;
            result += "\t";
            result+= lon;
            return result;
        }
    }

}

