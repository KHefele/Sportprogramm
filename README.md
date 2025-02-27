# Simple Fitness Coach Application  

A simple C# application that guides you through customizable fitness exercises in fullscreen mode.  

## 🚀 Features  
- Displays exercises with duration, sets, and breaks.  
- Fully customizable workout routines.  
- Fullscreen display for distraction-free training.  

## 🔧 Customizing Exercises  
You can define your workout exercises in the `Program.cs` file by modifying the `SportUnit` instances.  

### Example:  
```csharp
// Define exercises (string name, int durationInSeconds = 40, int numberOfSets = 3, int pauseBetweenSets = 20)
SportUnit squats = new SportUnit("Squats", 60, 4, 30);
```

## 🔑 Controls
Space bar – Start and stop workout.
ESC Key – Exit application.

## 📌 Future Improvements
- Aesthetic improvements
- Progress bar

## 📜 License
This project is free to use and modify for non-commercial purposes. It is licensed under the Creative Commons (CC BY-NC-SA) license.
For more details, see the [Creative Commons License](https://creativecommons.org/licenses/by-nc-sa/4.0/).

## 📬 Contact
For any inquiries or collaboration requests, feel free to reach out:
👤 Katharina Hefele
📧 [kathefele@gmail.com](kathefele@gmail.com)
