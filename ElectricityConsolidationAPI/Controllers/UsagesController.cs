using ElectricityConsolidationAPI.Model;
using ElectricityConsolidationAPI.Model.EloverblikUsageDTO;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;
using Uge43ProjektAPI.Models;
using static System.Net.WebRequestMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectricityConsolidationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsagesController : ControllerBase
    {
        TokenProvider _tokenProvider;
        public UsagesController(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }
        // GET: api/<UsageController>
        [HttpGet("{meterId}/{fromDate}/{toDate}")]
        public async Task<IActionResult> Get(string meterId, string fromDate, string toDate, [FromHeader] string authorization)
        {

            HttpClient client = new HttpClient();
            Root deserializedResponse;
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"https://api.eloverblik.dk/customerapi/api/meterdata/gettimeseries/{fromDate}/{toDate}/Actual"))
            {
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetRefreshToken(authorization));
                requestMessage.Content = JsonContent.Create(
                    new
                    {
                        meteringPoints = new
                        {
                            meteringPoint = new string[] {
                                meterId
                            }
                        }
                    }
                    );
                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                deserializedResponse = JsonSerializer.Deserialize<Root>(responseString);

            }

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            List<Usage> usages = new List<Usage>();
            var periods = deserializedResponse.Result[0].MyEnergyDataMarketDocument.TimeSeries[0].Period;
            foreach (var period in periods)
            {
                DateTime startDate = period.TimeInterval.Start;
                DateTime endDate = period.TimeInterval.End;
                foreach (var point in period.Point)
                {
                    int position = int.Parse(point.Position);
                    Usage temp = new Usage()
                    {
                        Consumption = Decimal.Parse(point.OutQuantityQuantity,provider),
                        DateAndTimeStart = startDate.AddHours(position-1),
                        DateAndTimeEnd = endDate.AddHours(position),
                    };
                    usages.Add(temp);
                }
            }
            Meter meterReturn = new Meter(long.Parse(deserializedResponse.Result[0].Id),usages);
            return Ok(meterReturn);
        }
    }
}
