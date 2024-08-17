namespace Library.Exceptions;

public class InvalidFileException: Exception
{
    public InvalidFileException() : base("Invalid file format. Only .jpg, .jpeg, .webp and .png files are allowed.")
    {
    }
}