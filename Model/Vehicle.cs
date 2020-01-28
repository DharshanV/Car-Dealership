
/*
 * Vehicle describes the data which dealership
 * relies and manages on. 
 * 
 * It describes the vehicle ID, Year, Make, and Model.
 */
public class Vehicle
{
    public Vehicle() { }
    public Vehicle(int Id, int Year, string Make, string Model)
    {
        this.Id = Id;
        this.Year = Year;
        this.Make = Make;
        this.Model = Model;
    }

    public int Id { get; set; }
    public int Year { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }

    public override string ToString()
    {
        return string.Format("Vehicle ID: {0} | Year: {1} | Make: {2} | Model: {3}", Id, Year, Make, Model);
    }

    /*
     * Make sure that the compare of two vehicle objects
     * are valid.
     */ 
    public override bool Equals(object obj)
    {
        var vehicle = obj as Vehicle;
        if (vehicle == null) return false;
        if (vehicle.Id == Id && vehicle.Year == Year &&
           vehicle.Make == Make && vehicle.Model == Model) return true;
        else return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}