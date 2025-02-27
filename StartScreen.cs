public class StartScreen : Form
{
    private SportUnit[] allUnits;

    public StartScreen(SportUnit[] allUnits)
    {
        // fields assignment
        this.allUnits = allUnits;
        this.Text = "ExerciseApp";
        this.WindowState = FormWindowState.Maximized; // maximise application
        this.BackColor = Color.DarkSlateGray; // set color 
        this.FormBorderStyle = FormBorderStyle.None; // remove border

        // welcome label
        Label welcomeLabel = new Label();
        welcomeLabel.Text = "Welcome"; 
        welcomeLabel.Font = new Font("Arial", 48, FontStyle.Bold);
        welcomeLabel.ForeColor = Color.Black;
        welcomeLabel.AutoSize = true;
        welcomeLabel.Left = (ClientSize.Width - welcomeLabel.Width) / 2;
        welcomeLabel.Top = (ClientSize.Height - welcomeLabel.Height) / 2;
        this.Controls.Add(welcomeLabel);

        // info lable
        string workouts = "";
        int count = 0;
        foreach (SportUnit unit in allUnits){
            workouts += $"{count+1}. {unit.name}\n";
            count++;
        }
        Label infoLabel = new Label();
        infoLabel.Text = $"Get ready for your guided exercises. Each workout includes \nmultiple sets and breaks.The workouts in order are: \n{workouts}"; 
        infoLabel.Font = new Font("Arial", 30, FontStyle.Regular);
        infoLabel.ForeColor = Color.Gray;
        infoLabel.AutoSize = true;
        infoLabel.Left = (ClientSize.Width - infoLabel.Width) / 2;
        infoLabel.Top = (ClientSize.Height - infoLabel.Height) / 2 + 100;
        this.Controls.Add(infoLabel);

        // press space lable
        Label controlsInfo = new Label();
        controlsInfo.Text = "[press SPACE to start (and stop)]\n[press ESC to exit]"; 
        controlsInfo.Font = new Font("Arial", 30, FontStyle.Regular);
        controlsInfo.ForeColor = Color.Gray;
        controlsInfo.AutoSize = true;
        controlsInfo.Left = (ClientSize.Width - controlsInfo.Width) / 2;
        controlsInfo.Top = (ClientSize.Height - controlsInfo.Height) / 2 + 500;
        this.Controls.Add(controlsInfo);

        // start with space
        this.KeyDown += (s, e) => { 
            if (e.KeyCode == Keys.Space) {
                this.Hide();
                new ExerciseScreen(allUnits).ShowDialog();
                this.Close(); 
            }
        }; 


        // close with ESC
        this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); }; 

    }

}