#include "stdafx.h"
#include "CppUnitTest.h"
#include "../Controller/pch.h"
#include "../Controller/trafficlight.h"

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

		TEST_METHOD(TestMethodChangeTraffic)
		{

		}

		TEST_METHOD(TestMethodFetchjson)
		{

		}

		TEST_METHOD(TestMethodSendjson)
		{

		}

	};
}