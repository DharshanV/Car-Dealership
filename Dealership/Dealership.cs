using System.Collections.Generic;
using System;

/*
 * Dealership keeps track of all active vehicles through CRUD
 * operations.
 * 
 * The Dealership meets the requirements of:
 * 1. GET vehicles
 * 2. GET vehicles/{id}
 * 3. GET vehicles/{make}
 * 4. GET vehicles/{minYear}-{maxYear}
 * 5. CREATE vehicles
 * 6. UPDATE vehicles
 * 7. DELETE vehicles/{id}
 * 
 * It uses the Dictionary (HashTable) to keep map between ID->Vehicle,
 * Year->Vehicles, and Make->Vehicles. This can be easily further extened to support
 * other search queries. By adding appropriate function calls.
 * 
 * This also handles if certian keys are present, not present, invalid, or repeated
 * id inputs. And if certian Vehicles are valid.
 */
public class Dealership
{
    private const bool DEBUG = DealershipConfig.DEBUG;
    private const int MIN_YEAR = DealershipConfig.MIN_YEAR;
    private const int MAX_YEAR = DealershipConfig.MAX_YEAR;
    private Dictionary<int, Vehicle> IDMap = new Dictionary<int, Vehicle>();
    private Dictionary<int, LinkedList<Vehicle>> YearMap = new Dictionary<int, LinkedList<Vehicle>>();
    private Dictionary<string, LinkedList<Vehicle>> MakeMap = new Dictionary<string, LinkedList<Vehicle>>();

    /*
     * Goes through all the key's (ID's) and appends all vehicles to linked list.
     */
    public LinkedList<Vehicle> GetVehicles()
    {
        LinkedList<Vehicle> allVehicles = new LinkedList<Vehicle>();
        foreach (KeyValuePair<int, Vehicle> pair in IDMap)
        {
            allVehicles.AddLast(pair.Value);
        }
        return allVehicles;
    }

    /*
     * Try to get vehicle from IDMap, if not returns a null object.
     */
    public Vehicle GetVehicles(int id)
    {
        Vehicle vehicle;
        if (!IDMap.TryGetValue(id, out vehicle))
            Error(DEALERSHIP_ERRORS.NOT_EXISTS, id);
        return vehicle;
    }

    /*
     * Goes thru MakeMap, where all the map of makes to vehicles are
     * addressed, and is appened for return (null if make does not exist).
     */
    public LinkedList<Vehicle> GetVehicles(string make)
    {
        LinkedList<Vehicle> vehicles;
        if (!MakeMap.TryGetValue(make.ToLower(), out vehicles))
            Error(DEALERSHIP_ERRORS.INVALID_MAKE);
        return vehicles;
    }

    /*
     * Goes from minYear to maxYear, and gets all vehicles between the years
     * from the YearMap.
     */
    public LinkedList<Vehicle> GetVehicles(int minYear, int maxYear)
    {
        LinkedList<Vehicle> vehicles = null;
        if (minYear < MIN_YEAR || maxYear > MAX_YEAR || maxYear < minYear)
        {
            Error(DEALERSHIP_ERRORS.INVALID_YEAR);
            return vehicles;
        }

        vehicles = new LinkedList<Vehicle>();
        for (int year = minYear; year <= maxYear; year++)
        {
            LinkedList<Vehicle> yearVehicles;
            if (!YearMap.TryGetValue(year, out yearVehicles)) continue;
            foreach (Vehicle vehicle in yearVehicles)
            {
                vehicles.AddLast(vehicle);
            }
        }
        return vehicles;
    }

    /*
     * Updates a existing vehicle with Id.
     * It first need to check if the new vehicle is valid (i.e. unique id).
     * Deletes the old vehicle in IDMap, MakeMap, and YearMap.
     * If the make or the year where to change, then the new vehicle would be in the wrong place
     * in other maps. To fix, just remove the the old vehicle and insert new vehicle.
     */
    public bool UpdateVehicle(int Id, Vehicle updatedVehicle)
    {
        if (!ValidateVehicle(updatedVehicle)) return false;
        Vehicle prevVehicle;
        if (!IDMap.TryGetValue(Id, out prevVehicle))
        {
            Error(DEALERSHIP_ERRORS.NOT_EXISTS, Id);
            return false;
        }
        if (updatedVehicle.Id != Id && IDMap.ContainsKey(updatedVehicle.Id))
        {
            Error(DEALERSHIP_ERRORS.EXISTS, updatedVehicle.Id);
            return false;
        }
        DeleteVehicles(prevVehicle);
        CreateVechicle(updatedVehicle);
        return true;
    }

