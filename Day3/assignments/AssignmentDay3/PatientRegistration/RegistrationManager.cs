public static class RegistrationManager
{
    public static Patient RegisterPatient()
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       HOSPITAL PATIENT REGISTRATION SYSTEM");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine();

        Patient patient = new Patient();

        // Name
        while (true)
        {
            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                patient.Name = name.Trim();
                break;
            }
            Console.WriteLine("Error: Name cannot be empty.");
        }

        // Age
        while (true)
        {
            Console.Write("Enter Age: ");
            string ageInput = Console.ReadLine();
            try
            {
                int age = int.Parse(ageInput);
                if (age > 0 && age < 120)
                {
                    patient.Age = age;
                    break;
                }
                Console.WriteLine("Error: Age must be between 1 and 119.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid numeric age.");
            }
        }

        // Gender
        Console.Write("Enter Gender (Male/Female/Other): ");
        patient.Gender = Console.ReadLine();

        // Phone Number
        while (true)
        {
            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(phone) &&
                phone.Length == 10 &&
                long.TryParse(phone, out _))
            {
                patient.PhoneNumber = phone;
                break;
            }
            Console.WriteLine("Error: Phone number must be exactly 10 digits.");
        }

        // City
        Console.Write("Enter City: ");
        patient.City = Console.ReadLine();

       
        patient.PatientID = "PAT-" + DateTime.Now.Year + "-001";

        Console.WriteLine();
        Console.WriteLine("[Registration Complete]");
        Console.WriteLine();

        return patient;
    }

    public static void PrintRegistrationSlip(Patient patient)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("            PATIENT REGISTRATION SLIP");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Date: {DateTime.Now:dd-MMM-yyyy}");
        Console.WriteLine();
        Console.WriteLine($"Patient ID: {patient.PatientID}");
        Console.WriteLine($"Name:       {patient.Name}");
        Console.WriteLine($"Age:        {patient.Age} years");
        Console.WriteLine($"Gender:     {patient.Gender}");
        Console.WriteLine($"Contact:    {patient.PhoneNumber}");
        Console.WriteLine($"Location:   {patient.City}");
        Console.WriteLine();
        Console.WriteLine("Instructions:");
        Console.WriteLine("Please proceed to the waiting area.");
        Console.WriteLine("--------------------------------------------------");
    }
}
