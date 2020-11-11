// Controller.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include "trafficlight.h"
#include <iostream>
#include <sstream>
#include <stdio.h> 
#include <string.h> 
#include <stdlib.h> 
#include <ctime>
#include <WS2tcpip.h>
#include "Controller.h"
#pragma comment(lib, "ws2_32.lib")
#include <sys/types.h>

#include <nlohmann/json.hpp> 
using json = nlohmann::json;
using u_short = unsigned short;

int main()
{
	std::cout << "Hello World!\n";
	controller controller;
	//send traffic lights
	controller.sendlight();
}

int controller::sendlight() {
	int order = 1; //start phase
	const char* traffic;
	const char* header;
	const char* package;
	int modorder;

	std::cout << "Startup sending!\n";

	//timing
	double time_counter = 0;
	clock_t this_time = clock();
	clock_t last_time = this_time;
	const int NUM_SECONDS = 4;

	//setup socket
	SOCKET sock = socketSetup();

	//first send
	std::cout << "First send!\n";
	socketServer(order, sock);

	//run clock
	while (true)
	{
		this_time = clock();
		time_counter += (double)(this_time - last_time);
		last_time = this_time;

		if (time_counter > (double)(NUM_SECONDS * CLOCKS_PER_SEC))
		{
			//timer counter
			time_counter -= (double)(NUM_SECONDS * CLOCKS_PER_SEC);

			std::cout << "Sending phase \n";

			//phase calculation
			modorder = (order % 6) + 1;
			std::cout << modorder;

			//receiving from simulator
			string received = receiver(modorder, sock);
			//parsing from received
			modorder = parsejson(received, modorder);
			
			//traffic lights send
			socketServer(modorder, sock);
			std::cout << "phase Send \n"; //package every 4 seconds
		}
		//continuous order
		order++;
	}

	int lightssend = 1;
	return lightssend;
}

