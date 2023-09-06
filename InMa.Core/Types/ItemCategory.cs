namespace InMa.Core.Types;

public sealed class ItemCategory : ValueOf<string, ItemCategory>
{
    internal const ushort MinNameLength = 1;
    internal const ushort MaxNameLength = 20;
    
    protected override void Validate()
    {
        if (Value.Length is < MinNameLength or > MaxNameLength)
        {
            throw new ItemCategoryLengthException(Value.Length);
        }
    }
}

public class ItemCategoryLengthException : Exception
{
    public ItemCategoryLengthException(int length) : base($"Length of {length} is invalid for ItemCategoryLengthException, minimum is {ItemCategory.MinNameLength} and maximum is {ItemCategory.MaxNameLength}")
    {
        
    }
}