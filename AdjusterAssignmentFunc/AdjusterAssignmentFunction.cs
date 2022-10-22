using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace AdjusterAssignmentFunc
{
    public static class AdjusterAssignmentFunction
    {
        [FunctionName("AdjusterAssignmentTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://20.103.212.223/AdjusterMatching");

            string requestBody = String.Empty;
           
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();    
            }
            var claimInput = JsonConvert.DeserializeObject<Claim>(requestBody);

            string json = JsonConvert.SerializeObject(claimInput);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpClient http = new HttpClient();

            HttpResponseMessage response = await http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;

                var assignmentInput = JsonConvert.DeserializeObject<AdjAssignment>(jsonString);
                assignmentInput.Id = "";
                json = JsonConvert.SerializeObject(assignmentInput);
                HttpRequestMessage requestOne = new HttpRequestMessage(HttpMethod.Post, "http://20.103.211.34/AdjusterAssignment");

                requestOne.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage responseOne = await http.SendAsync(requestOne);
                if (responseOne.IsSuccessStatusCode)
                {
                    var result = responseOne.Content.ReadAsStringAsync().Result;
                }
                else
                {

                }
            }
            else
            {

            }

            return new OkObjectResult(response);
        }
    }
}
