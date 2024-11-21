using dotnet_chat_rag.Controllers;
using dotnet_chat_rag.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // used to add controllers to the application
builder.Services.AddOpenApi(); // used to describe the API in a standard way

// Configure ApiSettings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings")); // used to configure the API settings

builder.Services.AddHttpClient<ChatController>();//HttpClient is used to send HTTP requests and receive HTTP responses from a resource identified by a URI

// Add CORS policy - API can be accessed from any domain
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", 
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


var app = builder.Build(); // the application is ready to run

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection(); // used to redirect HTTP requests to HTTPS

app.UseAuthorization(); // used to authorize the user to access the API

// Use CORS policy
app.UseCors("AllowAllOrigins");

app.MapControllers(); // application is ready to accept requests

app.Run();

