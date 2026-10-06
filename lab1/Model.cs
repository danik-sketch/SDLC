using System.Security.Cryptography;

namespace PasswordGeneratorApp;

public class PasswordSettings
{
    public int Length { get; set; } = 12;
    public int Count { get; set; } = 1;
    public bool IncludeUppercase { get; set; } = true;
    public bool IncludeLowercase { get; set; } = true;
    public bool IncludeDigits { get; set; } = true;
    public bool IncludeSpecial { get; set; } = true;
}

public class Model
{
    private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string DigitChars = "0123456789";
    private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
    public PasswordSettings CurrentSettings { get; private set; } = new();
    public List<string> GeneratedPasswords { get; } = new();

    public event Action DataChanged;
    public event Action<string> ValidationError;

    public bool ValidateSettings(PasswordSettings settings)
    {
        if (settings.Length <= 0)
        {
            ValidationError?.Invoke("Длина пароля должна быть больше 0.");
            return false;
        }

        if (settings.Count <= 0)
        {
            ValidationError?.Invoke("Количество паролей должно быть больше 0.");
            return false;
        }

        if (!settings.IncludeUppercase && !settings.IncludeLowercase &&
            !settings.IncludeDigits && !settings.IncludeSpecial)
        {
            ValidationError?.Invoke("Выберите хотя бы один набор символов.");
            return false;
        }

        return true;
    }

    public bool SaveSettings(PasswordSettings settings)
    {
        if (!ValidateSettings(settings))
            return false;

        CurrentSettings = settings;
        DataChanged?.Invoke();
        return true;
    }

    public void Generate()
    {
        if (!ValidateSettings(CurrentSettings))
            return;

        GeneratedPasswords.Clear();

        var validChars = "";
        if (CurrentSettings.IncludeLowercase) validChars += LowercaseChars;
        if (CurrentSettings.IncludeUppercase) validChars += UppercaseChars;
        if (CurrentSettings.IncludeDigits) validChars += DigitChars;
        if (CurrentSettings.IncludeSpecial) validChars += SpecialChars;

        using (var rng = RandomNumberGenerator.Create())
        {
            for (var i = 0; i < CurrentSettings.Count; i++)
            {
                var password = new char[CurrentSettings.Length];
                var randomBytes = new byte[CurrentSettings.Length];
                rng.GetBytes(randomBytes);

                for (var j = 0; j < CurrentSettings.Length; j++)
                    password[j] = validChars[randomBytes[j] % validChars.Length];

                GeneratedPasswords.Add(new string(password));
            }
        }

        DataChanged?.Invoke();
    }
}