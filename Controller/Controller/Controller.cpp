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
	int binded = 0;

	std::cout << "Startup sending!\n";

	//timing
	double time_counter = 0;
	clock_t this_time = clock();
	clock_t last_time = this_time;
	const int NUM_SECONDS = 4;

	//setup socket
	SOCKET sock = socketSetup();

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

			//get simulator sensor data
			//string sensor = socketclient();
			//string sensor = "{'A1-1':1,'A1-2':1,'A1-3':0,'B1-1':1,'B1-2':1,'F1-1':0,'F1-2':0,'V1-1':0,'V1-2':0,'V1-3':0,'V1-4':0,'A2-1':1,'A2-2':1,'A2-3':0,'A2-4':0,'F2-1':0,'F2-2':0,'V2-1':0,'V2-2':0,'V2-3':0,'V2-4':0,'A3-1':0,'A3-2':0,'A3-3':0,'A3-4':0,'A4-1':0,'A4-2':0,'A4-3':0,'A4-4':0,'B4-1':1,'F4-1':0,'F4-2':0,'V4-1':0,'V4-2':0,'V4-3':0,'V4-4':0,'A5-1':0,'A5-2':0,'A5-3':1,'A5-4':1,'F5-1':0,'F5-2':0,'V5-1':0,'V5-2':0,'V5-3':0,'V5-4':0,'A6-1':1,'A6-2':1,'A6-3':0,'A6-4':0}";
			//string sensor = "{\"A1-1\":1}";
			//if sensor data
			/*
			if (sensor !="error") {
				//dserialize json
				modorder = parsejson(sensor, order);
			}
			//else no sensor data
			else {
				//current order
				modorder = (order % 6) + 1;
			}
			*/
			std::cout << "Sending phase \n";

			modorder = (order % 6) + 1;
			std::cout << modorder;

			socketServer(modorder, sock);

			std::cout << "phase Send \n";

			//traffic lights
			//string traffic = changetraffic(modorder);
			//string length = std::to_string(traffic.length());
			//string header = length + ":";
			//string package = header + traffic;				
			//const char* input = package.c_str();
			//socketserver(input); //package every 4 seconds
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

	std::cout << "Socket start receiving!\n";

	int binder;

	/*
	//receive
	char buffer[4096];
	string output;

	// Wait for response
	ZeroMemory(buffer, 4096);
	int bytesReceived = recv(ClientSocket, buffer, 4096, 0);
	if (bytesReceived > 0)
	{
		// Echo response to console
		std::cout << "SERVER> " << string(buffer, 0, bytesReceived);
		output = output + buffer;
	}
	else
	{
		output = "error";
	}

	std::string str = output;
	int currentorder;

	if (output != "error") {
		//dserialize json
		//currentorder = parsejson(output, modorder);
		currentorder = (modorder % 6) + 1;
	}
	//else no sensor data
	else {
		//current order
		currentorder = (modorder % 6) + 1;
	}

	*/

	//create send data
	std::cout << "Socket start sending!\n";

	//string traffic = changetraffic(currentorder);



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
	std::cout << "Socket closed!\n";
}

int controller::setlight() {
	int light = 0;
	return light;
}

int controller::trafficorder() {
	int trafficorder = 1;
	return trafficorder;
}


string controller::receiver()
{
	string receive = "test";
	return receive;
}

/*
string controller::socketclient()
{
	string ipAddress = "127.0.0.1";			// IP Address of the server
	int port = 54000;						// Listening port # on the server

	//output string
	string output;

	// Initialize WinSock
	WSAData data;
	WORD ver = MAKEWORD(2, 2);
	int wsResult = WSAStartup(ver, &data);
	if (wsResult != 0)
	{
		std::cout << "Socket not init!\n";
		output = "error";
		return output;
	}

	// Create socket
	SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);
	if (sock == INVALID_SOCKET)
	{
		std::cout << "Can't create socket, Err #" << WSAGetLastError();
		WSACleanup();
		output = "error";
		return output;
	}

	// Fill in a hint structure
	sockaddr_in hint;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(port);
	inet_pton(AF_INET, ipAddress.c_str(), &hint.sin_addr);

	// Connect to server
	int connResult = connect(sock, (sockaddr*)&hint, sizeof(hint));
	if (connResult == SOCKET_ERROR)
	{
		std::cout << "Can't connect to server, Err #" << WSAGetLastError();
		closesocket(sock);
		WSACleanup();
		output = "error";
		return output;
	}


	//write

	//read

	// Do-while loop to send and receive data
	char buf[4096];
	//byte buffer[];
	//byte buffer = new byte[1024];
	//byte buf[4096];
	string header = "0";
	int i;

	while (connResult > 0)
	{

		// Wait for response
		ZeroMemory(buf, 4096);
		int bytesReceived = recv(sock, buf, 4096, 0);
		if (bytesReceived > 0)
		{
			// Echo response to console
			std::cout << "SERVER> " << string(buf, 0, bytesReceived);
			output = output + buf;
		}

	}

	// Gracefully close down everything
	closesocket(sock);
	WSACleanup();

	return output;
}
*/

//https://github.com/nlohmann/json#examples
//json parsing and deserialization
int controller::parsejson(string sensor, int order) {
	json j = sensor;
	int modulusorder;

	auto j2 = json::parse(sensor);

	int currentorder = (order % 6) + 1;

	if (currentorder == 1 && j2 == 0) {
		modulusorder = order + 1;
		return modulusorder;
	}
	else if (currentorder == 2 && j2 == 0) {
		modulusorder = order + 1;
		return modulusorder;
	}
	else if (currentorder == 3 && j2 == 0) {
		modulusorder = order + 1;
		return modulusorder;
	}
	else if (currentorder == 4 && j2 == 0) {
		modulusorder = order + 1;
		return modulusorder;
	}
	else if (currentorder == 5 && j2 == 0) {
		modulusorder = order + 1;
		return modulusorder;
	}
	else if (currentorder == 6 && j2 == 0) {
		modulusorder = order + 1;
		return modulusorder;
	}

}


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