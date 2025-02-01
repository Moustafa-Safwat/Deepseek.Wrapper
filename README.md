# Deepseek.Wrapper

Deepseek.Wrapper is a modern WPF application targeting .NET 8. It provides an interactive chat interface that communicates with an AI model to handle and respond to user messages. The application is designed with a focus on dynamic theming and a user-friendly interface.

## Key Features

- **Interactive Chat Interface**: Users can send and receive messages in a seamless chat environment.
- **Dynamic Theming**: The application supports dynamic theme changes, ensuring a consistent look and feel based on the current theme.
- **AI Integration**: Utilizes an AI model to process and respond to user messages, providing intelligent and context-aware interactions.
- **Modern UI Design**: Built using WPF UI components for a sleek and responsive user experience.

## Components

### Projects

- **WPF.Deepseek.Wrapper**: The main project containing the WPF application.

### Key Files

- **ChatPage.xaml**: Defines the layout and structure of the chat page.
- **MainWindow.xaml**: Defines the layout and structure of the main window.
- **ChatService.cs**: Handles sending messages to the AI model and streaming responses back to the user.
- **BooleanToBrushConverter.cs**: Converts boolean values to brushes for dynamic theming.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022

### Installation

1. Clone the repository:
    
    git clone https://github.com/your-repo/WPF.Deepseek.Wrapper.git
2. Navigate to the folder with the provided custom Dockerfile and Docker Compose file, and run:
[docker compose up] to start your container

### Configuration

Ensure that the `appsettings.json` file contains the necessary configuration for the AI model and API URL.

### Building and Running

1. Build the solution:
    
    dotnet build
2. Run the application:
    
    dotnet run

## Usage

- **ChatPage**: The main page where users can interact with the chat interface.
- **MainWindow**: The main window of the application that hosts the navigation and content.

### Key Components

- **ChatService**: Handles sending messages to the AI model and receiving responses.
- **BooleanToBrushConverter**: Converts boolean values to brushes for dynamic theming.
- **MainWindow.xaml**: Defines the layout and structure of the main window.
- **ChatPage.xaml**: Defines the layout and structure of the chat page.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.

   