    /*
     * Deletes the vehicle from IDMap, MakeMap, and YearMap.
     */
    private bool DeleteVehicles(Vehicle removeVehicle)
    {
        LinkedList<Vehicle> vehicles;
        if (!MakeMap.TryGetValue(removeVehicle.Make.ToLower(), out vehicles))
            return false;
        vehicles.Remove(removeVehicle);

        if (!YearMap.TryGetValue(removeVehicle.Year, out vehicles))
            return false;
        vehicles.Remove(removeVehicle);

        return IDMap.Remove(removeVehicle.Id);
    }

    /*
     * Deletes vehicle with id only if its exists.
     */
    public bool DeleteVehicles(int id)
    {
        Vehicle vehicle;
        if (!IDMap.TryGetValue(id, out vehicle))
            return false;
        return DeleteVehicles(vehicle);
    }

    /*
     * Inserts a valid vehicle into maps, for retrival.
     * Only adds vehicle with unique Id's.
     * Then each vehicle is mapped with respect to there type
     * and year. Vehicle's might have many same makes and years,
     * hence linked list.
     */
    public bool CreateVechicle(Vehicle vehicle)
    {
        if (!ValidateVehicle(vehicle))
            return false;

        if (IDMap.TryGetValue(vehicle.Id, out Vehicle vehicleTemp))
        {
            Error(DEALERSHIP_ERRORS.EXISTS);
            return false;
        }
        IDMap.Add(vehicle.Id, vehicle);

        LinkedList<Vehicle> mapVehicles;
        if (!MakeMap.TryGetValue(vehicle.Make.ToLower(), out mapVehicles))
        {
            mapVehicles = new LinkedList<Vehicle>();
            MakeMap.Add(vehicle.Make.ToLower(), mapVehicles);
        }
        mapVehicles.AddLast(vehicle);

        if (!YearMap.TryGetValue(vehicle.Year, out mapVehicles))
        {
            mapVehicles = new LinkedList<Vehicle>();
            YearMap.Add(vehicle.Year, mapVehicles);
        }
        mapVehicles.AddLast(vehicle);

        return true;
    }

    private bool ValidateVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        if (vehicle.Id < 0) return false;
        if (vehicle.Make.Length == 0) return false;
        if (vehicle.Model.Length == 0) return false;
        if (vehicle.Year < MIN_YEAR || vehicle.Year > MAX_YEAR) return false;
        return true;
    }

    private void Error(DEALERSHIP_ERRORS type, int vehicleID = -1)
    {
        if (!DEBUG) return;
        switch (type)
        {
            case DEALERSHIP_ERRORS.NOT_EXISTS:
                Console.WriteLine(ResponeMessages.VEHICLE_ID_NOT_EXISTS, vehicleID);
                break;
            case DEALERSHIP_ERRORS.EXISTS:
                Console.WriteLine(ResponeMessages.VEHICLE_ID_ALREADY_EXISTS, vehicleID);
                break;
            case DEALERSHIP_ERRORS.INVALID_VEHICLE:
                Console.WriteLine(ResponeMessages.INVALID_VEHICLE);
                break;
            case DEALERSHIP_ERRORS.INVALID_YEAR:
                Console.WriteLine(ResponeMessages.YEAR_MUST_BE_BETWEEN_1950_AND_2050);
                break;
            case DEALERSHIP_ERRORS.INVALID_MAKE:
                Console.WriteLine(ResponeMessages.VEHICLE_MAKE_NOT_EXISTS);
                break;
            default:
                Console.WriteLine(ResponeMessages.SOMETHING_WENT_WRONG);
                break;
        }
    }
}