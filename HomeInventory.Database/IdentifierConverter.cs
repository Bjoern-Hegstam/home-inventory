using HomeInventory.Model;
using HomeInventory.Model.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeInventory.Database;

public class IdentifierConverter<TModel>() : ValueConverter<TModel, string>(
    v => v.Id.ToString(),
    v => Identifier<TModel>.Parse(v)
) where TModel : Identifier<TModel>;