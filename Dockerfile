# This Dockerfile is created by 
# ----- Eng. Moustafa Safwat -----

# Use the official Ollama image as the base image
FROM ollama/ollama:latest

# Set the working directory
WORKDIR /root/.ollama

# Copy entrypoint script into the container
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Set entrypoint to the script
ENTRYPOINT ["/entrypoint.sh"]

# Keep the default Ollama service running
CMD ["serve"]
