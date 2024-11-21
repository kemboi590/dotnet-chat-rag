using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using dotnet_chat_rag.Services;
using Microsoft.Extensions.Options;

namespace dotnet_chat_rag.Controllers
{
    [Route("api/[controller]")] //https://localhost:5001/api/chat/ask
    [ApiController]
    public class ChatController : ControllerBase //base class is a class that handles HTTP requests
    {
        private readonly ApiSettings _apiSettings;
        public readonly HttpClient _httpClient; //HttpClient is used to send HTTP requests and receive HTTP responses from a resource identified by a URI
        public ChatController(IOptions<ApiSettings> apiSettings, HttpClient httpClient) //Constructor helps to initialize the object of the class. i.e HttpClient, ApiSettings
        {
            _apiSettings = apiSettings.Value;
            _httpClient = httpClient;
        }

        [HttpPost("ask")] //POST method means to create a new resource (CRUD operation)
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request) //FromBody attribute is used to bind the request body to a parameter in a controller action
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
                                    text = "You are an AI assistant that helps people find information. Please provide concise responses."
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

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));// selialize the payload to JSON format (string format - Json formart)
                var response = await _httpClient.PostAsync(_apiSettings.Endpoint, content); //Send a POST request to the specified Uri as an asynchronous operation
                var responseString = await response.Content.ReadAsStringAsync(); //Read HTTP content as a string from GPT4 API
                var responseJson = JsonDocument.Parse(responseString); //convert the JSON response to a JSON document

                var messageContent = responseJson.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString(); //Get the response from the GPT4 API

                return Ok(new { content = messageContent });
            }
        }
    }

    public class QuestionRequest
    {
        public required string Question { get; set; }
    }
}
