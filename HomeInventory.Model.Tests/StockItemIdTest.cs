using System.Text.Json;
using FluentAssertions;
using HomeInventory.Model;
using JetBrains.Annotations;
using Xunit;

namespace HomInventory.Model.Tests;

[TestSubject(typeof(StockItemId))]
public class StockItemIdTest
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { Converters = { new IdentifierJsonConverterFactory() } };

    [Fact]
    public void JsonSerialize()
    {
        // arrange
        var testEvent = new TestEvent { StockItemId = new StockItemId() };

        // act
        string eventJson = JsonSerializer.Serialize(testEvent, _jsonSerializerOptions);

        // assert
        eventJson.Should().Be($@"{{""StockItemId"":""{testEvent.StockItemId.Id.ToString()}""}}");
    }

    [Fact]
    public void JsonSerializeDeserialize_IdSerializedAsString()
    {
        // arrange
        var stockItemId = new StockItemId();
        var eventJson = @$"{{""StockItemId"":""{stockItemId.Id}""}}";

        // act
        var deserializedEvent = JsonSerializer.Deserialize<TestEvent>(eventJson, _jsonSerializerOptions)!;

        // assert
        deserializedEvent.StockItemId.Should().Be(stockItemId);
    }

    [Fact]
    public void JsonSerializeDeserialize_IdSerializedAsObject()
    {
        // arrange
        var stockItemId = new StockItemId();
        var eventJson = @$"{{""StockItemId"":{{""Id"":""{stockItemId.Id}""}}}}";

        // act
        var deserializedEvent = JsonSerializer.Deserialize<TestEvent>(eventJson, _jsonSerializerOptions)!;

        // assert
        deserializedEvent.StockItemId.Should().Be(stockItemId);
    }
}

internal class TestEvent
{
    public StockItemId StockItemId { get; init; }
}