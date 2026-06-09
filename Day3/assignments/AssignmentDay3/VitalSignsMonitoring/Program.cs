using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       VITAL SIGNS MONITOR");
        Console.WriteLine("--------------------------------------------------");

        VitalSigns vitals = new VitalSigns();

        // Patient Name
        Console.Write("Enter Patient Name: ");
        vitals.PatientName = Console.ReadLine();

        // Temperature
        while (true)
        {
            Console.Write("Enter Temperature (C): ");
            try
            {
                vitals.Temperature = double.Parse(Console.ReadLine());
                break;
            }
            catch
            {
                Console.WriteLine("Error: Enter a valid numeric temperature.");
            }
        }

        // Oxygen Level
        while (true)
        {
            Console.Write("Enter Oxygen Level (%): ");
            try
            {
                vitals.Oxygen = int.Parse(Console.ReadLine());
                if (vitals.Oxygen >= 0 && vitals.Oxygen <= 100)
                    break;

                Console.WriteLine("Error: Oxygen must be between 0 and 100.");
            }
            catch
            {
                Console.WriteLine("Error: Enter a valid number.");
            }
        }

        // Pulse Rate
        while (true)
        {
            Console.Write("Enter Pulse Rate (BPM): ");
            try
            {
                vitals.Pulse = int.Parse(Console.ReadLine());
                if (vitals.Pulse > 0)
                    break;

                Console.WriteLine("Error: Pulse must be a positive number.");
            }
            catch
            {
                Console.WriteLine("Error: Enter a valid number.");
            }
        }

        Console.WriteLine("\n[Analyzing Data...]\n");

        // Determine Status
        string status = VitalAnalyzer.CheckStatus(
            vitals.Temperature,
            vitals.Oxygen,
            vitals.Pulse
        );

        // REPORT
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       MEDICAL ASSESSMENT REPORT");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Patient: {vitals.PatientName}\n");
        Console.WriteLine("Vitals Recorded:");
        Console.WriteLine($"- Temp:   {vitals.Temperature} C");
        Console.WriteLine($"- Oxygen: {vitals.Oxygen} %");
        Console.WriteLine($"- Pulse:  {vitals.Pulse} BPM\n");
        Console.WriteLine($"Status Assessment: {status}");

        if (status == "CRITICAL / EMERGENCY")
            Console.WriteLine("(Reason: One or more vitals in dangerous range)\nAction: Immediate medical attention required.");
        else if (status == "OBSERVATION NEEDED")
            Console.WriteLine("(Reason: Elevated vitals detected)\nAction: Nurse to monitor every hour.");
        else
            Console.WriteLine("Action: No immediate concern.");

        Console.WriteLine("--------------------------------------------------");
    }
}
