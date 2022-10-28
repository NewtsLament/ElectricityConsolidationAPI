using System.Text.Json.Serialization;

namespace ElectricityConsolidationAPI.Model.EloverblikMeterDTO
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Result
    {
        [JsonPropertyName("streetCode")]
        public string StreetCode { get; set; }

        [JsonPropertyName("streetName")]
        public string StreetName { get; set; }

        [JsonPropertyName("buildingNumber")]
        public string BuildingNumber { get; set; }

        [JsonPropertyName("floorId")]
        public string FloorId { get; set; }

        [JsonPropertyName("roomId")]
        public string RoomId { get; set; }

        [JsonPropertyName("citySubDivisionName")]
        public string CitySubDivisionName { get; set; }

        [JsonPropertyName("municipalityCode")]
        public string MunicipalityCode { get; set; }

        [JsonPropertyName("locationDescription")]
        public object LocationDescription { get; set; }

        [JsonPropertyName("settlementMethod")]
        public string SettlementMethod { get; set; }

        [JsonPropertyName("meterReadingOccurrence")]
        public string MeterReadingOccurrence { get; set; }

        [JsonPropertyName("firstConsumerPartyName")]
        public string FirstConsumerPartyName { get; set; }

        [JsonPropertyName("secondConsumerPartyName")]
        public object SecondConsumerPartyName { get; set; }

        [JsonPropertyName("meterNumber")]
        public string MeterNumber { get; set; }

        [JsonPropertyName("consumerStartDate")]
        public DateTime ConsumerStartDate { get; set; }

        [JsonPropertyName("meteringPointId")]
        public string MeteringPointId { get; set; }

        [JsonPropertyName("typeOfMP")]
        public string TypeOfMP { get; set; }

        [JsonPropertyName("balanceSupplierName")]
        public string BalanceSupplierName { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("cityName")]
        public string CityName { get; set; }

        [JsonPropertyName("hasRelation")]
        public bool HasRelation { get; set; }

        [JsonPropertyName("consumerCVR")]
        public object ConsumerCVR { get; set; }

        [JsonPropertyName("dataAccessCVR")]
        public object DataAccessCVR { get; set; }

        [JsonPropertyName("childMeteringPoints")]
        public List<object> ChildMeteringPoints { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("result")]
        public List<Result> Result { get; set; }
    }


}
