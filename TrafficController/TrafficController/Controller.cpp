#include "controller.hpp"
#include <nlohmann/json.hpp> 
using json = nlohmann::json;
using u_short = unsigned short;
/*
*Which lights on green or red json string.
*/
std::string Controller::changetraffic(int order) {

	std::string traffic;
	//Straight north - south, east - west bus. 
	if (order == 1)
	{
		traffic = "{\"A1-1\":1,\"A1-2\":1,\"A1-3\":0,\"B1-1\":1,\"B1-2\":1,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":1,\"A2-2\":1,\"A2-3\":0,\"A2-4\":0,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":0,\"A3-2\":0,\"A3-3\":0,\"A3-4\":0,\"A4-1\":0,\"A4-2\":0,\"A4-3\":0,\"A4-4\":0,\"B4-1\":1,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":0,\"A5-2\":0,\"A5-3\":1,\"A5-4\":1,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":1,\"A6-2\":1,\"A6-3\":0,\"A6-4\":0}";
	}
	//Straight and right north-south car.
	else if (order == 2) {
		traffic = "{\"A1-1\":1,\"A1-2\":1,\"A1-3\":1,\"B1-1\":0,\"B1-2\":0,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":1,\"A2-2\":1,\"A2-3\":0,\"A2-4\":0,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":0,\"A3-2\":0,\"A3-3\":0,\"A3-4\":0,\"A4-1\":1,\"A4-2\":1,\"A4-3\":1,\"A4-4\":1,\"B4-1\":0,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":0,\"A5-2\":0,\"A5-3\":1,\"A5-4\":1,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":0,\"A6-2\":0,\"A6-3\":0,\"A6-4\":0}";
	}
	//Straight and right east-west car.
	else if (order == 3) {
		traffic = "{\"A1-1\":0,\"A1-2\":0,\"A1-3\":0,\"B1-1\":0,\"B1-2\":0,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":1,\"A2-2\":1,\"A2-3\":1,\"A2-4\":1,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":0,\"A3-2\":0,\"A3-3\":1,\"A3-4\":1,\"A4-1\":0,\"A4-2\":0,\"A4-3\":0,\"A4-4\":0,\"B4-1\":0,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":1,\"A5-2\":1,\"A5-3\":1,\"A5-4\":1,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":1,\"A6-2\":1,\"A6-3\":0,\"A6-4\":0}";
	}
	//Left north-west en east-south car.
	else if (order == 4) {
		traffic = "{\"A1-1\":1,\"A1-2\":1,\"A1-3\":0,\"B1-1\":0,\"B1-2\":0,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":0,\"A2-2\":0,\"A2-3\":0,\"A2-4\":0,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":1,\"A3-2\":1,\"A3-3\":1,\"A3-4\":1,\"A4-1\":0,\"A4-2\":0,\"A4-3\":1,\"A4-4\":1,\"B4-1\":0,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":0,\"A5-2\":0,\"A5-3\":0,\"A5-4\":0,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":1,\"A6-2\":1,\"A6-3\":1,\"A6-4\":1}";
	}
	//Left north-east en south-west car. 
	else if (order == 5) {
		traffic = "{\"A1-1\":1,\"A1-2\":1,\"A1-3\":1,\"B1-1\":0,\"B1-2\":0,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":0,\"A2-2\":0,\"A2-3\":0,\"A2-4\":0,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":0,\"A3-2\":0,\"A3-3\":0,\"A3-4\":0,\"A4-1\":1,\"A4-2\":1,\"A4-3\":1,\"A4-4\":1,\"B4-1\":0,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":0,\"A5-2\":0,\"A5-3\":1,\"A5-4\":1,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":0,\"A6-2\":0,\"A6-3\":0,\"A6-4\":0}";
	}
	//Bike and foot traffic.
	else if (order == 6) {
		traffic = "{\"A1-1\":0,\"A1-2\":0,\"A1-3\":0,\"B1-1\":0,\"B1-2\":0,\"F1-1\":1,\"F1-2\":1,\"V1-1\":1,\"V1-2\":1,\"V1-3\":1,\"V1-4\":1,\"A2-1\":0,\"A2-2\":0,\"A2-3\":0,\"A2-4\":0,\"F2-1\":1,\"F2-2\":1,\"V2-1\":1,\"V2-2\":1,\"V2-3\":1,\"V2-4\":1,\"A3-1\":0,\"A3-2\":0,\"A3-3\":0,\"A3-4\":0,\"A4-1\":0,\"A4-2\":0,\"A4-3\":0,\"A4-4\":0,\"B4-1\":0,\"F4-1\":1,\"F4-2\":1,\"V4-1\":1,\"V4-2\":1,\"V4-3\":1,\"V4-4\":1,\"A5-1\":0,\"A5-2\":0,\"A5-3\":0,\"A5-4\":0,\"F5-1\":1,\"F5-2\":1,\"V5-1\":1,\"V5-2\":1,\"V5-3\":1,\"V5-4\":1,\"A6-1\":0,\"A6-2\":0,\"A6-3\":0,\"A6-4\":0}";
	}
	//Default all orange.
	else {
		traffic = "{\"A1-1\":2,\"A1-2\":2,\"A1-3\":2,\"B1-1\":2,\"B1-2\":2,\"F1-1\":2,\"F1-2\":2,\"V1-1\":2,\"V1-2\":2,\"V1-3\":2,\"V1-4\":2,\"A2-1\":2,\"A2-2\":2,\"A2-3\":2,\"A2-4\":2,\"F2-1\":2,\"F2-2\":2,\"V2-1\":2,\"V2-2\":2,\"V2-3\":2,\"V2-4\":2,\"A3-1\":2,\"A3-2\":2,\"A3-3\":2,\"A3-4\":2,\"A4-1\":2,\"A4-2\":2,\"A4-3\":2,\"A4-4\":2,\"B4-1\":2,\"F4-1\":2,\"F4-2\":2,\"V4-1\":2,\"V4-2\":2,\"V4-3\":2,\"V4-4\":2,\"A5-1\":2,\"A5-2\":2,\"A5-3\":2,\"A5-4\":2,\"F5-1\":2,\"F5-2\":2,\"V5-1\":2,\"V5-2\":2,\"V5-3\":2,\"V5-4\":2,\"A6-1\":2,\"A6-2\":2,\"A6-3\":2,\"A6-4\":2}";
	}
	//Echo traffic to console.
	std::cout << "Sending \n";
	std::cout << traffic;
	std::cout << "\n";
	return traffic;

}

