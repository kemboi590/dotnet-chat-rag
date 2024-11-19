using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using dotnet_chat_rag.Services;
using Microsoft.Extensions.Options;

namespace dotnet_chat_rag.Controllers
{
    [Route("api/[controller]")] //api/chat/ask
    [ApiController]
    public class ChatController : ControllerBase
    {
        // private const string API_KEY = "BMUkarf6ePtyjSRDdOlk9K1svu18r2Hyr3xOg2JNOA47qPjEChXQJQQJ99AKACrIdLPXJ3w3AAAAACOGRLJq"; // Set your API key here
        // private const string ENDPOINT = "https://dotnetchat4614159492.openai.azure.com/openai/deployments/gpt-4o/chat/completions?api-version=2024-08-01-preview";

        private readonly ApiSettings _apiSettings;
        public readonly HttpClient _httpClient;
        public ChatController(IOptions<ApiSettings> apiSettings, HttpClient httpClient)
        {
            _apiSettings = apiSettings.Value;
            _httpClient = httpClient;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                // httpClient.DefaultRequestHeaders.Add("api-key", API_KEY);
                _httpClient.DefaultRequestHeaders.Add("api-key", _apiSettings.ApiKey);
                
                var payload = new
                {
                    messages = new object[]
                    {
                        new {
                            role = "system",
                            content = new object[]
                            {
                                new {
                                    type = "text",
                                    text = "You are an AI assistant that helps people find information."
                                }
                            }
                        },
                        new {
                            role = "user",
                            content = new object[]
                            {
                                new {
                                    type = "text",
                                    text = request.Question
                                }
                            }
                        }
                    }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
                var response = await _httpClient.PostAsync(_apiSettings.Endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JsonDocument.Parse(responseString);

                var messageContent = responseJson.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return Ok(new { content = messageContent });


            }


        }
    }

    public class QuestionRequest
    {
        public required string Question { get; set; }
    }
}