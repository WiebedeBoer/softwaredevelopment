#ifndef Controller
#define Controller

#include <string.h>
#include <iostream>
#include <sstream>
#include <stdio.h> 
#include <WS2tcpip.h>
#pragma comment(lib, "ws2_32.lib")
#include <sys/types.h>
using std::string;

class controller
{
public:

	int sendlight();
	string changetraffic(int order);	
	SOCKET socketSetup();
	void socketServer(int modorder, SOCKET ClientSocket);	
	string receiver(int modorder, SOCKET ClientSocket);
	int parsejson(string sensor, int order);

	//string Replace(string str, const string& oldStr, const string& newStr);
	//string socketclient();
};

#endif