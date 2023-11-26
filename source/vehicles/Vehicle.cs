using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    //The "Super-class"
    public interface Vehicle
    {
        String GetVehicleType();

        bool IsTollFreeVehicle();
    }
}