//which lights on green
string controller::changetraffic(int order) {

	string traffic;
	//rechtdoor noord - zuid, oost - west bus 
	if (order == 1)
	{
		traffic = "{'A1-1':1,'A1-2':1,'A1-3':0,'B1-1':1,'B1-2':1,'F1-1':0,'F1-2':0,'V1-1':0,'V1-2':0,'V1-3':0,'V1-4':0,'A2-1':1,'A2-2':1,'A2-3':0,'A2-4':0,'F2-1':0,'F2-2':0,'V2-1':0,'V2-2':0,'V2-3':0,'V2-4':0,'A3-1':0,'A3-2':0,'A3-3':0,'A3-4':0,'A4-1':0,'A4-2':0,'A4-3':0,'A4-4':0,'B4-1':1,'F4-1':0,'F4-2':0,'V4-1':0,'V4-2':0,'V4-3':0,'V4-4':0,'A5-1':0,'A5-2':0,'A5-3':1,'A5-4':1,'F5-1':0,'F5-2':0,'V5-1':0,'V5-2':0,'V5-3':0,'V5-4':0,'A6-1':1,'A6-2':1,'A6-3':0,'A6-4':0}";
	}
	//rechtdoor en rechtsaf noord - zuid auto 
	else if (order == 2) {
		traffic = "{'A1-1':1,'A1-2':1,'A1-3':1,'B1-1':0,'B1-2':0,'F1-1':0,'F1-2':0,'V1-1':0,'V1-2':0,'V1-3':0,'V1-4':0,'A2-1':1,'A2-2':1,'A2-3':0,'A2-4':0,'F2-1':0,'F2-2':0,'V2-1':0,'V2-2':0,'V2-3':0,'V2-4':0,'A3-1':0,'A3-2':0,'A3-3':0,'A3-4':0,'A4-1':1,'A4-2':1,'A4-3':1,'A4-4':1,'B4-1':0,'F4-1':0,'F4-2':0,'V4-1':0,'V4-2':0,'V4-3':0,'V4-4':0,'A5-1':0,'A5-2':0,'A5-3':1,'A5-4':1,'F5-1':0,'F5-2':0,'V5-1':0,'V5-2':0,'V5-3':0,'V5-4':0,'A6-1':0,'A6-2':0,'A6-3':0,'A6-4':0}";
	}
	//rechtdoor en rechtsaf oost - west auto 
	else if (order == 3) {
		traffic = "{'A1-1':0,'A1-2':0,'A1-3':0,'B1-1':0,'B1-2':0,'F1-1':0,'F1-2':0,'V1-1':0,'V1-2':0,'V1-3':0,'V1-4':0,'A2-1':1,'A2-2':1,'A2-3':1,'A2-4':1,'F2-1':0,'F2-2':0,'V2-1':0,'V2-2':0,'V2-3':0,'V2-4':0,'A3-1':0,'A3-2':0,'A3-3':1,'A3-4':1,'A4-1':0,'A4-2':0,'A4-3':0,'A4-4':0,'B4-1':0,'F4-1':0,'F4-2':0,'V4-1':0,'V4-2':0,'V4-3':0,'V4-4':0,'A5-1':1,'A5-2':1,'A5-3':1,'A5-4':1,'F5-1':0,'F5-2':0,'V5-1':0,'V5-2':0,'V5-3':0,'V5-4':0,'A6-1':1,'A6-2':1,'A6-3':0,'A6-4':0}";
	}
	//linksaf noord - west en oost - zuid auto
	else if (order == 4) {
		traffic = "{'A1-1':1,'A1-2':1,'A1-3':0,'B1-1':0,'B1-2':0,'F1-1':0,'F1-2':0,'V1-1':0,'V1-2':0,'V1-3':0,'V1-4':0,'A2-1':0,'A2-2':0,'A2-3':0,'A2-4':0,'F2-1':0,'F2-2':0,'V2-1':0,'V2-2':0,'V2-3':0,'V2-4':0,'A3-1':1,'A3-2':1,'A3-3':1,'A3-4':1,'A4-1':0,'A4-2':0,'A4-3':1,'A4-4':1,'B4-1':0,'F4-1':0,'F4-2':0,'V4-1':0,'V4-2':0,'V4-3':0,'V4-4':0,'A5-1':0,'A5-2':0,'A5-3':0,'A5-4':0,'F5-1':0,'F5-2':0,'V5-1':0,'V5-2':0,'V5-3':0,'V5-4':0,'A6-1':1,'A6-2':1,'A6-3':1,'A6-4':1}";
	}
	//linksaf noord - oost en zuid - west auto 
	else if (order == 5) {
		traffic = "{'A1-1':1,'A1-2':1,'A1-3':1,'B1-1':0,'B1-2':0,'F1-1':0,'F1-2':0,'V1-1':0,'V1-2':0,'V1-3':0,'V1-4':0,'A2-1':0,'A2-2':0,'A2-3':0,'A2-4':0,'F2-1':0,'F2-2':0,'V2-1':0,'V2-2':0,'V2-3':0,'V2-4':0,'A3-1':0,'A3-2':0,'A3-3':0,'A3-4':0,'A4-1':1,'A4-2':1,'A4-3':1,'A4-4':1,'B4-1':0,'F4-1':0,'F4-2':0,'V4-1':0,'V4-2':0,'V4-3':0,'V4-4':0,'A5-1':0,'A5-2':0,'A5-3':1,'A5-4':1,'F5-1':0,'F5-2':0,'V5-1':0,'V5-2':0,'V5-3':0,'V5-4':0,'A6-1':0,'A6-2':0,'A6-3':0,'A6-4':0}";
	}
	//fietsverkeer en voetgangersverkeer 
	else if (order == 6) {
		traffic = "{'A1-1':0,'A1-2':0,'A1-3':0,'B1-1':0,'B1-2':0,'F1-1':1,'F1-2':1,'V1-1':1,'V1-2':1,'V1-3':1,'V1-4':1,'A2-1':0,'A2-2':0,'A2-3':0,'A2-4':0,'F2-1':1,'F2-2':1,'V2-1':1,'V2-2':1,'V2-3':1,'V2-4':1,'A3-1':0,'A3-2':0,'A3-3':0,'A3-4':0,'A4-1':0,'A4-2':0,'A4-3':0,'A4-4':0,'B4-1':0,'F4-1':1,'F4-2':1,'V4-1':1,'V4-2':1,'V4-3':1,'V4-4':1,'A5-1':0,'A5-2':0,'A5-3':0,'A5-4':0,'F5-1':1,'F5-2':1,'V5-1':1,'V5-2':1,'V5-3':1,'V5-4':1,'A6-1':0,'A6-2':0,'A6-3':0,'A6-4':0}";
	}
	//default all orange
	else {
		traffic = "{'A1-1':2,'A1-2':2,'A1-3':2,'B1-1':2,'B1-2':2,'F1-1':2,'F1-2':2,'V1-1':2,'V1-2':2,'V1-3':2,'V1-4':2,'A2-1':2,'A2-2':2,'A2-3':2,'A2-4':2,'F2-1':2,'F2-2':2,'V2-1':2,'V2-2':2,'V2-3':2,'V2-4':2,'A3-1':2,'A3-2':2,'A3-3':2,'A3-4':2,'A4-1':2,'A4-2':2,'A4-3':2,'A4-4':2,'B4-1':2,'F4-1':2,'F4-2':2,'V4-1':2,'V4-2':2,'V4-3':2,'V4-4':2,'A5-1':2,'A5-2':2,'A5-3':2,'A5-4':2,'F5-1':2,'F5-2':2,'V5-1':2,'V5-2':2,'V5-3':2,'V5-4':2,'A6-1':2,'A6-2':2,'A6-3':2,'A6-4':2}";
	}
	return traffic;
}

