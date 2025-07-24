using DotNetEnv;

public class ConfigService
{
    public static void LoadEnv()
    {
        Env.Load();
    }

    public static string EmailSender => Env.GetString("EMAIL_ADDRESS");
    public static string EmailPassword => Env.GetString("EMAIL_SECRET_PASS");
}
