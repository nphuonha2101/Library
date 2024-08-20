namespace Library.Exceptions;

public class NoFileException : Exception
{
    public NoFileException() : base("No file uploaded.")
    {
    }
}