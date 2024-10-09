class Program
{
    static void Main(string[] args)
    {
        ParkingGarage garage = new ParkingGarage();

        // Testa parkering
        garage.ParkVehicle("CAR", "ABäC123");
        garage.ParkVehicle("MC", "MCå001");
        garage.ParkVehicle("MC", "MCü002");
    }
}

public class ParkingGarage
{
    private string[] parkingLot;

    
    public ParkingGarage()
    {
        parkingLot = new string[100]; // Initierar parkeringsplatserna
    }

    // Metod för att parkera fordon
    public void ParkVehicle(string vehicleType, string registrationNumber)
    {
        int spot = FindAvailableSpot(vehicleType);
        if (spot == -1)
        {
            Console.WriteLine("Ingen ledig plats för fordonet.");
            return;
        }

        string vehicle = $"{vehicleType}#{registrationNumber}";

        if (vehicleType == "CAR")
        {
            parkingLot[spot] = vehicle;
            Console.WriteLine($"Bil {registrationNumber} har parkerats på plats {spot + 1}.");
        }
        else if (vehicleType == "MC")
        {
            // Här kommer vi att parkera motorcykeln
            if (string.IsNullOrEmpty(parkingLot[spot]))
            {
                parkingLot[spot] = vehicle; // Parkera motorcykel på tom plats
            }
            else
            {
                // Om det redan finns en motorcykel, lägg till den nya
                parkingLot[spot] += $"|{vehicle}"; // Lägg till motorcykel på samma plats
            }
            Console.WriteLine($"MC {registrationNumber} har parkerats på plats {spot + 1}.");
        }
    }

    // Metod för att hitta en ledig plats
    private int FindAvailableSpot(string vehicleType)
    {
        // Om MC, först leta efter en plats som redan har en motorcykel
        if (vehicleType == "MC")
        {
            for (int i = 0; i < parkingLot.Length; i++)
            {
                if (parkingLot[i]?.StartsWith("MC") == true) // Plats har redan en motorcykel
                {
                    string[] parkedBikes = parkingLot[i].Split('|');
                    if (parkedBikes.Length < 2) // Det finns plats för en till motorcykel
                    {
                        return i; // Returnera den platsen
                    }
                }
            }
        }

        // Om ingen plats hittades, leta efter en tom plats
        for (int i = 0; i < parkingLot.Length; i++)
        {
            if (vehicleType == "CAR" && (parkingLot[i] == null || parkingLot[i] == ""))
            {
                return i; // Ledig plats för bil
            }
            else if (vehicleType == "MC" && (parkingLot[i] == null || parkingLot[i] == ""))
            {
                return i; // Ledig plats för en MC
            }
        }

        return -1; // Ingen ledig plats
    }
}
