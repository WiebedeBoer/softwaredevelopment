#include <string.h>
#include <iostream>
#include <sstream>
#include <stdio.h> 
#include "jsonreader.h"
//#include "pch.h"
using std::string;

//class controller : controller
class controller
{
public:
	int setlight();
	int trafficorder();
	int sendlight();
	//int sendpackage(std::string hello);
	string changetraffic(int order);
	//static rapidjson::Document fetchjson(const char* json);
	//void sendjson(const rapidjson::Value& value);
	string Replace(string str, const string& oldStr, const string& newStr);
	void socketserver(string userInput);
	string socketclient(string userInput);
};
