using MongoDB.Bson;
using Newtonsoft.Json;

namespace web_app_domain
{
    public class ObjectIdConverter : JsonConverter<ObjectId>
    {
        public override void WriteJson(JsonWriter writer, ObjectId value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override ObjectId ReadJson(JsonReader reader, Type objectType, ObjectId existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return ObjectId.TryParse(reader.Value?.ToString(), out var objectId) ? objectId : ObjectId.Empty;
        }
    }
}
