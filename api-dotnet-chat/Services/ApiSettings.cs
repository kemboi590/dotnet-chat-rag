using System;

namespace dotnet_chat_rag.Services;

public class ApiSettings
{
    public required string ApiKey { get; set; }
    public required string Endpoint { get; set; }
}
