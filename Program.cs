﻿
// class runs main function
class Programm
{
    static void Main()
    {
        // insert all wanted sport exercises (string name, int durationInSeconds = 40, int numberOfSets = 3, int pauseBetweenSets = 20)
        SportUnit kniebeugen = new SportUnit("Squats",5,2,3);
        SportUnit liegestuetzen = new SportUnit("Push-ups");
        SportUnit ausfallschritte = new SportUnit("Lunges");
        SportUnit plank = new SportUnit("Plank");
        SportUnit superman = new SportUnit("Superman");

        SportUnit[] allUnits = new SportUnit[] { kniebeugen, liegestuetzen, ausfallschritte, plank, superman };

        // run fitness application
        Application.Run(new StartScreen(allUnits));
    }
}
