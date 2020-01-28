using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * RandomVehicles handels the creation of
 * random valid vehicles from a given set of
 * years, makes, and models.
 */
public class RandomVehicles
{
    public static Random randomGen = new Random();
    public const int MIN_YEAR = 1950;
    public const int MAX_YEAR = 2050;
    public static string[] makes = { "Toyota", "Volkswagen Group", "Hyundai", "General Motors", "Ford", "Nissan", "Honda", "BMW" };
    public static string[] models = { "2019 Acura NSX", "2019 Acura RDX", "2019 Aston Martin Vantage", "2019 BMW 330i", "2019 BMW 8 Series",
                                "2019 BMW Z4","2019 Ferrari Portofino","2020 AcuraRLX","2020 Alfa Romeo4C"};

    public Vehicle next()
    {
        return RandomVehicle();
    }

    public static int RandomYears(int min = MIN_YEAR, int max = MAX_YEAR)
    {
        return randomGen.Next(min, max);
    }

    public static int RandomIDs(int min, int max)
    {
        return randomGen.Next(min, max);
    }

    public static string RandomMakes()
    {
        return makes[randomGen.Next(0, makes.Length - 1)];
    }

    public static Vehicle RandomVehicle(int IDMin = 0, int IDMax = int.MaxValue,
                                  int YearMin = MIN_YEAR, int YearMax = MAX_YEAR)
    {
        int Id = RandomIDs(IDMin, IDMax);
        int Year = RandomYears(YearMin, YearMax);
        string Make = RandomMakes();
        string Model = RandomModels();
        return new Vehicle(Id, Year, Make, Model);
    }

    public static string RandomModels()
    {
        return models[randomGen.Next(0, models.Length - 1)];
    }
}