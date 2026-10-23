using Microsoft.AspNetCore.Mvc;

namespace ProdcutManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching","Testing"
        ];
        private static List<string> SummariesList = new()
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching", "Testing"
};
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("summaries")]
        public ActionResult<IEnumerable<string>> GetSummaries()
        {
            return Ok(Summaries);
        }
        // POST: api/weatherforecast/summaries
        [HttpPost("summaries")]
        public ActionResult<string> AddSummary([FromBody] string newSummary)
        {
            if (string.IsNullOrWhiteSpace(newSummary))
            {
                return BadRequest("Summary cannot be empty.");
            }

            // Check if it already exists (optional validation)
            if (SummariesList.Contains(newSummary, StringComparer.OrdinalIgnoreCase))
            {
                
                return Conflict("Summary already exists.");
            }

            // Add the new value to the list
            SummariesList.Add(newSummary);

            // Return the added item and a 201 Created status
            return CreatedAtAction(nameof(GetSummaries), new { }, newSummary);
        }
    }
}
