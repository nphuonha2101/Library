namespace Library.Utils.Validations;

public class PasswordValidation() : AbstractValidation(Pattern)
{
    private const string Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$";
}