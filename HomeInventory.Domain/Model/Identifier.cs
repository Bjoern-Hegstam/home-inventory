namespace HomeInventory.Domain.Model;

public abstract record Identifier<T>(Guid Id) where T : Identifier<T>
{
    public static T Parse(string id) => CreateInstance(Guid.Parse(id));

    private static T CreateInstance(Guid id)
    {
        return (T)Activator.CreateInstance(typeof(T), id)!;
    }
}