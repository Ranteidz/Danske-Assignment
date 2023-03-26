namespace Company.API.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string s) : base(s)
    {
    }
}