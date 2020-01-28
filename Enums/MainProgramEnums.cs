using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum MAIN_PROGRAM_ENUM {
    PARSE_FAIL = -2,QUIT_PROGRAM = -1,INVALID_INPUT = 0,
    GET_VEHICLES = 1, GET_VEHICLES_ID,GET_VEHICLES_MAKE,
    GET_VEHICLES_MINYEAR_MAXYEAR,CREATE_VEHICLES,
    UPDATE_VEHICLES, DELETE_VEHICLES_ID,CREATE_RANDOM_VEHICLE,PRINT_MENU
};