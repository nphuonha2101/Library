namespace Library.Utils.Securities.Jwt;

public class TokenInvalid : ITokenInvalid
{
    private readonly HashSet<string> _invalidTokens = new HashSet<string>();

    /**
     * Add a token to the invalid token list
     */
    public void Add(string token)
    {
        _invalidTokens.Add(token);
    }

    /**
     * Check if a token is invalid
     */
    public bool IsInvalid(string token)
    {
        return _invalidTokens.Contains(token);
    }
}