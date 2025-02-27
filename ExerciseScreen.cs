
// class inherits from Windows.Forms 
// to show the instructions of the training units on the whole screen
public class ExerciseScreen : Form
{
    // fields
    private SportUnit[] allUnits;
    private System.Windows.Forms.Timer timer;
    private System.Media.SoundPlayer successSound = new System.Media.SoundPlayer(@"resources/success.wav");

    private Label exerciseName;
    private Label secondsLeftLabel;
    private ProgressBar progressBar;
    private Label progressLabel;

    int currentUnitInSeconds = 0;
    int currentSet = 0;
    int allUnitsIndex = 0;
    private bool isPause = false;
    bool stopp = false;


    // constructor
    public ExerciseScreen(SportUnit[] allUnits)
    {
        // fields assignment
        this.allUnits = allUnits;
        this.Text = "ExerciseApp";
        this.WindowState = FormWindowState.Maximized; // maximise application
        this.BackColor = Color.DarkGray; // set color CHANGE IN TIMER
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

        InitializeProgressBar();
        

    }

    // progress bar 
    private void InitializeProgressBar()
    {
        // calculate duration of excercises
        int durationAllUnits = 0;
        foreach (SportUnit unit in allUnits){
            durationAllUnits += (unit.durationInSeconds + unit.pauseBetweenSets) * unit.numberOfSets;
        }

        // progress bar  
        progressBar = new ProgressBar();
        progressBar.Visible = true; // Display the ProgressBar control.
        progressBar.Minimum = 0; // Set Minimum to 1 to represent the first file being copied.
        progressBar.Maximum = durationAllUnits;  // Set Maximum to the total number of files to copy.
        progressBar.Value = 0; // Set the initial value of the ProgressBar.
        progressBar.Step = 1; // Set the Step property to a value of 1 to represent each file being copied.
        progressBar.Left = 0;
        progressBar.BackColor = Color.DarkGray;
        progressBar.ForeColor = Color.WhiteSmoke;
        progressBar.Style = ProgressBarStyle.Continuous;
        this.Controls.Add(progressBar);

        // progress bar label
        progressLabel = new Label();
        progressLabel.Font = new Font("Arial", 15, FontStyle.Italic);
        progressLabel.ForeColor = Color.Gray;
        this.Controls.Add(progressLabel);
    }


    // timer_tick function is called every 1 second
    private void Timer_Tick(object sender, EventArgs e)
    {

        int currentSecondsLeft;

        progressBar.PerformStep();
        progressLabel.Text = $"{Math.Round((double)progressBar.Value/progressBar.Maximum*100)}%";

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
        if (currentSecondsLeft == 1)
        {
            currentUnitInSeconds = 0;
            if (isPause == true)
            {
                currentSet++;

                // increase allUnitsIndex
                if (currentSet == allUnits[allUnitsIndex].numberOfSets)
                {
                    allUnitsIndex++;
                    currentSet = 0;

                    if (allUnitsIndex >= allUnits.Length){
                        timer.Stop();
                        this.Close();
                    }
                }
            } else {
                successSound.Play();
            }
            isPause = !isPause;
        }

        // recenter the text 
        exerciseName.Left = (ClientSize.Width - exerciseName.Width) / 2;
        exerciseName.Top = (ClientSize.Height - exerciseName.Height) / 2 - 50;
        secondsLeftLabel.Left = (ClientSize.Width - secondsLeftLabel.Width) / 2;
        secondsLeftLabel.Top = (ClientSize.Height - secondsLeftLabel.Height) / 2 + 50;
        progressBar.Width = ClientSize.Width;
        progressBar.Top = ClientSize.Height - progressBar.Height;
        progressLabel.Top = ClientSize.Height - progressBar.Height - progressLabel.Height;
        progressLabel.Width = ClientSize.Width;
    }


}