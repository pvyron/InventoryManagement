namespace InMa.Core.Types;

public class StorageUnitName : ValueOf<string, StorageUnitName>
{
    internal const ushort MinNameLength = 5;
    internal const ushort MaxNameLength = 50;
    
    protected override void Validate()
    {
        if (Value.Length is < MinNameLength or > MaxNameLength)
        {
            throw new StorageUnitNameNameLengthException(Value.Length);
        }
    }
}

public class StorageUnitNameNameLengthException : Exception
{
    public StorageUnitNameNameLengthException(int length) : base(
        $"Length of {length} is invalid for ItemName, minimum is {StorageUnitName.MinNameLength} and maximum is {StorageUnitName.MaxNameLength}")
    {
        
    }
}