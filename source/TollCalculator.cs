using System;
using System.Globalization;
using TollFeeCalculator;

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int CalculateTotalTollFee(Vehicle vehicle, DateTime[] dates)
    {   
        if(IsTollFreeVehicle(vehicle)) return 0;
        else{
            return GetTotalFee(dates);
        }
    }

    /**
     * Calculate the total fee
     *
     * @param dates - a list of date and times for toll passings
     * @return - the total toll fee
     */

    private int GetTotalFee(DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date);
            int tempFee = GetTollFee(intervalStart); 

            long minutes = CalculateMinutesBetweenPassings(date.Millisecond, intervalStart.Millisecond);

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee; 
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    /**
     * Calculate the minutes between toll passings
     *
     * @param milliSeconds - current time of passing
     * @param startMilliSeconds   - start time for 60min interval
     * @return - the amount of minutes between passings
     */
    private long CalculateMinutesBetweenPassings(long milliSeconds, long startMilliSeconds)
    {
        long diffInMillies = milliSeconds - startMilliSeconds;
        long minutes = diffInMillies/1000/60;
        return minutes;
    }

    /**
     * Calculate the toll fee for a specific passing
     *
     * @param date - date and time of passing
     * @return - the toll fee for the date and time of passing
     */ 
    public int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    /**
     * Checks whether the passing vehicle is toll free
     *
     * @param vehicle - the type of vehicle passing the toll
     * @return - true or false depending if it is a toll free vehicle
     */
    
    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        else return vehicle.IsTollFreeVehicle();
    }

    /**
     * Checks whether the current date is toll free
     *
     * @param date - the time and date of passing
     * @return - true or false depending if it is a toll free date or not
     */
    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }
}