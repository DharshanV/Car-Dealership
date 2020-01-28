using System;
using System.Collections.Generic;

/*
 * DealershipInterface handels the interactions 
 * between the user and the Dealership, such as
 * executing inputs get, create, and delete. It is
 * also resposible for making unique Id's i.e keeping
 * an Id counter. 
 * 
 * Only invalid inputs (cast errors) are handeled here,
 * while validating on that input is done by the Dealership
 * class.
 */
public class DealershipInterface
{
    private Dealership dealership = new Dealership();
    private RandomVehicles randomVehicles = new RandomVehicles();
    int uniqueID = 1;

    public void GetVehicles()
    {
        Print(dealership.GetVehicles());
    }

    /*
     * Gets input ID from the user, and
     * trys to retrive the vehicle. Throws
     * an expection if the input is not an
     * integer.
     */
    public void GetVehicles_ID()
    {
        int ID = -1;
        try
        {
            ID = GetID();
            Print(dealership.GetVehicles(ID));
        }
        catch (System.IO.IOException)
        {
            Console.WriteLine(ResponeMessages.INVALID_ID);
        }
    }

    public void GetVehicles_Make()
    {
        string make = GetMake();
        Print(dealership.GetVehicles(make));
    }

    /*
     * Gets all the vehicles between two valid
     * years.
     */ 
    public void GetVehicles_MinYear_MaxYear()
    {
        try
        {
            int minYear = GetYear("MinYear: ");
            int maxYear = GetYear("MaxYear: ");
            Print(dealership.GetVehicles(minYear,maxYear));
        }
        catch (System.IO.IOException)
        {
            Console.WriteLine(ResponeMessages.INVALID_INPUT_CAST);
        }
    }

    /*
     * Creates a vehicle specified through
     * user inputs (make, model, and year).
     * The vehicle ID is handeled by the class,
     * by making it unique (1 - int.MaxValue).
     */ 
    public void CreateVehicles()
    {
        try
        {
            string make = GetMake();
            string model = GetModel();
            int year = GetYear();
            if(dealership.CreateVechicle(new Vehicle(uniqueID, year, make, model)))
            {
                Console.WriteLine(ResponeMessages.CREATED_VEHICLE_WITH_ID + uniqueID.ToString());
                uniqueID++;
            }
        }
        catch (System.IO.IOException)
        {
            Console.WriteLine(ResponeMessages.INVALID_INPUT_CAST);
        }
    }

    /*
     * Updates a vehicle in the dealership
     * with valid ID. Prints to indicate
     * previous values. Updates with user
     * inputs.
     */ 
    public void UpdateVehicles()
    {
        try
        {
            int id = GetID();
            Vehicle vehicle = dealership.GetVehicles(id);
            if (vehicle == null) return;
            Console.WriteLine(vehicle);

            string make = GetMake("\nNew Make: ");
            string model = GetModel("New Model: ");
            int year = GetYear("New Year: ");

            if (dealership.UpdateVehicle(id,new Vehicle(id, year, make, model)))
                Console.WriteLine("\n" + ResponeMessages.CREATED_VEHICLE_WITH_ID + id.ToString());
        }
        catch (System.IO.IOException)
        {
            Console.WriteLine(ResponeMessages.INVALID_INPUT_CAST);
        }
    }

    public void DeleteVehicles_ID()
    {
        try {
            int ID = GetID();

            if (dealership.DeleteVehicles(ID))
                Console.WriteLine("\n" + ResponeMessages.SUCCESSFULLY_DELETED_VEHICLE + ID.ToString());
            else
                Console.WriteLine("\n" + ResponeMessages.SOMETHING_WENT_WRONG);
        }
        catch (System.IO.IOException)
        {
            Console.WriteLine(ResponeMessages.INVALID_ID);
        }
    }

    /*
     * Creates a valid random vehicle, handeled
     * by the RandomVehicles class
     */
    public void CreateVehicle_Random()
    {
        Vehicle vehicle = randomVehicles.next();
        if (dealership.CreateVechicle(vehicle))
        {
            Console.WriteLine(ResponeMessages.CREATED_VEHICLE_WITH_ID + vehicle.Id.ToString());
        }
    }

    private void Print(Vehicle vehicle)
    {
        if (vehicle == null) return;
        Console.WriteLine(vehicle);
    }

    private void Print(LinkedList<Vehicle> vehicles)
    {
        if (vehicles == null) return;
        foreach(Vehicle vehicle in vehicles)
        {
            Console.WriteLine(vehicle);
        }
    }

    private int GetID(string message = "ID: ")
    {
        Console.Write(message);
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int inputNum)) throw new System.IO.IOException();
        return inputNum;
    }

    private int GetYear(string message = "Year: ")
    {
        Console.Write(message);
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int inputNum)) throw new System.IO.IOException();
        return inputNum;
    }

    private string GetMake(string message = "Make: ")
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    private string GetModel(string message = "Model: ")
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}