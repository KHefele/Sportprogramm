
// class inherits from Windows.Forms 
// to show the instructions of the training units on the whole screen
public class ExerciseScreen : Form
{
    // fields
    private SportUnit[] allUnits;
    private bool isPause = false;
    private System.Windows.Forms.Timer timer;

    private System.Media.SoundPlayer successSound = new System.Media.SoundPlayer(@"resources/success.wav");

    private Label exerciseName;

    private Label secondsLeftLabel;

    int currentUnitInSeconds = 0;
    int currentSet = 0;

    int allUnitsIndex = 0;

    bool stopp = false;


    // constructor
    public ExerciseScreen(SportUnit[] allUnits)
    {
        // fields assignment
        this.allUnits = allUnits;
        this.Text = "ExerciseApp";
        this.WindowState = FormWindowState.Maximized; // maximise application
        this.BackColor = Color.DarkSlateGray; // set color CHANGE IN TIMER
        this.FormBorderStyle = FormBorderStyle.None; // remove border

        // name of exercise lable 
        exerciseName = new Label();
        exerciseName.Text = ""; // CHANGE IN TIMER
        exerciseName.Font = new Font("Arial", 48, FontStyle.Bold);
        exerciseName.ForeColor = Color.White;
        exerciseName.AutoSize = true;
        this.Controls.Add(exerciseName);

        // seconds left lable
        secondsLeftLabel = new Label();
        secondsLeftLabel.Text = ""; // CHANGE IN TIMER
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

        // stopp and start when space bar is used
        this.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Space)
            {

                if (stopp == false)
                {
                    timer.Stop();
                    stopp = true;
                }
                else if (stopp == true)
                {
                    timer.Start();
                    stopp = false;
                }

            }
        };


    }

    // // progress bar 
    // private void CopyWithProgress(string[] filenames)
    // {
    //     // Display the ProgressBar control.
    //     progressBar.Visible = true;
    //     // Set Minimum to 1 to represent the first file being copied.
    //     pBar1.Minimum = 1;
    //     // Set Maximum to the total number of files to copy.
    //     pBar1.Maximum = filenames.Length;
    //     // Set the initial value of the ProgressBar.
    //     pBar1.Value = 1;
    //     // Set the Step property to a value of 1 to represent each file being copied.
    //     pBar1.Step = 1;

    //     // Loop through all files to copy.
    //     for (int x = 1; x <= filenames.Length; x++)
    //     {
    //         // Copy the file and increment the ProgressBar if successful.
    //         if (CopyFile(filenames[x - 1]) == true)
    //         {
    //             // Perform the increment on the ProgressBar.
    //             pBar1.PerformStep();
    //         }
    //     }
    // }


    // timer_tick function is called every 1 second
    private void Timer_Tick(object sender, EventArgs e)
    {

        int currentSecondsLeft;

        // change name of unit (to Exercise name or "Pause"), change duration of unit, change background color
        if (isPause != true)
        {
            exerciseName.Text = $"{allUnits[allUnitsIndex].name}: Set {currentSet + 1}/{allUnits[allUnitsIndex].numberOfSets}";
            currentSecondsLeft = allUnits[allUnitsIndex].durationInSeconds - currentUnitInSeconds;
            BackColor = Color.LightCoral;
        }
        else
        {
            exerciseName.Text = "Pause";
            currentSecondsLeft = allUnits[allUnitsIndex].pauseBetweenSets - currentUnitInSeconds;
            BackColor = Color.LightGreen;
        }

        // count seconds
        currentUnitInSeconds++;

        // write seconds left to screen
        secondsLeftLabel.Text = currentSecondsLeft + "s left";

        // reset seconds to 0 if current unit ends (pause or excercise) & changes bool isPause
        if (currentSecondsLeft == 0)
        {
            currentUnitInSeconds = 0;
            if (isPause == false)
            {
                currentSet++;
                successSound.Play();

                // increase allUnitsIndex
                if (currentSet == allUnits[allUnitsIndex].numberOfSets)
                {
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