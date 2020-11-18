using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simulator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Simulator.Testing
{
    [TestClass()]
    public class UnitTest1
    {
        Simulator.RegularTrafficLight lights = new RegularTrafficLight();

        string jsonString = "{\"A1-1\":1,\"A1-2\":1,\"A1-3\":0}";

        Mock mock = new Mock<BackGroundListener>();

        JObject incomingJSON = null;

        [TestMethod()]
        public void isTrafficLightCreated()
        {
            lights.createTrafficLight(0,0,"test","left");
            Assert.AreEqual("test", lights.name);
        }

        [TestMethod()]
        public void JSON_parsing_test()
        {
            incomingJSON = JObject.Parse(jsonString);

            int seq = (int)incomingJSON["A1-2"];
            int seq2 = (int)incomingJSON["A1-3"];
            Assert.AreEqual(1, seq);
            Assert.AreEqual(0, seq2);
        }
    }
}
