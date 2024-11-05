using System.Text.Json;
using FluentAssertions;
using HomeInventory.Model;
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
        var stockItemId = new StockItemId();
        var eventJson = @$"{{""StockItemId"":""{stockItemId.Id}""}}";

        var jsonSerializerOptions = new JsonSerializerOptions { Converters = { new IdentifierJsonConverter() } };

        // act
        var deserializedEvent = JsonSerializer.Deserialize<TestEvent>(eventJson, jsonSerializerOptions)!;

        // assert
        deserializedEvent.StockItemId.Should().Be(stockItemId);
    }

    [Fact]
    public void JsonSerializeDeserialize_IdSerializedAsObject()
    {
        // arrange
        var stockItemId = new StockItemId();
        var eventJson = @$"{{""StockItemId"":{{""Id"":""{stockItemId.Id}""}}}}";

        var jsonSerializerOptions = new JsonSerializerOptions { Converters = { new IdentifierJsonConverter() } };

        // act
        var deserializedEvent = JsonSerializer.Deserialize<TestEvent>(eventJson, jsonSerializerOptions)!;

        // assert
        deserializedEvent.StockItemId.Should().Be(stockItemId);
    }
}

internal class TestEvent
{
    public StockItemId StockItemId { get; init; }
}