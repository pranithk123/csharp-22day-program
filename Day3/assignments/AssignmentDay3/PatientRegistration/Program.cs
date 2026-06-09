class Program
{
    static void Main(string[] args)
    {
        Patient patient = RegistrationManager.RegisterPatient();
        RegistrationManager.PrintRegistrationSlip(patient);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}


