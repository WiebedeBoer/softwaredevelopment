#include "Controller.hpp"
#include "Sender.hpp"
#include <ctime>
#include <thread>

int main()
{
	std::cout << "Hello World!\n";
	std::shared_ptr<Controller> controller(new Controller);
	std::shared_ptr<Sender> ptr_sender(new Sender(controller));
	//controller controller;
	//send traffic lights

	//setup socket
	ptr_sender->socketSetup();

	int order = 1; //start phase
	int modorder;

	//timing
	double time_counter = 0;
	clock_t this_time = clock();
	clock_t last_time = this_time;
	const int NUM_SECONDS = 4;
	double total_time = 0;

	/*
	//first send
	std::cout << "First send!\n";
	string traffic = controller->changetraffic(order);
	sendingSocket.socketServer(traffic);
	*/

	//thread
	std::thread t1(&Sender::receiving, ptr_sender);

	//run clock
	while (true)
	{
		this_time = clock();
		time_counter += (double)(this_time - last_time);
		last_time = this_time;
		total_time += time_counter;
		if (time_counter <= NUM_SECONDS * CLOCKS_PER_SEC)
			continue;
		time_counter = 0;

		//phase calculation
		modorder = (order % 6) + 1;
		std::cout << modorder;
		std::cout << "\n";
		//parsing from received
		modorder = controller->parsejson(modorder);
		//traffic lights send
		std::string newtraffic = controller->changetraffic(modorder);
		ptr_sender->socketServer(newtraffic);
		std::cout << "phase Send \n"; //package every 4 seconds		

		//continuous order
		order++;
	}

	t1.join();

	//controller->sendlight();
	controller.reset();
}