/*
* Clearance timer string is returned.
*/
std::string Controller::clearancetime() {
	std::string traffic = "{\"A1-1\":0,\"A1-2\":0,\"A1-3\":0,\"B1-1\":0,\"B1-2\":0,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":0,\"A2-2\":0,\"A2-3\":0,\"A2-4\":0,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":0,\"A3-2\":0,\"A3-3\":0,\"A3-4\":0,\"A4-1\":0,\"A4-2\":0,\"A4-3\":0,\"A4-4\":0,\"B4-1\":0,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":0,\"A5-2\":0,\"A5-3\":0,\"A5-4\":0,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":0,\"A6-2\":0,\"A6-3\":0,\"A6-4\":0}";
	std::cout << "Clearance \n";
	return traffic;
}


/*
*Json parsing and deserialization and phase number.
*/
int Controller::parsejson(int order) {

	//Checking string length.
	int sensorlength = buffer.length();
	//See if string is complete.
	if (sensorlength > 451 && sensorlength < 700) {
		std::cout << "Parsing!\n";
		std::cout << "Full simulator package received!\n";
		//Remove header.
		std::string substr = buffer.substr(4);
		//Remove spaces.
		std::string::iterator end_pos = std::remove(substr.begin(), substr.end(), ' ');
		substr.erase(end_pos, substr.end());
		//Preparing json string.
		json j = substr;
		
		static bool jsonvalid;
			//See if json is valid.
		try
		{
			jsonvalid = json::accept(substr);
		}
		catch (int e)
		{
			std::cout << "An exception occurred. Exception json incorrect. " << e << "\n";
			jsonvalid = false;
		}


			if (jsonvalid ==true) {
				std::cout << "Json is valid.\n";
				//Parsing json string.
				auto j2 = json::parse(substr);
				//Fetching lanes.
				//Car lanes.
				int A11 = j2["A1-1"];
				int A12 = j2["A1-2"];
				int A13 = j2["A1-3"];
				int A21 = j2["A2-1"];
				int A22 = j2["A2-2"];
				int A23 = j2["A2-3"];
				int A24 = j2["A2-4"];
				int A31 = j2["A3-1"];
				int A32 = j2["A3-2"];
				int A33 = j2["A3-3"];
				int A34 = j2["A3-4"];
				int A41 = j2["A4-1"];
				int A42 = j2["A4-2"];
				int A43 = j2["A4-3"];
				int A44 = j2["A4-4"];
				int A51 = j2["A5-1"];
				int A52 = j2["A5-2"];
				int A53 = j2["A5-3"];
				int A54 = j2["A5-4"];
				int A61 = j2["A6-1"];
				int A62 = j2["A6-2"];
				int A63 = j2["A6-3"];
				int A64 = j2["A6-4"];
				//Bus lanes.
				int B11 = j2["B1-1"];
				int B12 = j2["B1-2"];
				int B41 = j2["B4-1"];
				//Bike lanes.
				int F11 = j2["F1-1"];
				int F12 = j2["F1-2"];
				int F21 = j2["F2-1"];
				int F22 = j2["F2-2"];
				int F41 = j2["F4-1"];
				int F42 = j2["F4-2"];
				int F51 = j2["F5-1"];
				int F52 = j2["F5-2"];
				//Foot lanes.
				int V11 = j2["V1-1"];
				int V12 = j2["V1-2"];
				int V13 = j2["V1-3"];
				int V14 = j2["V1-4"];
				int V21 = j2["V2-1"];
				int V22 = j2["V2-2"];
				int V23 = j2["V2-3"];
				int V24 = j2["V2-4"];
				int V41 = j2["V4-1"];
				int V42 = j2["V4-2"];
				int V43 = j2["V4-3"];
				int V44 = j2["V4-4"];
				int V51 = j2["V5-1"];
				int V52 = j2["V5-2"];
				int V53 = j2["V5-3"];
				int V54 = j2["V5-4"];
				//Phase calculation.
				int increment = 1;
				while (increment < 7) {
					//Traffic lights sensor logic.	
					//Straight north - south, east - west bus.
					if (order == 1 && (A11 == 0 || A12 == 0 || B11 == 0 || B12 == 0 || A21 == 0 || A22 == 0 || B41 == 0 || A54 == 0 || A61 == 0 || A62 == 0)) {
						order = order + 1;
						increment++;
					}
					//Straight and right north-south car.
					else if (order == 2 && (A11 == 0 || A12 == 0 || A13 == 0 || A21 == 0 || A22 == 0 || A41 == 0 || A42 == 0 || A43 == 0 || A44 == 0 || A53 == 0 || A54 == 0)) {
						order = order + 1;
						increment++;
					}
					//Straight and right east-west car.
					else if (order == 3 && (A21 == 0 || A22 == 0 || A23 == 0 || A24 == 0 || A33 == 0 || A34 == 0 || A51 == 0 || A52 == 0 || A53 == 0 || A54 == 0 || A61 == 0 || A62 == 0)) {
						order = order + 1;
						increment++;
					}
					//Left north-west en east-south car.
					else if (order == 4 && (A11 == 0 || A12 == 0 || A31 == 0 || A32 == 0 || A33 == 0 || A34 == 0 || A43 == 0 || A44 == 0 || A61 == 0 || A62 == 0 || A63 == 0 || A64 == 0)) {
						order = order + 1;
						increment++;
					}
					//Left north-east en south-west car. 
					else if (order == 5 && (A11 == 0 || A12 == 0 || A13 == 0 || A41 == 0 || A42 == 0 || A43 == 0 || A44 == 0 || A53 == 0 || A54 == 0)) {
						order = order + 1;
						increment++;
					}
					//Bike and foot traffic.
					else if (order == 6 && (F11 == 0 || F12 == 0 || V11 == 0 || V12 == 0 || V13 == 0 || V14 == 0 || F21 == 0 || F22 == 0 || V21 == 0 || V22 == 0 || V23 == 0 || V24 == 0 || F41 == 0 || F42 == 0 || V41 == 0 || V42 == 0 || V43 == 0 || V44 == 0 || F51 == 0 || F52 == 0 || V51 == 0 || V52 == 0 || V53 == 0 || V54 == 0)) {
						order = 1;
						increment++;
					}
					else {
						increment = 7;
						order = order;
					}
				}

			}
			else {
				std::cout << "Json is invalid!\n";
				order = order;
			}
		
	}
	return order;
}