#include "pch.h"
#include "Sender.h"

void Sender::receiving() {

	// Do-while loop to send data
	
	do {
		

		//std::cout << "Socket no result error!\n";					
		// Wait for response
		ZeroMemory(receivedbuffer, 1024);
		//int bytesReceived = recv(sock, buf, 4096, 0);
		int bytesReceived = recv(clientSocket, receivedbuffer, 1024, 0);
		isSending = true;
		if (bytesReceived > 0)
		{
			// Echo response to console
			std::cout << "Socket buffer!\n";
			std::cout << "Receiving\n";
			std::cout << receivedbuffer;
			std::cout << "\n";
			//receivedbuffer = buf;
		}


	} while (isRunning);


}

void Sender::socketSetup() {

	std::string ipAddress = "127.0.0.1";			// IP Address of the server
	int port = 54000;						// Listening port # on the server

	std::cout << "Socket startup!\n";
	// Initialize WinSock
	WSAData data;
	WORD ver = MAKEWORD(2, 2);
	int wsResult = WSAStartup(ver, &data);
	if (wsResult != 0)
	{
		std::cout << "Socket not init!\n";
		//return 0;
	}

	// Create socket
	SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET)
	{
		std::cout << "Socket not valid!\n";
		WSACleanup();
		//return 0;
	}

	//bind
	SOCKADDR_IN serverInf;
	serverInf.sin_family = AF_INET;
	serverInf.sin_addr.s_addr = INADDR_ANY;
	serverInf.sin_port = htons(port);
	inet_pton(AF_INET, ipAddress.c_str(), &serverInf.sin_addr); //pton

	if (bind(sock, (SOCKADDR*)(&serverInf), sizeof(serverInf)) == SOCKET_ERROR)
	{
		std::cout << "Unable to bind socket!\r\n";
		WSACleanup();
		system("PAUSE");
		//return 0;
	}

	//listen
	int iResult;
	iResult = listen(sock, SOMAXCONN);
	if (iResult == SOCKET_ERROR) {
		printf("listen failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
		//return 0;
	}

	// Wait for a connection
	int clientSize = sizeof(serverInf);

	// Accept a client socket
	SOCKET ClientSocket = accept(sock, (sockaddr*)&serverInf, &clientSize);
	if (ClientSocket == INVALID_SOCKET) {
		printf("accept failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
		//return 0;
	
	}
	

	clientSocket = ClientSocket;
	isRunning = true;
}

void Sender::socketServer(string traffic)
{


	//create send data
	std::cout << "Socket start sending!\n";

	//string traffic = controller::changetraffic(modorder);
	string length = std::to_string(traffic.length());
	string header = length + ":";
	string package = header + traffic;
	const char* Input = package.c_str();
	std::string str = traffic;

	//std::thread sender;
	//std::thread receiv;

	do
	{
		// get text
		int size = strlen(Input);
		if (size > 0)		// Make sure there is input
		{
			// Send the text
			int sendResult =0;
			if (isSending) {
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




	// Gracefully close down everything
	//closesocket(sock);
	//closesocket(ClientSocket);
	//WSACleanup();
	std::cout << "Phase closed!\n";
}
