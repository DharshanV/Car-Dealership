using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class DealershipTests
    {
        RandomVehicles randomVehicles = new RandomVehicles();

        [Test]
        public void GetVehicles_AllVehicles()
        {
            Dealership dealership = new Dealership();
            int vehiclesCount = 10;
            Vehicle[] allVehicles = new Vehicle[vehiclesCount];

            for (int i = 0; i < vehiclesCount; i++)
            {
                Vehicle vehicle = randomVehicles.next();
                //Asserts when creation of vehicle was not done either invalid ID.
                Assert.IsTrue(dealership.CreateVechicle(vehicle));
                allVehicles[i] = vehicle;
            }

            LinkedList<Vehicle> dealerVehicles = dealership.GetVehicles();
            Assert.NotNull(dealerVehicles);

            for (int i = 0; i < vehiclesCount; i++)
            {
                Assert.IsTrue(dealerVehicles.Contains(allVehicles[i]));
            }
            Assert.Pass();
        }

        [Test]
        public void GetVehicles_AllVehicles_with_ID()
        {
            Dealership dealership = new Dealership();
            int vehiclesCount = 10;
            Vehicle[] allVehicles = new Vehicle[vehiclesCount];

            for (int i = 0; i < vehiclesCount; i++)
            {
                Vehicle vehicle = randomVehicles.next();
                Assert.IsTrue(dealership.CreateVechicle(vehicle));
                allVehicles[i] = vehicle;
            }

            for(int i = 0; i < vehiclesCount; i++)
            {
                Vehicle vehicle = dealership.GetVehicles(allVehicles[i].Id);
                Assert.NotNull(vehicle);
                Assert.AreEqual(allVehicles[i], vehicle);
            }
            Assert.Pass();
        }


        [Test]
        public void GetVehicles_AllVehicles_with_Make()
        {
            Dealership dealership = new Dealership();
            int vehiclesCount = 10;
            Vehicle[] allVehicles = new Vehicle[vehiclesCount];

            for (int i = 0; i < vehiclesCount; i++)
            {
                Vehicle vehicle = randomVehicles.next();
                Assert.IsTrue(dealership.CreateVechicle(vehicle));
                allVehicles[i] = vehicle;
            }

            for(int i = 0; i < RandomVehicles.makes.Length; i++)
            {
                LinkedList<Vehicle> makeVehicles = dealership.GetVehicles(RandomVehicles.makes[i]);
                if (makeVehicles == null) continue;
                //List of vehicles got by make, atleast should exist in allVehicles
                //that was inserted
                foreach(Vehicle vehicle in makeVehicles)
                {
                    bool containsVehicle = false;
                    for(int j = 0; j < vehiclesCount; j++)
                    {
                        if (vehicle.Equals(allVehicles[j]))
                        {
                            containsVehicle = true;
                            break;
                        }
                    }
                    Assert.IsTrue(containsVehicle);
                }
            }
            Assert.Pass();
        }

        [Test]
        public void GetVehicles_AllVehicles_with_MinAndMaxYear()
        {
            Dealership dealership = new Dealership();
            int vehiclesCount = 10;
            Vehicle[] allVehicles = new Vehicle[vehiclesCount];

            for (int i = 0; i < vehiclesCount; i++)
            {
                Vehicle vehicle = randomVehicles.next();
                Assert.IsTrue(dealership.CreateVechicle(vehicle));
                allVehicles[i] = vehicle;
            }

            //Get vehicles with 5 year window between min and max year
            for(int i = DealershipConfig.MIN_YEAR; i <= DealershipConfig.MAX_YEAR; i+=5)
            {
                int minYear = i;
                int maxYear = i + 5;
                LinkedList<Vehicle> vehicles = dealership.GetVehicles(minYear, maxYear);
                if (vehicles == null) continue;
                foreach (Vehicle vehicle in vehicles)
                {
                    //Check if the get vehicles exists in allVehicles
                    bool containsVehicle = false;
                    for (int j = 0; j < vehiclesCount; j++)
                    {
                        if (vehicle.Equals(allVehicles[j]))
                        {
                            containsVehicle = true;
                            break;
                        }
                    }
                    Assert.IsTrue(containsVehicle);

                    //Check if the current vehicle is truely inbetween
                    //the get range
                    if(vehicle.Year < i && vehicle.Year > i + 5)
                    {
                        Assert.Fail();
                    }
                }
            }
            Assert.Pass();
        }

        //Update each vehicle by changing their year to year + 1.
        //Get the same vehicle back by ID, and see if the year
        //has been updated
        [Test]
        public void UpdateVehicles()
        {
            Dealership dealership = new Dealership();
            int vehiclesCount = 10;
            Vehicle[] allVehicles = new Vehicle[vehiclesCount];

            for (int i = 0; i < vehiclesCount; i++)
            {
                Vehicle vehicle = randomVehicles.next();
                Assert.IsTrue(dealership.CreateVechicle(vehicle));
                allVehicles[i] = vehicle;
            }

            for (int i = 0; i < vehiclesCount; i++)
            {
                int ID = allVehicles[i].Id;
                int oldYear = allVehicles[i].Year;
                string make = allVehicles[i].Make;
                string model = allVehicles[i].Model;

                Assert.IsTrue(dealership.UpdateVehicle(ID, new Vehicle(ID, oldYear + 1, make, model)));
                Vehicle newVehicle = dealership.GetVehicles(ID);

                Assert.IsNotNull(newVehicle);
                Assert.AreEqual(oldYear+1, newVehicle.Year);
                Assert.AreEqual(ID, newVehicle.Id);
                Assert.AreEqual(make, newVehicle.Make);
                Assert.AreEqual(model, newVehicle.Model);
            }
            Assert.Pass();
        }

        //Delete all vehicles with their ID.
        //Then try to Get vehicle with the same ID. They the function
        //should return a null vehicle. 
        //Also check if it is delete in Make and Year dictonary.
        [Test]
        public void DeleteVehicle_With_ID()
        {
            Dealership dealership = new Dealership();
            int vehiclesCount = 10;
            Vehicle[] allVehicles = new Vehicle[vehiclesCount];

            for (int i = 0; i < vehiclesCount; i++)
            {
                Vehicle vehicle = randomVehicles.next();
                Assert.IsTrue(dealership.CreateVechicle(vehicle));
                allVehicles[i] = vehicle;
            }

            for(int i = 0; i < vehiclesCount; i++)
            {
                Assert.IsTrue(dealership.DeleteVehicles(allVehicles[i].Id));
                if(dealership.GetVehicles(allVehicles[i].Id) != null)
                {
                    Assert.Fail();
                }

                LinkedList<Vehicle> makeVehicles = dealership.GetVehicles(allVehicles[i].Make);
                foreach(Vehicle vehicle in makeVehicles)
                {
                    Assert.AreNotEqual(vehicle.Id, allVehicles[i].Id);
                }

                LinkedList<Vehicle> yearVehicles = dealership.GetVehicles(allVehicles[i].Year,allVehicles[i].Year);
                foreach (Vehicle vehicle in yearVehicles)
                {
                    Assert.AreNotEqual(vehicle.Id, allVehicles[i].Id);
                }
            }
            Assert.Pass();
        }

        //Insert 1,000,000 vehicle. Keep count of number of Fords and
        //years between 2015-2020. Then retrive and check if the counts
        //are the same
        [Test]
        public void StressTest()
        {
            Dealership dealership = new Dealership();
            int vehicleCount = 1000000;
            int fordCount = 0;
            int yearRangeCount = 0;

            for(int i = 0; i < vehicleCount; i++)
            {
                int ID = i;
                int year = RandomVehicles.RandomYears();
                string make = RandomVehicles.RandomMakes();
                string model = RandomVehicles.RandomModels();
                Vehicle vehicle = new Vehicle(ID, year, make, model);
                dealership.CreateVechicle(vehicle);
                if (make == "Ford") fordCount++;
                if (year >= 2015 && year <= 2020) yearRangeCount++;
            }

            LinkedList<Vehicle> makeVehicles = dealership.GetVehicles("Ford");
            Assert.AreEqual(fordCount, makeVehicles.Count);

            LinkedList<Vehicle> yearVehicles = dealership.GetVehicles(2015,2020);
            Assert.AreEqual(yearRangeCount, yearVehicles.Count);

            Assert.Pass();
        }
    }
}