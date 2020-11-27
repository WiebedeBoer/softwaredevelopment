#pragma once
#include <iostream>
#include <string>

class Controller
{
	//Controller();

public:
	std::string buffer;
	std::string changetraffic(int order);
	int parsejson(int order);
	std::string clearancetime();
private:
	//Sender sendingSocket;
};