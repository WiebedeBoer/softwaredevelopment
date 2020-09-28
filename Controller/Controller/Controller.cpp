// Controller.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include "trafficlight.h"
#include <iostream>
#include <sstream>

int fromsim[4];
int tosim[4];

int main()
{
    std::cout << "Hello World!\n"; 
}

void fetchlight() {

}

void checklight() {

}

void setlight() {
	int light;
	trafficlight trafficlight;
	light = trafficlight.changecolor(light);
}

//which lights on green
void changetraffic(int order) {
	int order = order;
	//rechtdoor noord - zuid bus
	if (order ==1)
	{
	}
	//rechtdoor en rechtsaf noord - zuid auto
	else if (order ==2) {

	}
	//rechtdoor en rechtsaf oost - west auto
	else if (order == 3) {

	}
	//linksaf noord - west en oost - zuid auto
	else if (order == 4) {

	}
	//linksaf noord - oost en zuid - west auto
	else if (order == 5) {

	}
	//fietsverkeer
	else if (order == 6) {

	}
	//voetgangersverkeer
	else if (order == 7) {

	}
	//default
	else {

	}
}

void fetchjson() {

}

void sendjson() {

}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
