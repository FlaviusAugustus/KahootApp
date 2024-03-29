namespace KahootFrontend.Settings;

public class JWT
{
    public static string JWTConfig = "JWTConfig";
    
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinutes { get; set; } 
}
