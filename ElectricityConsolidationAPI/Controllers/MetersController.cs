using ElectricityConsolidationAPI.Model;
using ElectricityConsolidationAPI.Model.EloverblikMeterDTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Uge43ProjektAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectricityConsolidationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetersController : ControllerBase
    {
        private TokenProvider _tokenProvider;
        public MetersController(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }
        // GET: api/<MeterController>
        [HttpGet]
        public async Task<ActionResult<string>> Get([FromHeader] string authorization)
        {
            HttpClient client = new HttpClient();
            Root deserializedResponse;
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://api.eloverblik.dk/customerapi/api/meteringpoints/meteringpoints?includeAll=false"))
            {
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _tokenProvider.GetRefreshToken(authorization));
                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                deserializedResponse = JsonSerializer.Deserialize<Root>(await response.Content.ReadAsStringAsync());

            }

            List<Meter> meters = new List<Meter>();
            foreach (var item in deserializedResponse.Result)
            {
                meters.Add(new Meter(long.Parse(item.MeteringPointId)));
            }
            return Ok(new { meters });
        }
    }
}
