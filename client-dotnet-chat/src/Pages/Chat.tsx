import React, { useState } from "react";
import axios from "axios";
import Header from "./Header";

const Chat: React.FC = () => {
  const [userInput, setUserInput] = useState<string>("");
  const [messages, setMessages] = useState<
    { role: "user" | "ai"; content: string }[]
  >([]);

  const sendMessage = async () => {
    if (!userInput.trim()) return;

    // Add user message to chat
    setMessages((prev) => [...prev, { role: "user", content: userInput }]);

    try {
      // Send request to the backend
      const response = await axios.post("http://localhost:5080/api/chat/ask", {
        question: userInput,
      });

      // Update messages with AI response
      const aiResponse = response.data.content;
      setMessages((prev) => [...prev, { role: "ai", content: aiResponse }]);
    } catch (error) {
      console.error("Error fetching AI response:", error);
      setMessages((prev) => [
        ...prev,
        { role: "ai", content: "Sorry, an error occurred. Please try again." },
      ]);
    }

    // Clear the input field
    setUserInput("");
  };

  return (
    <div className="flex flex-col h-screen bg-gray-100 p-4">
       <Header />
      <div className="flex-1 overflow-y-auto bg-white p-4 rounded shadow">
        {messages.map((msg, index) => (
          <div
            key={index}
            className={`mb-2 ${msg.role === "user" ? "text-right" : "text-left"
              }`}
          >
            <div
              className={`inline-block px-4 py-2 rounded ${msg.role === "user"
                  ? "bg-blue-500 text-white"
                  : "bg-gray-200 text-gray-900"
                }`}
            >
              {msg.content}
            </div>
          </div>
        ))}
      </div>

      <div className="mt-4 flex items-center space-x-2">
        <input
          type="text"
          className="flex-1 p-2 border rounded focus:outline-none focus:ring focus:ring-blue-300"
          placeholder="Type your question..."
          value={userInput}
          onChange={(e) => setUserInput(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && sendMessage()}
        />
        <button
          onClick={sendMessage}
          className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
        >
          Send
        </button>
      </div>
    </div>
  );
};

export default Chat;
