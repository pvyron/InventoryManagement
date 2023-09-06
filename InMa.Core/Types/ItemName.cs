namespace InMa.Core.Types;

public class ItemName : ValueOf<string, ItemName>
{
    internal const ushort MinNameLength = 5;
    internal const ushort MaxNameLength = 100;
    
    protected override void Validate()
    {
        if (Value.Length is < MinNameLength or > MaxNameLength)
        {
            throw new ItemNameLengthException(Value.Length);
        }
    }
}

public class ItemNameLengthException : Exception
{
    public ItemNameLengthException(int length) : base($"Length of {length} is invalid for ItemName, minimum is {ItemName.MinNameLength} and maximum is {ItemName.MaxNameLength}")
    {
        
    }
}