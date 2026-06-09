using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       APPOINTMENT BOOKING SYSTEM");
        Console.WriteLine("--------------------------------------------------");

        Appointment appt = AppointmentManager.BookAppointment();
        AppointmentManager.PrintTicket(appt);

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
