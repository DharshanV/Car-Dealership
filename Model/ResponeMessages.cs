using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ResponeMessages holds global values of all
 * messages that are responeded by the program,
 * either errors or valid inputs to the user.
 */
public class ResponeMessages
{
    public static string SUCCESSFULLY_DELETED_VEHICLE = "Successfully deleted vehicle: ";

	public static string UNABLE_TO_DELETE_VEHICLE = "Unable to delete vehicle information for id = ";

	public static string SUCCESSFULLY_UPDATED_VEHICLE = "Successfully updated vehicle:";

	public static string CREATED_VEHICLE_WITH_ID = "Successfully created vehicle with id: ";

	public static string YEAR_MUST_BE_BETWEEN_1950_AND_2050 = "Vehicle make year must be between 1950 and 2050";

	public static string VEHICLE_ID_MUST_BE_POSITIVE_NUMBER = "Invalid vehicle id, vehicle id must be positive number";

	public static string SOMETHING_WENT_WRONG = "Something went wrong ";

    public static string INVALID_INPUT_CAST = "Invalid input, cannot cast to integer";

    public static string INVALID_INPUT = "Invalid input: ";

    public static string INVALID_ID = "Invalid vehicle ID";

    public static string INVALID_VEHICLE = "Vehicle is invalid";

    public static string VEHICLE_ID_NOT_EXISTS = "Vehicle with ID #{0} does not exist";

    public static string VEHICLE_MAKE_NOT_EXISTS = "Vehicle make does not exist";

    public static string VEHICLE_ID_ALREADY_EXISTS = "Vehicle with ID #{0} already exists.";
}
