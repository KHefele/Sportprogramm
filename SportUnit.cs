// class for storing informations about the training unit 
// e.g. "Kniebeugen" with 40s duration, 3 sets and a pause of 20s between the sets
public class SportUnit
{
    // Fields
    public string name;
    public int durationInSeconds;
    public int numberOfSets;
    public int pauseBetweenSets;

    // Constructor
    public SportUnit(string name, int durationInSeconds = 40, int numberOfSets = 3, int pauseBetweenSets = 20)
    {
        this.name = name;
        this.durationInSeconds = durationInSeconds;
        this.numberOfSets = numberOfSets;
        this.pauseBetweenSets = pauseBetweenSets;
    }
}