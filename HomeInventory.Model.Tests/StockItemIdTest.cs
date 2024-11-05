using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using HomeInventory.Model;
using HomeInventory.Model.Common;
using JetBrains.Annotations;
using Xunit;

namespace HomInventory.Model.Tests;

[TestSubject(typeof(StockItemId))]
public class StockItemIdTest
{
    [Fact]
    public void JsonSerialize()
    {
        // arrange
        var testEvent = new TestEvent { StockItemId = new StockItemId() };
        
        var jsonSerializerOptions = new JsonSerializerOptions { Converters = { new IdentifierJsonConverter() } };
        
        // act
        string eventJson = JsonSerializer.Serialize(testEvent, jsonSerializerOptions);
        
        // assert
        eventJson.Should().Be($@"{{""StockItemId"":""{testEvent.StockItemId.Id.ToString()}""}}");
    }
    
    [Fact]
    public void JsonSerializeDeserialize_IdSerializedAsString()
    {
        // arrange
        var testEvent = new TestEvent { StockItemId = new StockItemId() };
        var jsonSerializerOptions = new JsonSerializerOptions { Converters = { new IdentifierJsonConverter() } };
        string eventJson = JsonSerializer.Serialize(testEvent, jsonSerializerOptions);
        
        // act
        var deserializedEvent = JsonSerializer.Deserialize<TestEvent>(eventJson, jsonSerializerOptions);

        // assert
        deserializedEvent.StockItemId.Should().Be(testEvent.StockItemId);
    }
    
    [Fact]
    public void JsonSerializeDeserialize_IdSerializedAsObject()
    {
        // arrange
        var testEvent = new TestEvent { StockItemId = new StockItemId() };
        string eventJson = JsonSerializer.Serialize(testEvent);
        var jsonDeSerializerOptions = new JsonSerializerOptions { Converters = { new IdentifierJsonConverter() } };
        
        // act
        var deserializedEvent = JsonSerializer.Deserialize<TestEvent>(eventJson, jsonDeSerializerOptions);

        // assert
        deserializedEvent.StockItemId.Should().Be(testEvent.StockItemId);
    }

}

internal class TestEvent
{
    public StockItemId StockItemId { get; init; }
}

public class IdentifierJsonConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        // Check if the type is a subclass of Identifier<>
        if (!typeToConvert.BaseType?.IsGenericType ?? true)
        {
            return false;
        }
        
        return typeToConvert.BaseType.GetGenericTypeDefinition() == typeof(Identifier<>);
    }

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Handle the flat Guid string case
        if (reader.TokenType == JsonTokenType.String)
        {
            string idString = reader.GetString()!;
            return ParseIdentifier(idString, typeToConvert);
        }

        // Handle the nested structure case
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read(); // Move to the "id" property
            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "Id")
            {
                reader.Read(); // Move to the Guid string value
                string idString = reader.GetString()!;
                reader.Read(); // Move past the end of the object
                return ParseIdentifier(idString, typeToConvert);
            }
        }

        throw new JsonException($"Invalid JSON format for {typeToConvert.Name}.");
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        // Serialize as a flat Guid string
        PropertyInfo idProperty = value.GetType().GetProperty("Id")!;
        writer.WriteStringValue(idProperty.GetValue(value)!.ToString());
    }

    private static object ParseIdentifier(string idString, Type typeToConvert)
    {
        // Use reflection to call the Parse method on Identifier<T>
        MethodInfo parseMethod = typeToConvert.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);
        return parseMethod.Invoke(null, new object[] { idString });
    }
}