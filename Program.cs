using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;


// class for storing informations about the training unit 
// e.g. "Kniebeugen" with 40s duration, 3 sets and a pause of 20s between the sets
class SportUnit
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





// class inherits from Windows.Forms 
// to show the instructions of the training units on the whole screen
class ExerciseApp : Form
{
    // fields
    private SportUnit[] allUnits;
    private bool isPause = false; 
    private System.Windows.Forms.Timer timer;

    private System.Media.SoundPlayer successSound = new System.Media.SoundPlayer(@"C:\Users\katha\OneDrive\3_Dokumente\GitHub\Sportprogramm\resources\b2af-a026-445c-8d4c-eafabed3f76c.wav"); 

    private Label exerciseName;

    private Label secondsLeftLabel;

    int currentUnitInSeconds = 0; 
    int currentSet = 0;

    int allUnitsIndex = 0;
    



    // constructor
    public ExerciseApp(SportUnit[] allUnits)
    {
        // fields assignment
        this.allUnits = allUnits;
        this.Text = "Vollbild";
        this.WindowState = FormWindowState.Maximized; // maximise application
        this.BackColor = Color.White; // set color CHANGE IN TIMER
        this.FormBorderStyle = FormBorderStyle.None; // remove border



        // name of exercise lable 
        exerciseName = new Label();
        exerciseName.Text = "Welcome"; // CHANGE IN TIMER
        exerciseName.Font = new Font("Arial", 48, FontStyle.Bold);
        exerciseName.ForeColor = Color.White;
        exerciseName.AutoSize = true;
        this.Controls.Add(exerciseName);

        // seconds left lable
        secondsLeftLabel = new Label();
        secondsLeftLabel.Text = allUnits[allUnitsIndex].durationInSeconds + "s left"; // CHANGE IN TIMER
        secondsLeftLabel.Font = new Font("Arial", 30, FontStyle.Regular);
        secondsLeftLabel.ForeColor = Color.Gray;
        secondsLeftLabel.AutoSize = true;
        this.Controls.Add(secondsLeftLabel);

        // close with ESC
        this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); }; 

        // timer
        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1000; // 1s
        timer.Tick += Timer_Tick;
        timer.Start();
    }


    // timer_tick function is called every 1 second
    private void Timer_Tick(object sender, EventArgs e)
    {

        int currentSecondsLeft;

        // change name of unit (to Exercise name or "Pause"), change duration of unit, change background color
        if (isPause != true){
            exerciseName.Text = $"{allUnits[allUnitsIndex].name}: Set {currentSet + 1}/{allUnits[allUnitsIndex].numberOfSets}";
            currentSecondsLeft = allUnits[allUnitsIndex].durationInSeconds - currentUnitInSeconds;
            BackColor =  Color.LightCoral;
        } else {
            exerciseName.Text = "Pause";
            currentSecondsLeft = allUnits[allUnitsIndex].pauseBetweenSets - currentUnitInSeconds;
            BackColor =  Color.LightGreen;
        }

        // count seconds
        currentUnitInSeconds++;

        // write seconds left to screen
        secondsLeftLabel.Text = currentSecondsLeft + "s left";

        // reset seconds to 0 if current unit ends (pause or excercise) & changes bool isPause
        if (currentSecondsLeft == 0)
        {
            currentUnitInSeconds = 0;
            if (isPause == false){
                currentSet++;
                successSound.Play();

                // increase allUnitsIndex
            	if (currentSet == allUnits[allUnitsIndex].numberOfSets){
                    allUnitsIndex++;
                    currentSet = 0;
                }
            }
            isPause = !isPause;
        }

        // recenter the text 
        exerciseName.Left = (ClientSize.Width - exerciseName.Width) / 2;
        exerciseName.Top = (ClientSize.Height - exerciseName.Height) / 2;
        secondsLeftLabel.Left = (ClientSize.Width - secondsLeftLabel.Width) / 2;
        secondsLeftLabel.Top = (ClientSize.Height - secondsLeftLabel.Height) / 2 + 100;
    }


}

// class runs main function
class Programm
{
    static void Main()
    {
        // insert all wanted sportUnits 
        SportUnit kniebeugen = new SportUnit("Kniebeugen",3,3,5);
        SportUnit liegestuetzen = new SportUnit("Liegestützen", 4, 2, 3);
        SportUnit ausfallschritte = new SportUnit("Ausfallschritte");
        SportUnit plank = new SportUnit("Plank");
        SportUnit superman = new SportUnit("Superman");

        SportUnit[] allUnits = new SportUnit[] { kniebeugen, liegestuetzen, ausfallschritte, plank, superman };

        // run fitness application
        Application.Run(new ExerciseApp(allUnits));
    }
}
