using System.Text.RegularExpressions;

namespace Library.Utils.Validations;

public class EmailValidation() : AbstractValidation(Pattern)
{
    private const string Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
}