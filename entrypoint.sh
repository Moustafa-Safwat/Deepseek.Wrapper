#!/bin/sh
# Start the Ollama server in the background
ollama serve &

# Wait for the server to be fully up (Optional: Sleep to avoid race conditions)
sleep 5 

# Run the DeepSeek model
ollama run deepseek-r1:7b

# Keep the container running
wait
