namespace Library.Utils.Validations;

public class UsernameValidation() : AbstractValidation(Pattern)
{
    private const string Pattern = @"^[a-zA-Z0-9]{5,}$";
}