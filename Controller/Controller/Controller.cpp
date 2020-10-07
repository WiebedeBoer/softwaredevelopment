// Controller.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include "trafficlight.h"
#include <iostream>
#include <sstream>
#include <stdio.h> 
#include <string.h> 
//#include <sys/socket.h> 
//#include <arpa/inet.h> 
//#include <unistd.h> 
#include "socket.h"
#include "json_message.h"
//rapid json library
#include "rapidjson\document.h"
#include "rapidjson\writer.h"
#include "rapidjson\stringbuffer.h"

using namespace rapidjson;


int fromsim[4];
int tosim[4];

int main()
{
    std::cout << "Hello World!\n"; 
	const char* json = "{ \"1\": {\"A1-1\": 1,\"A1-2\" : 1} }";
	controller controller;
	//controller.fetchjson(json);

	const rapidjson::Document json_value = controller.fetchjson(json);
	assert(json_value.IsArray());
	controller.sendjson(json_value);
}

class controller {

public:

	controller()
	{
			int order;
			std::string traffic;
			std::string changetraffic(int order);
	}

	void fetchlight() {

	}

	void checklight() {

	}

	int setlight() {
		int light;
		trafficlight trafficlight;
		light = trafficlight.changecolor(light);
		return light;
	}

	int trafficorder() {
		int trafficorder = 1;
		return trafficorder;
	}

	//which lights on green
	std::string changetraffic(int order) {

		std::string traffic;
		//rechtdoor noord - zuid bus
		if (order == 1)
		{
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		//rechtdoor en rechtsaf noord - zuid auto
		else if (order == 2) {
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		//rechtdoor en rechtsaf oost - west auto
		else if (order == 3) {
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		//linksaf noord - west en oost - zuid auto
		else if (order == 4) {
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		//linksaf noord - oost en zuid - west auto
		else if (order == 5) {
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		//fietsverkeer
		else if (order == 6) {
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		//voetgangersverkeer
		else if (order == 7) {
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		//default
		else {
			trafficnorthsouthbus northsouthbus;
			traffic = northsouthbus.traffic;
		}
		return traffic;
	}

	static rapidjson::Document fetchjson(const char* json) {
		Document document;
		
		document.Parse<0>(json);

		//convert document to string

		StringBuffer strbuf;
		strbuf.Clear();

		Writer<StringBuffer> writer(strbuf);
		document.Accept(writer);
	}

	void sendjson(const rapidjson::Value& value) {
		rapidjson::StringBuffer buffer;
		rapidjson::Writer<rapidjson::StringBuffer> writer(buffer);
		value.Accept(writer);

		std::cout << buffer.GetString() << std::endl;
	}

	class trafficnorthsouthbus {
	public: 
		std::string traffic = "{\"1\":{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1},\"2\": {\"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0},\"3\": {\"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0},\"4\": {\"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0},\"5\": {\"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0},\"6\": {\"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}}";
	};



	/*
	//https://www.space-research.org/blog/lib_netsockets.html
	//
	//https://github.com/pedro-vicente/lib_netsockets
	//
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/src/socket.hh
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/src/socket.cc
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/examples/json_message.hh
	//https://github.com/pedro-vicente/lib_netsockets/blob/master/examples/json_client.cc
	//
	//https://github.com/akheron/jansson
	//https://github.com/nlohmann/json
	//
	//
	//https://answers.ros.org/question/260095/how-to-send-data-in-json-format-using-service-client-in-c-code/
	//https://rapidjson.org/md_doc_tutorial.html
	//
	//https://github.com/ebshimizu/socket.io-clientpp
	*/

	/*
	void fetchjson() {
		//tcp client
		tcp_client_t client("127.0.0.1", 2000);
		//request
		json_t *request = json_object();
		json_object_set_new(request, "start_year", json_integer(2016));
		//open client
		client.open();
		client.write(request);
		//connect in port
		tcp_server_t server(2000);
		while (true)
		{
			socket_t socket = server.accept_client();
			handle_client(socket);
			socket.close();
		}
		//close sserver
		server.close();
		//receive response
		json_t *response = client.read();
		json_t *json_obj;
		json_obj = json_object_get(response, "next_year");
		json_int_t next_year = json_integer_value(json_obj);
		std::cout << "client received: " << std::endl;
		std::cout << "next_year: " << next_year << std::endl;
		client.close();
	}
	*/

	/*
	void handle_client(socket_t& socket_client)
	{
		json_t *response = NULL;
		json_t *request = socket_client.read();

		//get dates
		json_t *json_obj;
		json_obj = json_object_get(request, "start_year");
		json_int_t start_year = json_integer_value(json_obj);
		std::cout << "server received: " << std::endl;
		std::cout << "start_year: " << start_year << std::endl;

		//do response
		response = json_object();
		json_object_set_new(response, "next_year", json_integer(start_year + 1));
		socket_client.write(response);
	}
	*/



};



// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
