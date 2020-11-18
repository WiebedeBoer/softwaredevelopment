#include "pch.h"
#include "CppUnitTest.h"
#include "../../TrafficController/Controller.hpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace ControllerTest
{
	TEST_CLASS(ControllerTest)
	{
	public:
		
		TEST_METHOD(TestMethodParseJson)
		{
			Controller contr;
			contr.buffer = "{\"A1-1\":1,\"A1-2\":1,\"A1-3\":0,\"B1-1\":1,\"B1-2\":1,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":1,\"A2-2\":1,\"A2-3\":0,\"A2-4\":0,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":0,\"A3-2\":0,\"A3-3\":0,\"A3-4\":0,\"A4-1\":0,\"A4-2\":0,\"A4-3\":0,\"A4-4\":0,\"B4-1\":1,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":0,\"A5-2\":0,\"A5-3\":1,\"A5-4\":1,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":1,\"A6-2\":1,\"A6-3\":0,\"A6-4\":0}";
			const int checkjson = contr.parsejson(1);
			const int testjson = 1;
			Assert::AreEqual(testjson, checkjson);
		}

		TEST_METHOD(TestMethodTrafficOrder)
		{
			Controller contr;			
			const std::string checktraffic = contr.changetraffic(1);
			const std::string testtraffic = "{\"A1-1\":1,\"A1-2\":1,\"A1-3\":0,\"B1-1\":1,\"B1-2\":1,\"F1-1\":0,\"F1-2\":0,\"V1-1\":0,\"V1-2\":0,\"V1-3\":0,\"V1-4\":0,\"A2-1\":1,\"A2-2\":1,\"A2-3\":0,\"A2-4\":0,\"F2-1\":0,\"F2-2\":0,\"V2-1\":0,\"V2-2\":0,\"V2-3\":0,\"V2-4\":0,\"A3-1\":0,\"A3-2\":0,\"A3-3\":0,\"A3-4\":0,\"A4-1\":0,\"A4-2\":0,\"A4-3\":0,\"A4-4\":0,\"B4-1\":1,\"F4-1\":0,\"F4-2\":0,\"V4-1\":0,\"V4-2\":0,\"V4-3\":0,\"V4-4\":0,\"A5-1\":0,\"A5-2\":0,\"A5-3\":1,\"A5-4\":1,\"F5-1\":0,\"F5-2\":0,\"V5-1\":0,\"V5-2\":0,\"V5-3\":0,\"V5-4\":0,\"A6-1\":1,\"A6-2\":1,\"A6-3\":0,\"A6-4\":0}";
			Assert::AreEqual(testtraffic, checktraffic);
		}
	};
}
