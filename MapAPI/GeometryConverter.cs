using System.Text.Json;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace MapAPI
{
    public class GeometryConverter : JsonConverter<Geometry>
    {
        public override Geometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var geoJsonReader = new NetTopologySuite.IO.GeoJsonReader();
            var json = JsonDocument.ParseValue(ref reader);
            var geoJson = json.RootElement.GetRawText();
            return geoJsonReader.Read<Geometry>(geoJson);
        }

        public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options)
        {
            var geoJsonWriter = new NetTopologySuite.IO.GeoJsonWriter();
            var geoJson = geoJsonWriter.Write(value);
            using (var document = JsonDocument.Parse(geoJson))
            {
                document.WriteTo(writer);
            }
        }
    }
}
