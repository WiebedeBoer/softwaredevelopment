#include "stdafx.h"
#include "CppUnitTest.h"
#include "../Controller/pch.h"
#include "../Controller/trafficlight.h"
#include <iostream>
#include <sstream>
#include <stdio.h> 
#include <string.h> 

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace ControllerTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(TestMethodSetLight)
		{
			const int light = 1;
			controller contr;
			const int checklight = contr.setlight();
			Assert::AreEqual(light,checklight);
		}

		TEST_METHOD(TestMethodTrafficOrder)
		{

		}

		//https://docs.microsoft.com/en-us/visualstudio/test/writing-unit-tests-for-c-cpp?view=vs-2019
		TEST_METHOD(TestMethodChangeTraffic)
		{
			const std::string traffic = "{\"1\":{\"A1-1\": 0,\"A1-2\": 0,\"A1-3\": 1,\"B1-1\": 1,\"B1-2\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 1},\"2\": {\"A2-1\": 0,\"A2-2\": 0,\"A2-3\": 0,\"A2-4\": 1,\"F1-1\": 0,\"F1-2\": 1,\"V1-1\": 0,\"V1-2\": 1,\"V1-3\": 0,\"V1-4\": 0},\"3\": {\"A3-1\": 1,\"A3-2\": 1,\"A3-3\": 0,\"A3-4\": 0},\"4\": {\"A4-1\": 1,\"A4-2\": 1,\"A4-3\": 0,\"A4-4\": 0,\"B4-1\": 1,\"F4-1\": 0,\"F4-2\": 1,\"V4-1\": 0,\"V4-2\": 0,\"V4-3\": 1,\"V4-4\": 0},\"5\": {\"A5-1\": 1,\"A5-2\": 1,\"A5-3\": 1,\"A5-4\": 1,\"F5-1\": 1,\"F5-2\": 1,\"V5-1\": 0,\"V5-2\": 0,\"V5-3\": 0,\"V5-4\": 0},\"6\": {\"A3-1\": 0,\"A3-2\": 0,\"A3-3\": 0,\"A3-4\": 0}}";
			int order = 1;
			//controller controller(int order);	
			controller controller;
			//std::string checktraffic;
			//controller* contr = new controller;
			//const std::string checktraffic = contr->changetraffic(order);
			const std::string checktraffic = controller.changetraffic(1);
			Assert::AreEqual(traffic, checktraffic);
		}

		TEST_METHOD(TestMethodFetchjson)
		{

		}

		TEST_METHOD(TestMethodSendjson)
		{

		}

	};
}