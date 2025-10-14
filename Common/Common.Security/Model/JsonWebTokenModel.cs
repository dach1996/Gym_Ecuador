namespace Common.Security.Model;
public class JsonWebTokenModel
{
    public string Token { get; protected set; }

    public long Expiration { get; protected set; }

    public JsonWebTokenModel(string token, long expiration)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Invalid token.");

        if (expiration <= 0)
            throw new ArgumentException("Invalid expiration.");

        Token = token;
        Expiration = expiration;
    }
}
