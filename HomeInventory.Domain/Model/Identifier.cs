using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeInventory.Domain.Model;

public abstract record Identifier<T>(Guid Id) where T : Identifier<T>
{
    public static T New() => CreateInstance(Guid.NewGuid());

    public static T Parse(string id) => CreateInstance(Guid.Parse(id));

    private static T CreateInstance(Guid id)
    {
        return (T)Activator.CreateInstance(typeof(T), id)!;
    }
}

public class IdentifierConverter<TModel>() : ValueConverter<TModel, string>(
    v => v.Id.ToString(),
    v => Identifier<TModel>.Parse(v)
) where TModel : Identifier<TModel>;