SOCKET controller::socketSetup() {

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
		return 0;
	}

	// Create socket
	SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET)
	{
		std::cout << "Socket not valid!\n";
		WSACleanup();
		return 0;
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
		return 0;
	}

	//listen
	int iResult;
	iResult = listen(sock, SOMAXCONN);
	if (iResult == SOCKET_ERROR) {
		printf("listen failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
		return 0;
	}

	// Wait for a connection
	int clientSize = sizeof(serverInf);

	// Accept a client socket
	SOCKET ClientSocket = accept(sock, (sockaddr*)&serverInf, &clientSize);
	if (ClientSocket == INVALID_SOCKET) {
		printf("accept failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
		return 0;
	}

	return ClientSocket;
}

void controller::socketServer(int modorder, SOCKET ClientSocket)
{
	std::string ipAddress = "127.0.0.1";			// IP Address of the server
	int port = 54000;						// Listening port # on the server

	//create send data
	std::cout << "Socket start sending!\n";

	string traffic = changetraffic(modorder);
	string length = std::to_string(traffic.length());
	string header = length + ":";
	string package = header + traffic;
	const char* Input = package.c_str();

	std::string str = traffic;

	// Do-while loop to send data
	char buf[4096];

	do
	{
		// get text
		int size = strlen(Input);
		if (size > 0)		// Make sure there is input
		{
			//std::cout << "size correct ";
			// Send the text
			int sendResult = send(ClientSocket, Input, size, 0);

			if (sendResult != SOCKET_ERROR)
				//if (sendResult == -1)
			{
				//std::cout << "Socket no result error!\n";					
				// Wait for response
				ZeroMemory(buf, 4096);
				//int bytesReceived = recv(sock, buf, 4096, 0);
				int bytesReceived = recv(ClientSocket, buf, 4096, 0);
				if (bytesReceived > 0)
				{
					// Echo response to console
					std::cout << "Socket buffer!\n";
					std::cout << buf;
					receivedbuffer = buf;
				}
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

string controller::receiver(int modorder, SOCKET ClientSocket)
{
	std::cout << "Socket start receiving!\n";
	//receive
	char buffer[1024];
	string output;
	//sleep
	//Sleep(500);
	output = receivedbuffer;
	std::cout << "Simulator data received!\n";
	return output;
}

//https://github.com/nlohmann/json#examples
//json parsing and deserialization
int controller::parsejson(string sensor, int order) {
	
	//checking string length
	int sensorlength = sensor.length();
	//if string is complete
	if (sensorlength >451) {
		//remove header
		string substr = sensor.substr(4);
		//parsing json
		json j = substr;
		auto j2 = json::parse(substr);
		//fetching lanes
		//car
		int A11 = j2["A1-1"];
		int A12 = j2["A1-2"];
		int A13 = j2["A1-3"];
		int A21 = j2["A2-1"];
		int A22 = j2["A2-2"];
		int A23 = j2["A2-3"];
		int A24 = j2["A2-4"];
		int A31 = j2["A3-1"];
		int A32 = j2["A3-2"];
		int A33 = j2["A3-3"];
		int A34 = j2["A3-4"];
		int A41 = j2["A4-1"];
		int A42 = j2["A4-2"];
		int A43 = j2["A4-3"];
		int A44 = j2["A4-4"];
		int A51 = j2["A5-1"];
		int A52 = j2["A5-2"];
		int A53 = j2["A5-3"];
		int A54 = j2["A5-4"];
		int A61 = j2["A6-1"];
		int A62 = j2["A6-2"];
		int A63 = j2["A6-3"];
		int A64 = j2["A6-4"];
		//bus
		int B11 = j2["B1-1"];
		int B12 = j2["B1-2"];
		int B41 = j2["B4-1"];
		//bike
		int F11 = j2["F1-1"];
		int F12 = j2["F1-2"];
		int F21 = j2["F2-1"];
		int F22 = j2["F2-2"];
		int F41 = j2["F4-1"];
		int F42 = j2["F4-2"];
		int F51 = j2["F5-1"];
		int F52 = j2["F5-2"];
		//foot
		int V11 = j2["V1-1"];
		int V12 = j2["V1-2"];
		int V13 = j2["V1-3"];
		int V14 = j2["V1-4"];
		int V21 = j2["V2-1"];
		int V22 = j2["V2-2"];
		int V23 = j2["V2-3"];
		int V24 = j2["V2-4"];
		int V41 = j2["V4-1"];
		int V42 = j2["V4-2"];
		int V43 = j2["V4-3"];
		int V44 = j2["V4-4"];
		int V51 = j2["V5-1"];
		int V52 = j2["V5-2"];
		int V53 = j2["V5-3"];
		int V54 = j2["V5-4"];

		//phase calculation
		int increment = 1;
		while (increment <7) {
			//traffic lights sensor logic	
			//rechtdoor noord - zuid, oost - west bus
			if (order == 1 && (A11 == 0 || A12 == 0 || B11 == 0 || B12 == 0 || A21 == 0 || A22 == 0 || B41 == 0 || A54 == 0 || A61 == 0 || A62 == 0)) {
				order = order + 1;
				increment++;
			}
			//rechtdoor en rechtsaf noord - zuid auto 
			else if (order == 2 && (A11 == 0 || A12 == 0 || A13 == 0 || A21 == 0 || A22 == 0 || A41 == 0 || A42 == 0 || A43 == 0 || A44 == 0 || A53 == 0 || A54 == 0)) {
				order = order + 1;
				increment++;
			}
			//rechtdoor en rechtsaf oost - west auto
			else if (order == 3 && (A21 == 0 || A22 == 0 || A23 == 0 || A24 == 0 || A33 == 0 || A34 == 0 || A51 == 0 || A52 == 0 || A53 == 0 || A54 == 0 || A61 == 0 || A62 == 0)) {
				order = order + 1;
				increment++;
			}
			//linksaf noord - west en oost - zuid auto
			else if (order == 4 && (A11 == 0 || A12 == 0 || A31 == 0 || A32 == 0 || A33 == 0 || A34 == 0 || A43 == 0 || A44 == 0 || A61 == 0 || A62 == 0 || A63 == 0 || A64 == 0)) {
				order = order + 1;
				increment++;
			}
			//linksaf noord - oost en zuid - west auto
			else if (order == 5 && (A11 == 0 || A12 == 0 || A13 == 0 || A41 == 0 || A42 == 0 || A43 == 0 || A44 == 0 || A53 == 0 || A54 == 0)) {
				order = order + 1;
				increment++;
			}
			//fietsverkeer en voetgangersverkeer 
			else if (order == 6 && (F11 == 0 || F12 == 0 || V11 == 0 || V12 == 0 || V13 == 0 || V14 == 0 || F21 == 0 || F22 == 0 || V21 == 0 || V22 == 0 || V23 == 0 || V24 == 0 || F41 == 0 || F42 == 0 || V41 == 0 || V42 == 0 || V43 == 0 || V44 == 0 || F51 == 0 || F52 == 0 || V51 == 0 || V52 == 0 || V53 == 0 || V54 == 0)) {
				order = 1;
				increment++;
			}
			else {
				increment = 7;
			}
		}


	}
	//else do nothing


	return order;

}

/*

string controller::Replace(string str, const string& oldStr, const string& newStr)
{

	size_t index = str.find(oldStr);
	while (index != str.npos)
	{
		str = str.substr(0, index) +
			newStr + str.substr(index + oldStr.size());
		index = str.find(oldStr, index + newStr.size());
	}
	return str;
}
*/



/*
//https://github.com/nlohmann/json
//
//https://bitbucket.org/sloankelly/youtube-source-repository/src/master/cpp/networking/BarebonesClient/Barebones_Client/main.cpp
//https://bitbucket.org/sloankelly/youtube-source-repository/src/master/cpp/networking/BarebonesClient/Barebones_Client/main.cpp
//
//
//https://www.space-research.org/blog/lib_netsockets.html
//https://github.com/pedro-vicente/lib_netsockets
//https://github.com/pedro-vicente/lib_netsockets/blob/master/src/socket.hh
//https://github.com/pedro-vicente/lib_netsockets/blob/master/src/socket.cc
//https://github.com/pedro-vicente/lib_netsockets/blob/master/examples/json_message.hh
//https://github.com/pedro-vicente/lib_netsockets/blob/master/examples/json_client.cc
//https://github.com/akheron/jansson
//https://github.com/ebshimizu/socket.io-clientpp
//https://www.youtube.com/watch?v=WDn-htpBlnU
//https://www.youtube.com/watch?v=0Zr_0Jy8mWE
//https://www.geeksforgeeks.org/socket-programming-cc/
//https://www.bogotobogo.com/cplusplus/sockets_server_client.php
//
*/