namespace Library.Utils.Securities.Jwt;

public interface ITokenInvalid
{
    void Add(string token);
    bool IsInvalid(string token);
}