#pragma once
#include <string>
#include <iostream>
#include <WS2tcpip.h>
#pragma comment(lib, "ws2_32.lib")
#include <sys/types.h>
#include "controller.hpp"

class Sender
{
public:
	Sender(std::shared_ptr<Controller> ptr_s) : ptr_controller(move(ptr_s)) {};
	char receivedbuf[1024] = { 0 };
	std::string bufferdata;
	SOCKET clientSocket = INVALID_SOCKET;
	bool isRunning = false;
	bool isSending = false;
	// IP Address of the server.
	std::string ipAddress = "127.0.0.1";
	// Listening port # on the server.
	int port = 54000;						

	void socketSetup();
	void socketServer(std::string traffic);
	void receiving();
	std::string receiver();

private:
	std::shared_ptr<Controller> ptr_controller;
};
