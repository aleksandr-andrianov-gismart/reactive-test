public class ClientMessageProvider
{
    public static string GetMessageSecretCode(string key)
    {
        return key.GetHashCode().ToString();
    }
}
