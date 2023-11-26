using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Emergency : Vehicle
    {
        public string GetVehicleType()
        {
            return "Emergency";
        }

        public bool IsTollFreeVehicle(){
            return true;
        }
        
    }
}
