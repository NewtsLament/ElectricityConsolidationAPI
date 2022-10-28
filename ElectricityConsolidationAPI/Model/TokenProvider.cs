using ElectricityConsolidationAPI.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uge43ProjektAPI.Models
{
    public class TokenProvider
    {

        private Dictionary<string, Tuple<DateTime, string>> _refreshTokens = new Dictionary<string, Tuple<DateTime, string>>();

        public async Task<string> GetRefreshToken(string apitoken)
        {
            string refreshToken = null;
            DateTime refreshTime = DateTime.Now;


            string hashedApitoken;
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] bytedApitoken = System.Text.Encoding.UTF8.GetBytes(apitoken);
                byte[] hashedBytesApitoken = sha.ComputeHash(bytedApitoken);
                hashedApitoken = BitConverter.ToString(hashedBytesApitoken).Replace("-", string.Empty);
            }
            Tuple<DateTime, string> tempToken;
            DateTime testTime;
            if (_refreshTokens.ContainsKey(hashedApitoken))
            {
                tempToken = _refreshTokens[hashedApitoken];

                refreshToken = tempToken.Item2;
                testTime = tempToken.Item1.AddHours(24);

            }
            else
            {
                tempToken = null;
                testTime = DateTime.MinValue;
            }

            if (tempToken == null || DateTime.Compare(testTime, DateTime.Now) <= 0)
            {
                using HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", apitoken);

                using HttpResponseMessage response = await client.GetAsync("https://api.eloverblik.dk/customerapi/api/token");

                var jsonResponse = await response.Content.ReadAsStringAsync();

                TokenResult deserializedEnergyReading = JsonSerializer.Deserialize<TokenResult>(jsonResponse);

                refreshToken = deserializedEnergyReading.Result;
                _refreshTokens.Add(hashedApitoken, new Tuple<DateTime, string>(refreshTime, refreshToken));
            }
            return refreshToken;
        }
        private class TokenResult
        {
            [JsonPropertyName("result")]
            public string Result { get; set; }
        }
    }
}
