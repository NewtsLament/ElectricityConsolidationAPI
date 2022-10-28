using System.Text.Json.Serialization;

namespace ElectricityConsolidationAPI.Model
{
    public class Meter
    {
        private long _id;
        [JsonPropertyName("id")]
        public long Id
        {
            get { return _id; }
        }
        private List<Usage>? _usages;
        [JsonPropertyName("usages")]
        public List<Usage>? Usages
        {
            get { return _usages; }
        }

        public Meter(long id, List<Usage>? usages)
        {
            _id = id;
            _usages = usages;
        }

        public Meter(long id):this(id,null)
        {
        }
    }
}
