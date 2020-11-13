#ifndef Controller
#define Controller

#include <string.h>
#include <iostream>
#include <sstream>
#include <stdio.h> 
#include <WS2tcpip.h>
#pragma comment(lib, "ws2_32.lib")
#include <sys/types.h>
#include "Sender.h"
using std::string;

class controller
{
private:
	//const char* receivedbuffer;
	Sender sendingSocket;

public:

	int sendlight();
	string changetraffic(int order);
	string receiver(int modorder);
	int parsejson(string sensor, int order);
	//string Replace(string str, const string& oldStr, const string& newStr);
	//string socketclient();
};

#endif