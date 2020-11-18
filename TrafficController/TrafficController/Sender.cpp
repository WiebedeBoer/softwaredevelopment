#include "Sender.hpp"
std::string Sender::receiver()
{
	std::cout << "Socket start receiving!\n";
	std::cout << "Simulator data received!\n";
	return bufferdata;
}

/*
* Receiving data from the simulator.
*/
void Sender::receiving() {

	// Do-while loop to send data.

	do {				
		// Wait for response.
		//ZeroMemory(receivedbuffer, 1024);
		int bytesReceived = recv(clientSocket, receivedbuf, 1024, 0);
		isSending = true;
		if (bytesReceived > 0 && bytesReceived < 1000)
		{
			//Echo response to console.
			std::cout << "Socket buffer!\n";
			std::cout << "Receiving\n";
			std::cout << receivedbuf;
			std::cout << "\n";
			//Received data.
			ptr_controller->buffer = receivedbuf;
		}

	} while (isRunning);

}

/*
*Setup the socket. 
*/
void Sender::socketSetup() {
	// IP Address of the server.
	std::string ipAddress = "127.0.0.1";
	// Listening port # on the server.
	int port = 54000;						
	//Echo startup to console.
	std::cout << "Socket startup!\n";
	// Initialize WinSock.
	WSAData data;
	WORD ver = MAKEWORD(2, 2);
	int wsResult = WSAStartup(ver, &data);
	if (wsResult != 0)
	{
		std::cout << "Socket not init!\n";
	}

	// Create socket.
	SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET)
	{
		std::cout << "Socket not valid!\n";
		WSACleanup();
	}

	//Bind socket.
	SOCKADDR_IN serverInf;
	serverInf.sin_family = AF_INET;
	serverInf.sin_addr.s_addr = INADDR_ANY;
	serverInf.sin_port = htons(port);
	inet_pton(AF_INET, ipAddress.c_str(), &serverInf.sin_addr);

	if (bind(sock, (SOCKADDR*)(&serverInf), sizeof(serverInf)) == SOCKET_ERROR)
	{
		std::cout << "Unable to bind socket!\r\n";
		WSACleanup();
		system("PAUSE");
	}

	//Listen socket.
	int iResult;
	iResult = listen(sock, SOMAXCONN);
	if (iResult == SOCKET_ERROR) {
		printf("listen failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
	}

	// Wait for a connection.
	int clientSize = sizeof(serverInf);

	// Accept a client socket.
	SOCKET ClientSocket = accept(sock, (sockaddr*)&serverInf, &clientSize);
	if (ClientSocket == INVALID_SOCKET) {
		printf("accept failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
	}

	clientSocket = ClientSocket;
	isRunning = true;
}

/*
*Socket send data and controller as server.
*/
void Sender::socketServer(std::string traffic)
{
	//Echo to console sending.
	std::cout << "Socket start sending!\n";
	//Create send data.
	std::string length = std::to_string(traffic.length());
	std::string header = length + ":";
	std::string package = header + traffic;
	const char* Input = package.c_str();
	std::string str = traffic;

	do
	{
		//Send data size.
		int size = strlen(Input);
		// Make sure there is data.
		if (size > 0)		
		{
			// Send the text.
			int sendResult = 0;
			if (true) {
				sendResult = send(clientSocket, Input, size, 0);
				isSending = false;
			}

			if (sendResult != SOCKET_ERROR)
			{

			}
			else {
				std::cout << "Socket result error!\n";
				std::cout << WSAGetLastError;
			}
		}
		str = "";

	} while (str.size() > 0);

	//Close down everything.
	//closesocket(sock);
	//closesocket(ClientSocket);
	//WSACleanup();
	//Echo phase closed.
	std::cout << "Phase closed!\n";
}