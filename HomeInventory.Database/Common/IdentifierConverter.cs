using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeInventory.Database.Common;

public class IdentifierConverter<TModel>() : ValueConverter<TModel, string>(
    v => v.Id.ToString(),
    v => Identifier<TModel>.Parse(v)
) where TModel : Identifier<TModel>;