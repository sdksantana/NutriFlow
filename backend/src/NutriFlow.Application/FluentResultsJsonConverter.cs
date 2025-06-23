using FluentResults;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NutriFlow.Application
{
    public class FluentResultsJsonConverter : JsonConverter<Result>
    {
        public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteBoolean("isSuccess", value.IsSuccess);
            writer.WriteBoolean("isFailed", value.IsFailed);

            writer.WritePropertyName("errors");
            JsonSerializer.Serialize(writer, value.Errors.Select(e => new { message = e.Message }), options);

            writer.WritePropertyName("successes");
            JsonSerializer.Serialize(writer, value.Successes, options);

            writer.WriteEndObject();
        }
    }
}
