public static class FormatScore
{
    private static readonly string[] suffixes = new string[]
    {
        "",
        "K",   // Thousand (10^3)
        "M",   // Million (10^6)
        "B",   // Billion (10^9)
        "T",   // Trillion (10^12)
        "Qa",  // Quadrillion (10^15)
        "Qi",  // Quintillion (10^18)
        "Sx",  // Sextillion (10^21)
        "Sp",  // Septillion (10^24)
        "Oc",  // Octillion (10^27)
        "No",  // Nonillion (10^30)
        "Dc",  // Decillion (10^33)
        "Ud",  // Undecillion (10^36)
        "Dd",  // Duodecillion (10^39)
        "Td",  // Tredecillion (10^42)
        "Qad", // Quattuordecillion (10^45)
        "Qid", // Quindecillion (10^48)
        "Sxd", // Sexdecillion (10^51)
        "Spd", // Septendecillion (10^54)
        "Ocd", // Octodecillion (10^57)
        "Nod", // Novemdecillion (10^60)
        "Vg"   // Vigintillion (10^63)
    };

    public static string Format(double score) => FormatScoreValue(score);

    private static string FormatScoreValue(double rawScore)
    {
        if (rawScore < 0)
            return "0";

        int suffixIndex = 0;
        while (rawScore >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            rawScore /= 1000;
            suffixIndex++;
        }

        if (suffixIndex == suffixes.Length - 1 && rawScore >= 1000)
            return $"999.99{suffixes[suffixIndex]}";

        string formattedOutput = $"{rawScore.ToString("0.##")}{suffixes[suffixIndex]}";
        return formattedOutput;
    }
}
