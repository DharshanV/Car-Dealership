using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ProgramInterface handels the state of the
 * program. Keeps the program running and handels
 * exits. It asks and validates casting of inputs.
 * Later is passed for input execution at DealershipInterface.
 */
public class ProgramInterface
{
    DealershipInterface dealershipInterface = new DealershipInterface();
    const int MAX_INPUT = 9;
    bool IsRunning = true;

    public void Run()
    {
        PrintMenu();
        while (IsRunning)
        {
            Console.WriteLine();
            int input = GetInput();
            if (ValidInput(input)) IsRunning = ExecuteInput(input);
        }
    }

    void PrintMenu()
    {
        Console.WriteLine("1. GET vehicles");
        Console.WriteLine("2. GET vehicles/{id}");
        Console.WriteLine("3. GET vehicles/{make}");
        Console.WriteLine("4. GET vehicles/{minYear}-{maxYear}");
        Console.WriteLine("5. CREATE vehicles");
        Console.WriteLine("6. UPDATE vehicles");
        Console.WriteLine("7. DELETE vehicles/{id}");
        Console.WriteLine("8. CREATE RANDOM vehicles");
        Console.WriteLine("9. Print menu");
        Console.WriteLine("Enter \"-1\" to stop the program");
    }

    /*
     * Handels menu selection inputs and
     * validates if it is an integer.
     */ 
    int GetInput()
    {
        string input;
        int numInput = -1;
        try
        {
            Console.Write(">> ");
            input = Console.ReadLine();
            if (!int.TryParse(input, out numInput))
            {
                throw new System.IO.IOException();
            }
        }
        catch (System.IO.IOException)
        {
            numInput = (int)MAIN_PROGRAM_ENUM.PARSE_FAIL;
        }
        return numInput;
    }

    /*
     * Validates the given input, and outputs
     * correct message to the user to if input
     * was not valid
     */ 
    bool ValidInput(int input)
    {
        if (input == (int)MAIN_PROGRAM_ENUM.PARSE_FAIL)
        {
            Console.WriteLine(ResponeMessages.INVALID_INPUT_CAST);
            return false;
        }
        else if (input == (int)MAIN_PROGRAM_ENUM.INVALID_INPUT)
        {
            Console.WriteLine(ResponeMessages.INVALID_INPUT + input.ToString());
            return false;
        }
        else if (input < -1 || input > MAX_INPUT)
        {
            Console.WriteLine(ResponeMessages.INVALID_INPUT + input.ToString());
            return false;
        }
        return true;
    }

    /*
     * Last stage which calls the appropriate 
     * DealershipInterface functions or quits the 
     * program.
     */
    bool ExecuteInput(int input)
    {
        Console.WriteLine();
        switch (input)
        {
            case (int)MAIN_PROGRAM_ENUM.QUIT_PROGRAM:
                return false;

            case (int)MAIN_PROGRAM_ENUM.GET_VEHICLES:
                dealershipInterface.GetVehicles();
                break;

            case (int)MAIN_PROGRAM_ENUM.GET_VEHICLES_ID:
                dealershipInterface.GetVehicles_ID();
                break;

            case (int)MAIN_PROGRAM_ENUM.GET_VEHICLES_MAKE:
                dealershipInterface.GetVehicles_Make();
                break;

            case (int)MAIN_PROGRAM_ENUM.GET_VEHICLES_MINYEAR_MAXYEAR:
                dealershipInterface.GetVehicles_MinYear_MaxYear();
                break;

            case (int)MAIN_PROGRAM_ENUM.CREATE_VEHICLES:
                dealershipInterface.CreateVehicles();
                break;

            case (int)MAIN_PROGRAM_ENUM.UPDATE_VEHICLES:
                dealershipInterface.UpdateVehicles();
                break;

            case (int)MAIN_PROGRAM_ENUM.DELETE_VEHICLES_ID:
                dealershipInterface.DeleteVehicles_ID();
                break;

            case (int)MAIN_PROGRAM_ENUM.PRINT_MENU:
                PrintMenu();
                break;

            case (int)MAIN_PROGRAM_ENUM.CREATE_RANDOM_VEHICLE:
                dealershipInterface.CreateVehicle_Random();
                break;
        }
        return true;
    }
}