using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using HomeInventory.Model;
using JetBrains.Annotations;
using Xunit;

namespace HomInventory.Model.Tests;

[TestSubject(typeof(StockItemId))]
public class StockItemIdTest
{
    [Fact]
    public void JsonSerializeDeserialize_IdSerializedAsString()
    {
        // arrange
        var testEvent = new TestEvent { StockItemId = new StockItemId() };
        var jsonSerializerOptions = new JsonSerializerOptions { Converters = { new StockItemIdJsonConverter() } };
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
        var jsonDeSerializerOptions = new JsonSerializerOptions { Converters = { new StockItemIdJsonConverter() } };
        
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

public class StockItemIdJsonConverter : JsonConverter<StockItemId>
{
    public override StockItemId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Handle the flat Guid string case
        if (reader.TokenType == JsonTokenType.String)
        {
            return StockItemId.Parse(reader.GetString());
        }
        
        // Handle the nested structure case
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read(); // Move to "id" property name
            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == nameof(StockItemId.Id))
            {
                reader.Read(); // Move to the Guid string
                String id = reader.GetString();
                reader.Read(); // Move past the end of the object
                return StockItemId.Parse(id);
            }
        }

        throw new JsonException("Invalid JSON format for StockItemId.");
    }

    public override void Write(Utf8JsonWriter writer, StockItemId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id.ToString());
    }
}