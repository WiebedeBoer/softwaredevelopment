#include "Controller.hpp"
#include "Sender.hpp"
#include <ctime>
#include <thread>

int main()
{
	std::cout << "Hello World!\n";
	std::shared_ptr<Controller> controller(new Controller);
	std::shared_ptr<Sender> ptr_sender(new Sender(controller));
	//Setup socket.
	ptr_sender->socketSetup();
	//Start phase number.
	int order = 1; 
	int clearance = 1;
	int modorder;
	int clearanceorder;
	//Timing.
	double time_counter = 0;
	clock_t this_time = clock();
	clock_t last_time = this_time;
	const int NUM_SECONDS = 5;
	double total_time = 0;
	//Thread for receiving.
	std::thread t1(&Sender::receiving, ptr_sender);

	//Run clock.
	while (true)
	{
		//Timer of clock.
		this_time = clock();
		time_counter += (double)(this_time - last_time);
		last_time = this_time;
		total_time += time_counter;
		if (time_counter <= NUM_SECONDS * CLOCKS_PER_SEC)
			continue;
		time_counter = 0;
		//Phase calculation.
		modorder = (order % 6) + 1;
		clearanceorder = (clearance % 2);
		std::cout << modorder;
		std::cout << "\n";
		//Parsing from received.
		modorder = controller->parsejson(modorder);
		//Traffic lights to send.
		std::string newtraffic;
		if (clearanceorder ==1) {
			newtraffic = controller->changetraffic(modorder);
			order++;
		}
		else {
			newtraffic = controller->clearancetime();
		}
		
		//Package every 4 seconds.
		ptr_sender->socketServer(newtraffic);
		std::cout << "phase Send \n"; 
		//Continuous order of phase number.
		
		clearance++;
	}
	t1.join();
	controller.reset();
}