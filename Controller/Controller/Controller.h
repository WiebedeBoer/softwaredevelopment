#ifndef Controller
#define Controller

#include <string.h>
#include <iostream>
#include <sstream>
#include <stdio.h> 
using std::string;

class controller
{
public:
	int setlight();
	int trafficorder();
	int sendlight();

	string changetraffic(int order);

	string Replace(string str, const string& oldStr, const string& newStr);

	void socketserver(int modorder);
	//string socketclient();
	int parsejson(string sensor, int order);
	string receiver();
};

#endif