#pragma once
#include <string.h>
#include <iostream>
#include <sstream>
#include <stdio.h> 
#include <WS2tcpip.h>
#pragma comment(lib, "ws2_32.lib")
#include <sys/types.h>
#include "Sender.h"
using std::string;
class Sender
{
public:
	//char* receivedbuffer;
	char receivedbuffer[1024];
	SOCKET clientSocket = INVALID_SOCKET;
	bool isRunning = false;
	bool isSending = false;
	std::string ipAddress = "127.0.0.1";			// IP Address of the server
	int port = 54000;						// Listening port # on the server

	void socketSetup();
	void socketServer(string traffic);
	void receiving();
};

