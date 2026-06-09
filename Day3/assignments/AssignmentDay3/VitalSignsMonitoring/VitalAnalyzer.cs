using System;

public static class VitalAnalyzer
{
    public static string CheckStatus(double temp, int oxygen, int pulse)
    {
        // Critical Conditions
        if (temp > 39.0 || oxygen < 90 || pulse < 50 || pulse > 120)
            return "CRITICAL / EMERGENCY";

        // Observation Needed
        if (temp > 37.5 || oxygen < 95 || pulse > 100)
            return "OBSERVATION NEEDED";

        // Normal
        return "NORMAL";
    }
}

