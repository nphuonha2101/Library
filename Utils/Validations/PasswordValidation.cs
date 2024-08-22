namespace Library.Utils.Validations;

public class PasswordValidation() : AbstractValidation(Pattern)
{
    private const string Pattern = "^(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,16}$";
}