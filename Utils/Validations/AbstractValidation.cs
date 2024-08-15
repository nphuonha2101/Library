using System.Text.RegularExpressions;

namespace Library.Utils.Validations;

public abstract class AbstractValidation(string pattern)
{
    private readonly Regex _regex = new(pattern);

    public bool IsValid(string value)
    {
        return _regex.IsMatch(value);
    }
}