using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Timers;


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
class VollbildText : Form
{
    // fields
    private System.Windows.Forms.Timer aTimer;

    // constructor
    public VollbildText(string text, int seconds, string color)
    {
        // fields assignment
        this.Text = "Vollbild";
        this.WindowState = FormWindowState.Maximized; // maximise application
        this.BackColor = (color == "red") ? Color.DarkRed : Color.DarkGreen; // set color
        this.FormBorderStyle = FormBorderStyle.None; // remove border

        // text lable 
        Label label = new Label();
        label.Text = text;
        label.Font = new Font("Arial", 48, FontStyle.Bold);
        label.ForeColor = Color.White;
        label.AutoSize = true; 
        label.Left = (this.ClientSize.Width - label.Width) / 2;
        label.Top = (this.ClientSize.Height - label.Height) / 2;
        this.Controls.Add(label);

        this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); }; // close with ESC
    }
}

// class runs main function
class Programm {
        static void Main()
    {
        Application.Run(new VollbildText("Kniebeugen", 20, "red"));
    }
}
