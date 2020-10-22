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
//#include "jsonreader.h"

#include <sys/types.h>
//#include <sys/socket.h>
//#include <netinet/in.h>
//#include <arpa/inet.h>
//#include <unistd.h>

//old socket
//#include <sys/socket.h> 
//#include <arpa/inet.h> 
//#include <unistd.h> 
//#include <netinet/in.h> 
//#include "socket.h"
//#include "json_message.h"

//rapid json library
//#include "rapidjson\document.h"
//#include "rapidjson\writer.h"
//#include "rapidjson\stringbuffer.h"
//using namespace rapidjson;


int main()
{
    std::cout << "Hello World!\n"; 
	//string json = "{ \"1\": {\"A1-1\": 1,\"A1-2\" : 1} }";
	
	controller controller;
	//json = controller.Replace(json, "\\\"", "\"");
	//Pakal::JsonReader JsonReader;
	//controller.fetchjson(json);

	//JsonReader.parse_element(object & object, Element * element);
	//JsonReader.read(json,"A1-1",1);
	//JsonReader.read("jason_controller.json", "A1-1", json);

	//fetching json
	//const rapidjson::Document json_value = controller.fetchjson(json);
	//assert(json_value.IsArray());
	//controller.sendjson(json_value);

	//sending packages of string json
	controller.sendlight();
}

	/*
	controller()
	{
			//int order;
			//string traffic;
			//std::string changetraffic(int order);
	}
	*/
	
	/*
	void fetchlight() {

	}

	void checklight() {

	}
	*/

	int controller::sendlight() {
		int order = 1;
		const char* traffic;
		const char* header;
		const char* package;
		int modorder;

		//timing
		double time_counter = 0;
		clock_t this_time = clock();
		clock_t last_time = this_time;
		const int NUM_SECONDS = 4;	

			//run clock
			while (true)
			{
				this_time = clock();
				time_counter += (double)(this_time - last_time);
				last_time = this_time;

				if (time_counter > (double)(NUM_SECONDS * CLOCKS_PER_SEC))
				{
					time_counter -= (double)(NUM_SECONDS * CLOCKS_PER_SEC);
					//int lightssend = sendpackage(package); //package every 4 seconds

					modorder = (order % 8) + 1;
					//string traffic = changetraffic(modorder);
					//string header = "25:{\"type\": \"A1-A\",\"state\":0}";
					//string package = header + traffic;
					string package = changetraffic(modorder);

					const char* input = package.c_str();
					//socketserver(package);
					socketserver(input); //package every 4 seconds
				}


				//send package


				//continuous order
				order++;

			}

		int lightssend = 1;
		return lightssend;
	}

	int controller::setlight() {
		int light = 0;
		//trafficlight trafficlight;
		//light = trafficlight.changecolor(light);
		return light;
	}

	int controller::trafficorder() {
		int trafficorder = 1;
		return trafficorder;
	}

	//which lights on green
	string controller::changetraffic(int order) {

		string traffic;
		//rechtdoor noord - zuid bus
		if (order == 1)
		{
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//rechtdoor oost - west bus
		else if (order == 2)
		{
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//rechtdoor en rechtsaf noord - zuid auto
		else if (order == 3) {
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//rechtdoor en rechtsaf oost - west auto
		else if (order == 4) {
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//linksaf noord - west en oost - zuid auto
		else if (order == 5) {
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//linksaf noord - oost en zuid - west auto
		else if (order == 6) {
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//fietsverkeer
		else if (order == 7) {
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//voetgangersverkeer
		else if (order == 8) {
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		//default
		else {
			traffic = "{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1, \"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0, \"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0, \"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0, \"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0, \"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}";
		}
		traffic = Replace(traffic, "\\\"", "\"");
		return traffic;
	}

	string controller::Replace(string str,const string& oldStr,const string& newStr)
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

	/*
	static rapidjson::Document fetchjson(const char* json) {
		Document document;
		
		document.Parse<0>(json);

		//convert document to string

		StringBuffer strbuf;
		strbuf.Clear();

		Writer<StringBuffer> writer(strbuf);
		document.Accept(writer);
	}
	*/

	/*
	void sendjson(const rapidjson::Value& value) {
		rapidjson::StringBuffer buffer;
		rapidjson::Writer<rapidjson::StringBuffer> writer(buffer);
		value.Accept(writer);

		std::cout << buffer.GetString() << std::endl;
	}
	*/



	/*
	//https://www.space-research.org/blog/lib_netsockets.html
	//
	//https://github.com/pedro-vicente/lib_netsockets
	//
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/src/socket.hh
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/src/socket.cc
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/examples/json_message.hh
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/examples/json_client.cc
	//
	//https://github.com/akheron/jansson
	//https://github.com/nlohmann/json
	//
	//
	//https://answers.ros.org/question/260095/how-to-send-data-in-json-format-using-service-client-in-c-code/
	//https://rapidjson.org/md_doc_tutorial.html
	//
	//https://github.com/ebshimizu/socket.io-clientpp
	//
	//https://www.youtube.com/watch?v=WDn-htpBlnU
	//https://www.youtube.com/watch?v=0Zr_0Jy8mWE
	//
	//https://www.geeksforgeeks.org/socket-programming-cc/
	//https://www.bogotobogo.com/cplusplus/sockets_server_client.php
	*/

	void controller::socketserver(const char* Input)
	{
		std::string ipAddress = "127.0.0.1";			// IP Address of the server
		int port = 54000;						// Listening port # on the server

		std::string str = Input;

		// Initialize WinSock
		WSAData data;
		WORD ver = MAKEWORD(2, 2);
		int wsResult = WSAStartup(ver, &data);
		if (wsResult != 0)
		{
			std::cout << "Socket not init!\n";
			return;
		}

		// Create socket
		//SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);
		SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
		if (sock == INVALID_SOCKET)
		{
			std::cout << "Socket not valid!\n";
			WSACleanup();
			return;
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
			return;
		}

		//listen
		int iResult;
		iResult = listen(sock, SOMAXCONN);
		if (iResult == SOCKET_ERROR) {
			printf("listen failed with error: %d\n", WSAGetLastError());
			closesocket(sock);
			WSACleanup();
			return;
		}

		// Wait for a connection
		int clientSize = sizeof(serverInf);

		// Accept a client socket
		SOCKET ClientSocket = accept(sock, (sockaddr*)&serverInf, &clientSize);
		if (ClientSocket == INVALID_SOCKET) {
			printf("accept failed with error: %d\n", WSAGetLastError());
			closesocket(sock);
			WSACleanup();
			return;
		}	

		// Do-while loop to send and receive data
		char buf[4096];
		//std::string userInput;

		do
		{
			// get text
			int size = strlen(Input);
			if (size > 0)		// Make sure the user has typed in something
			{
				//std::cout << "size correct ";
				// Send the text
				int sendResult = send(ClientSocket, Input, size, 0);

				if (sendResult != SOCKET_ERROR)
				//if (sendResult == -1)
				{
					//std::cout << "Socket no result error!\n";
					//std::cout << SOCKET_ERROR;
					
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

		} while (str.size() > 0);

		// Gracefully close down everything
		//closesocket(sock);
		closesocket(ClientSocket);
		WSACleanup();
	}	