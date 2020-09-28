using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public enum RegLightSequence
    {
        Green,
        Yellow,
        Red
    }

    public class RegularTrafficLight
    {
        const int delayedLightChange = 500; // in milliseconds
        RegLightSequence currentColor = RegLightSequence.Red;

        // Switch trafficlight to yellow, after a delay switch to green
        public void SwitchLightToGreen()
        {

        }

        // Switch trafficlight to yellow, after a delay switch to red
        public void SwitchLightToRed()
        {

        }
    }
}
