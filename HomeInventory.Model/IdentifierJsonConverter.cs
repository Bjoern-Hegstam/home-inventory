using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using HomeInventory.Model.Common;

namespace HomeInventory.Model;

public class IdentifierJsonConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.BaseType is { IsGenericType: true } &&
               typeToConvert.BaseType.GetGenericTypeDefinition() == typeof(Identifier<>);
    }

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Support for reading id as serialized by this converter
        if (reader.TokenType == JsonTokenType.String)
        {
            string idString = reader.GetString()!;
            return ParseIdentifier(idString, typeToConvert);
        }

        // Support id serialized by default serializer, i.e. as an object with an Id property
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read();
            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "Id")
            {
                reader.Read();
                string idString = reader.GetString()!;
                reader.Read(); // Move past the end of the object
                return ParseIdentifier(idString, typeToConvert);
            }
        }

        throw new JsonException($"Invalid JSON format for {typeToConvert.Name}.");
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        // Serialize as a flat id string
        PropertyInfo idProperty = value.GetType().GetProperty("Id")!;
        writer.WriteStringValue(idProperty.GetValue(value)!.ToString());
    }

    private static object ParseIdentifier(string idString, Type typeToConvert)
    {
        // Use reflection to call the Parse method on Identifier<T>
        MethodInfo parseMethod = typeToConvert.BaseType!.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static)!;
        return parseMethod.Invoke(null, [idString])!;
    }
}