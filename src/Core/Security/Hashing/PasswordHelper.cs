namespace Security.Hashing;

public static class PasswordHelper
{
    public static string GeneratePassword()
    {
        const string lowerCase = "abcdefghijklmnopqursuvwxyz";
        const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "123456789";
        const string specials = "!@$%&*()#";

        char[] _password = new char[8];
        string charSet = string.Empty;
        Random _random = new();
        int counter;

        charSet += lowerCase;
        charSet += upperCase;
        charSet += numbers;
        charSet += specials;

        for (counter = 0; counter < 8; counter++)
            _password[counter] = charSet[_random.Next(charSet.Length - 1)];

        return String.Join(null, _password);
    }
}