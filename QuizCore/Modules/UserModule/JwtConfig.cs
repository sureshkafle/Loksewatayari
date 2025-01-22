namespace QuizCore.Modules.UserModule.Identities;
public class JwtConfig
{
    public JwtConfig(string key, string issuer , int expireInHours)
    {
        Key = key;
        Issuer = issuer;
        ExpireInHours =expireInHours;
    }
    public string Key { get; init; }
    public string Issuer { get; init; }
    public int ExpireInHours {get;init;} =12;

}
