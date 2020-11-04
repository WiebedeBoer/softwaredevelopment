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

	void socketserver(const char* userInput);
	string socketclient();
	string parsejson(string sensor);
};

#endif