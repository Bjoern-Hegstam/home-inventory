using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using HomeInventory.Model.Common;

namespace HomeInventory.Model;

public class IdentifierJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.BaseType is { IsGenericType: true } &&
               typeToConvert.BaseType.GetGenericTypeDefinition() == typeof(Identifier<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return (JsonConverter)Activator.CreateInstance(
            typeof(IdentifierJsonConverter<>).MakeGenericType(typeToConvert),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: null,
            culture: null)!;
    }
}

public class IdentifierJsonConverter<T> : JsonConverter<T> where T : Identifier<T>
{
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id.ToString());
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Support for reading id as serialized by this converter
        if (reader.TokenType == JsonTokenType.String)
        {
            string idString = reader.GetString()!;
            return Identifier<T>.Parse(idString);
        }

        // Support id serialized by default serializer, i.e. as an object with an Id property
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read();
            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == nameof(Identifier<T>.Id))
            {
                reader.Read();
                string idString = reader.GetString()!;
                reader.Read(); // Move past the end of the object
                return Identifier<T>.Parse(idString);
            }
        }

        throw new JsonException($"Invalid JSON format for {typeToConvert.Name}.");
    }
}