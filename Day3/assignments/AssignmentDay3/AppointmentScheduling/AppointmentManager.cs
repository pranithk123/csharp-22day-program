using System;
using System.Collections.Generic;

public static class AppointmentManager
{
    private static string[] departments =
    {
        "GeneralMedicine",
        "Dental",
        "Orthopedics"
    };
    private static List<string> generalDoctors = new List<string>
    {
        "raju",
        "jiju"
    };
    private static List<string> dentalDoctors = new List<string>
    {
        "priya",
        "riya"
    };
    private static List<string> orthoDoctors = new List<string>
    {
        "suresh",
        "ramesh"
    };
    private static string[] timeSlots =
    {
        "10:00 AM",
        "11:00 AM",
        "12:00 PM"
    };
    public static Appointment BookAppointment()
    {
        Appointment appt = new Appointment();

        Console.Write("Enter Patient Name: ");
        appt.PatientName = Console.ReadLine();

        // Select Department
        int deptChoice = GetValidChoice("\nSelect Department:", departments);

        appt.Department = departments[deptChoice - 1];

        // Select Doctor
        List<string> selectedDoctors = deptChoice switch
        {
            1 => generalDoctors,
            2 => dentalDoctors,
            3 => orthoDoctors,
            _ => new List<string>()
        };

        int docChoice = GetValidChoice("\nSelect Doctor:", selectedDoctors.ToArray());
        appt.Doctor = selectedDoctors[docChoice - 1];

        // Select Time Slot
        int timeChoice = GetValidChoice("\nSelect Time Slot:", timeSlots);
        appt.TimeSlot = timeSlots[timeChoice - 1];

        return appt;
    }

    private static int GetValidChoice(string title, string[] options)
    {
        while (true)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{i + 1}. {options[i]}");

            Console.Write("Enter Choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice >= 1 && choice <= options.Length)
            {
                return choice;
            }

            Console.WriteLine("Invalid choice. Please try again.");
        }
    }

    public static void PrintTicket(Appointment appt)
    {
        Console.WriteLine("\n[Booking Confirmed]\n");

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("            APPOINTMENT TICKET");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Patient:    {appt.PatientName}");
        Console.WriteLine($"Department: {appt.Department}");
        Console.WriteLine($"Doctor:     {appt.Doctor}");
        Console.WriteLine($"Time:       {appt.TimeSlot}");
        Console.WriteLine("Status:     Confirmed");
        Console.WriteLine();
        Console.WriteLine("Please arrive 15 mins before your slot.");
        Console.WriteLine("--------------------------------------------------");
    }
}
