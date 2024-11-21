# Azure GPT-4 Model Integration

This project demonstrates how to integrate GPT-4 deployed on Azure into a .NET and React-based application. It is designed for learning purposes, with a step-by-step guide to set up both the API and client components.

## Project Structure

The project consists of:

1. **API**: A .NET backend for handling GPT-4 interactions.
2. **Client**: A React-based frontend for interacting with the API.

---

## Installation Guide

### Clone the Repository

To get started, clone the project from the repository:

```sh
git clone https://github.com/kemboi590/dotnet-chat-rag.git
```

---

### API Setup

1. **Navigate to the API Directory**

   ```sh
   cd api-dotnet-chat
   ```

2. **Restore Dependencies**

   Restore all required NuGet packages:

   ```sh
   dotnet restore
   ```

3. **Add API Key and Endpoint**

   Create a file named `appsettings.json` in the root of the `api-dotnet-chat` directory. Add the following configuration:

   ```json
   {
     "ApiSettings": {
       "ApiKey": "Your-API-Key-Here",
       "Endpoint": "Your-Endpoint-Here"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

   Replace `Your-API-Key-Here` and `Your-Endpoint-Here` with your actual Azure GPT-4 API key and endpoint.

4. **Run the Application**

   Start the API server with:

   ```sh
   dotnet run
   ```

---

### Client Setup (React)

1. **Navigate to the Client Directory**

   ```sh
   cd client-dotnet-chat
   ```

2. **Install Dependencies**

   Install all required packages using `pnpm`:

   ```sh
   pnpm install
   ```

3. **Run the Client**

   Start the React development server:

   ```sh
   pnpm run dev
   ```

---

## Final Notes

Congratulations! Your application should now be up and running.  
Feel free to explore and customize the project for your needs.

Happy coding! 🚀
